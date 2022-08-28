using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace _01_Framework.Application.Email
{
    public class EmailService : IEmailService
    {
        public void SendEmail(string destination,string subject,string body)
        {
            var message = new MimeMessage();

            var from = new MailboxAddress("MyStore", "rubik.software1@gmail.com");
            message.From.Add(from);

            var to = new MailboxAddress("User", destination);
            message.To.Add(to);

            message.Subject = subject;
            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = body
            };

            message.Body = bodyBuilder.ToMessageBody();

            var client = new SmtpClient();
            client.Connect("smtp.gmail.com", 465, true);
            client.Authenticate("rubik.software1@gmail.com", "mfntllsnrkkzvzmf");
            client.Send(message);
            client.Disconnect(true);
            client.Dispose();
        }
    }
}
