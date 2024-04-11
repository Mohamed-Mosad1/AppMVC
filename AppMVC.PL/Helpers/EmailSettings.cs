using AppMVC.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace AppMVC.PL.Helpers
{
    public class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            // Email Server : Gmail.com

            var client = new SmtpClient("smtp.gmail.com", 587);

            client.EnableSsl = true;

            client.Credentials = new NetworkCredential("mohamedmosaad115@gmail.com", "dvtcsttfyiabcbtz");

            client.Send("mohamedmosaad115@gmail.com", email.Recipients, email.Subject, email.Body); // Send Email
        }
    }
}
