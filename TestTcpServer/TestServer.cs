using System.Net;
using System.Net.Sockets;
using System.Text;

var listener = new TcpListener(IPAddress.Loopback, 9000);

listener.Start();

Console.WriteLine("TCP 서버 실행: 127.0.0.1:9000");

while (true)
{
    using TcpClient client = await listener.AcceptTcpClientAsync();
    using NetworkStream stream = client.GetStream();

    using var reader = new StreamReader(stream, Encoding.UTF8);
    using var writer = new StreamWriter(stream, Encoding.UTF8)
    {
        AutoFlush = true
    };

    string? message = await reader.ReadLineAsync();

    Console.WriteLine($"수신: {message}");

    await writer.WriteLineAsync($"서버 응답: {message}");
}