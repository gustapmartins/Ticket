using System.Net;
using System.Net.Mail;
using Ticket.Interface;

namespace Ticket.Service;

public class EmailService: IEmailService
{

    public void SendMail(string email, string subject, string message)
    {
        string mail = "gustavopereirafacal@gmail.com";

        SmtpClient client = new SmtpClient("sandbox.smtp.mailtrap.io", 2525)
        {
            Credentials = new NetworkCredential("b6ccf0df5b6e1f", "de255eb01c2e93"),
            EnableSsl = true
        };

        client.Send(mail, email, subject, message);
    }
}
