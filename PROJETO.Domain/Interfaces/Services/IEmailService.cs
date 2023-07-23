namespace PROJETO.Domain.Interfaces;

public interface IEmailService
{
    Task SendEmail(string to, string subjetct, string message);
}
