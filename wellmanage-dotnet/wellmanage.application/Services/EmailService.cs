using System.Security.Authentication;
using wellmanage.application.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using wellmanage.application.Constants;
using wellmanage.application.Models;

namespace wellmanage.application.Services;

public class EmailService : IEmailService
{
    private readonly EmailConfiguration _emailConfig;
    public EmailService(EmailConfiguration emailConfig) => _emailConfig = emailConfig;
    public async Task SendEmail(Message message)
    {
        var emailMessage = CreateEmailMessage(message);
        await SendAsync(emailMessage);
    }

    private MimeMessage CreateEmailMessage(Message message)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress(EmailConstant.EmailHost, _emailConfig.From));
        emailMessage.To.AddRange(message.To);
        emailMessage.Subject = message.Subject;
        var contentType = message.ContentType != null ? message.ContentType : MimeKit.Text.TextFormat.Text;
        emailMessage.Body = new TextPart(contentType) { Text = message.Content };

        return emailMessage;
    }

    private async Task SendAsync(MimeMessage mailMessage)
    {
        using var client = new SmtpClient();
        try
        {
            client.ServerCertificateValidationCallback = (s, c, h, e) => true;
            //client.CheckCertificateRevocation = false;
            client.SslProtocols = SslProtocols.Ssl3 | SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12 | SslProtocols.Tls13;
            await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, SecureSocketOptions.Auto);
            client.AuthenticationMechanisms.Remove("XOAUTH2");
            await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);
            await client.SendAsync(mailMessage);
        }
        finally
        {
            await client.DisconnectAsync(true);
            client.Dispose();
        }
    }
}