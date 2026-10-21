using System.Net;
using System.Net.Sockets;

var listener = new TcpListener(IPAddress.Loopback, 1502);
listener.Start();

Console.WriteLine("Modbus TCP 테스트 서버 시작: 127.0.0.1:1502");

ushort[] registers = { 230, 1250, 1 };

while (true)
{
    using TcpClient client = await listener.AcceptTcpClientAsync();
    using NetworkStream stream = client.GetStream();

    Console.WriteLine("클라이언트 연결");

    try
    {
        while (true)
        {
            // MBAP Header: 7 bytes
            byte[] header = new byte[7];
            await stream.ReadExactlyAsync(header);

            ushort transactionId = ReadUInt16(header, 0);
            ushort protocolId = ReadUInt16(header, 2);
            ushort length = ReadUInt16(header, 4);
            byte unitId = header[6];

            if (protocolId != 0 || length < 2 || length > 254)
                break;

            // Unit ID는 이미 읽었으므로 1바이트 제외
            byte[] request = new byte[length - 1];
            await stream.ReadExactlyAsync(request);

            byte functionCode = request[0];

            if (functionCode != 0x03 || request.Length != 5)
            {
                await SendExceptionAsync(
                    stream, transactionId, unitId, functionCode, 0x01);
                continue;
            }

            ushort startAddress = ReadUInt16(request, 1);
            ushort count = ReadUInt16(request, 3);

            if (count == 0 || count > 125)
            {
                await SendExceptionAsync(
                    stream, transactionId, unitId, functionCode, 0x03);
                continue;
            }

            if (startAddress >= registers.Length ||
                count > registers.Length - startAddress)
            {
                await SendExceptionAsync(
                    stream, transactionId, unitId, functionCode, 0x02);
                continue;
            }

            byte[] response = new byte[9 + count * 2];

            WriteUInt16(response, 0, transactionId);
            WriteUInt16(response, 2, 0);
            WriteUInt16(response, 4, (ushort)(3 + count * 2));

            response[6] = unitId;
            response[7] = 0x03;
            response[8] = (byte)(count * 2);

            for (int i = 0; i < count; i++)
            {
                WriteUInt16(
                    response,
                    9 + i * 2,
                    registers[startAddress + i]
                );
            }

            await stream.WriteAsync(response);

            Console.WriteLine(
                $"레지스터 조회: 시작={startAddress}, 개수={count}");
        }
    }
    catch (EndOfStreamException)
    {
        Console.WriteLine("클라이언트 연결 종료");
    }
    catch (IOException ex)
    {
        Console.WriteLine($"통신 종료: {ex.Message}");
    }
}

static ushort ReadUInt16(byte[] data, int offset)
{
    return (ushort)((data[offset] << 8) | data[offset + 1]);
}

static void WriteUInt16(byte[] data, int offset, ushort value)
{
    data[offset] = (byte)(value >> 8);
    data[offset + 1] = (byte)value;
}

static async Task SendExceptionAsync(
    NetworkStream stream,
    ushort transactionId,
    byte unitId,
    byte functionCode,
    byte exceptionCode)
{
    byte[] response = new byte[9];

    WriteUInt16(response, 0, transactionId);
    WriteUInt16(response, 2, 0);
    WriteUInt16(response, 4, 3);

    response[6] = unitId;
    response[7] = (byte)(functionCode | 0x80);
    response[8] = exceptionCode;

    await stream.WriteAsync(response);
}