namespace Ticket.Interface;

public interface IEmailService
{
    void SendMail(string email, string subject, string message);
}
