namespace Orkaris_Back.Services;

using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;

public class EmailService
{
    public EmailService()
    {
    }
    private readonly string smtpServer = "smtp.gmail.com";
    private readonly int smtpPort = 587;
    private readonly string smtpUser = "orkaris.info@gmail.com";
    private readonly string smtpPass = "lbps vone nytg jkik";

    public async Task SendEmailAsync(string toEmail, string subject, string htmlContent)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(smtpUser));
        email.To.Add(MailboxAddress.Parse(toEmail));
        email.Subject = subject;
        email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = htmlContent
        };

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(smtpServer, smtpPort, MailKit.Security.SecureSocketOptions.StartTls); // STARTTLS = sécurisé
        await smtp.AuthenticateAsync(smtpUser, smtpPass);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}
