using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace WinFormsHello.Services;

internal sealed class MyMailService
{
    public static MyMailService Instance { get; } = new();

    private MyMailService() { }

    public async Task SendMailAsync(
        string to,
        string subject,
        string body)
    {
        string user = Environment.GetEnvironmentVariable("SMTP_USER")
            ?? throw new InvalidOperationException("SMTP_USER가 없습니다.");

        string password = Environment.GetEnvironmentVariable("SMTP_PASSWORD")
            ?? throw new InvalidOperationException("SMTP_PASSWORD가 없습니다.");

        var message = new MimeMessage();

        message.From.Add(MailboxAddress.Parse(user));
        message.To.Add(MailboxAddress.Parse(to));
        message.Subject = subject;

        message.Body = new TextPart("plain")
        {
            Text = body
        };

        using var client = new SmtpClient();

        await client.ConnectAsync(
            host: "smtp.naver.com",
            port: 465,
            options: SecureSocketOptions.SslOnConnect
        );

        await client.AuthenticateAsync(user, password);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}