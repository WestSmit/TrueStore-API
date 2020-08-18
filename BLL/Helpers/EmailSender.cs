using MailKit.Net.Smtp;
using MimeKit;

namespace BLL.Helpers
{
    public class EmailSender
    {

        public void SendEmail(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("TrueStore", "mypetproject404@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);               
                client.Authenticate("mypetproject404@gmail.com", "superpassword");
                client.Send(emailMessage);
                client.Disconnect(true);
            }
        }
    }
}
