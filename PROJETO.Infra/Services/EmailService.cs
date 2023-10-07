using System.Net;
using System.Net.Mail;

using Microsoft.Extensions.Configuration;

using PROJETO.Infra.Interfaces.Services;

namespace PROJETO.Domain.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _config;

    private readonly string _email;

    private readonly string _pwd;

    public EmailService(IConfiguration config)
    {
        _config = config;
        _email = _config.GetSection("Email")["Login"]!;
        _pwd = _config.GetSection("Email")["Password"]!;
    }

    public async Task SendEmail(string to, string subject, string message)
    {
        var client = new SmtpClient("smtp-mail.outlook.com", 587)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential(_email, _pwd),
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false
        };

        await client.SendMailAsync(new MailMessage(_email, to, subject, message));
    }
}
