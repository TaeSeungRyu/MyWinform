using System.Net.Sockets;

namespace WinFormsHello.Services;

internal sealed class MyTcpService
{
    public static MyTcpService Instance { get; } = new();

    // 외부에서 new MyTcpService() 호출 방지
    private MyTcpService() { }

    /// <summary>
    /// Modbus TCP 서버에서 Holding Register 값을 읽어오는 함수
    /// </summary>
    /// <param name="host">서버 IP 주소</param>
    /// <param name="port">서버 포트</param>
    /// <param name="startAddress">읽기 시작할 레지스터 주소</param>
    /// <param name="count">읽어올 레지스터 개수</param>
    /// <returns>레지스터 값 배열 (Java의 int[]와 비슷하지만 ushort는 0~65535)</returns>
    public async Task<ushort[]> ReadHoldingRegistersAsync(
        string host,
        int port,
        ushort startAddress,
        ushort count)
    {
        // Modbus 규칙: 한 번에 1~125개의 Holding Register를 읽을 수 있음
        if (count == 0 || count > 125)
            throw new ArgumentOutOfRangeException(nameof(count));

        // TCP 클라이언트 생성
        // using var: 함수 종료 시 자동으로 연결 자원 해제
        using var client = new TcpClient();

        // TCP 서버에 접속
        await client.ConnectAsync(host, port);

        // TCP 통신에 사용할 데이터 스트림 가져오기
        // Java의 InputStream / OutputStream과 비슷
        using NetworkStream stream = client.GetStream();

        // Modbus TCP 요청 메시지는 총 12바이트
        // MBAP 헤더(7바이트) + 기능 코드(1) + 시작 주소(2) + 개수(2)
        byte[] request = new byte[12];

        // Transaction ID: 요청과 응답을 구분하는 번호
        WriteUInt16(request, 0, 1);

        // Protocol ID: Modbus TCP는 항상 0
        WriteUInt16(request, 2, 0);

        // Length: Unit ID부터 마지막 데이터까지 총 6바이트
        WriteUInt16(request, 4, 6);

        // Unit ID: 통신할 장비 번호
        request[6] = 1;

        // Function Code 0x03: Holding Register 읽기
        request[7] = 0x03;

        // 몇 번 주소부터 읽을지 설정
        WriteUInt16(request, 8, startAddress);

        // 몇 개의 레지스터를 읽을지 설정
        WriteUInt16(request, 10, count);

        // 위에서 만든 12바이트 요청을 TCP 서버로 전송
        await stream.WriteAsync(request);

        // ==============================
        // 여기부터 서버 응답 수신
        // ==============================

        // 먼저 Modbus TCP 응답 헤더 7바이트 읽기
        byte[] header = new byte[7];

        // TCP는 데이터가 나누어 도착할 수 있으므로
        // 정확히 7바이트가 모일 때까지 비동기로 읽음
        await stream.ReadExactlyAsync(header);

        // 응답에 포함된 요청 번호
        ushort transactionId = ReadUInt16(header, 0);

        // 응답 프로토콜 번호 (정상 Modbus TCP라면 0)
        ushort protocolId = ReadUInt16(header, 2);

        // 헤더 이후 읽어야 할 데이터 길이
        // 정확히는 Unit ID를 포함한 길이
        ushort length = ReadUInt16(header, 4);

        // 요청 번호, 프로토콜, 데이터 길이, 장비 번호 검사
        if (transactionId != 1 || protocolId != 0 ||
            length < 3 || length > 254 || header[6] != 1)
        {
            throw new IOException("잘못된 Modbus TCP 응답입니다.");
        }

        // Unit ID는 헤더에서 이미 읽었으므로 1바이트 제외
        byte[] response = new byte[length - 1];

        // 나머지 응답 데이터를 모두 읽기
        await stream.ReadExactlyAsync(response);

        // 0x83은 0x03 요청에 대한 오류 응답
        // 예: 존재하지 않는 레지스터 주소 요청
        if (response[0] == 0x83)
        {
            throw new IOException(
                $"Modbus 예외 응답: 0x{response[1]:X2}");
        }

        // 정상 응답인지 검사
        // response[0]: 기능 코드 (0x03)
        // response[1]: 데이터 바이트 수 (레지스터 1개 = 2바이트)
        if (response[0] != 0x03 ||
            response[1] != count * 2 ||
            response.Length != 2 + count * 2)
        {
            throw new IOException("레지스터 응답 형식이 올바르지 않습니다.");
        }

        // 서버에서 받은 레지스터 값을 저장할 배열
        ushort[] registers = new ushort[count];

        // 응답의 바이트 데이터를 ushort 숫자로 변환
        for (int i = 0; i < count; i++)
        {
            registers[i] = ReadUInt16(response, 2 + i * 2);
        }

        // 변환된 레지스터 배열 반환
        return registers;
    }

    // 바이트 배열에서 2바이트를 읽어 ushort 숫자로 변환
    // Modbus는 큰 바이트가 먼저 오는 Big Endian 방식
    private static ushort ReadUInt16(byte[] data, int offset)
    {
        return (ushort)((data[offset] << 8) | data[offset + 1]);
    }

    // ushort 숫자를 2바이트로 나누어 배열에 저장
    private static void WriteUInt16(
        byte[] data,
        int offset,
        ushort value)
    {
        // 상위 8비트 저장
        data[offset] = (byte)(value >> 8);

        // 하위 8비트 저장
        data[offset + 1] = (byte)value;
    }
}