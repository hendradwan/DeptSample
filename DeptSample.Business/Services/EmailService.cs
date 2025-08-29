using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace DeptSample.Business.Services
{
    public class EmailService
    {

        private readonly SmtpSettings _settings;

        public EmailService(IOptions<SmtpSettings> smtpOptions)
        {
            _settings = smtpOptions.Value;
        }

        public void SendReminderEmail(string to,string subject, string body)
        {
            var message = new MailMessage();
            message.To.Add(to); 
            message.Subject = subject;
            message.Body = body;
            message.From = new MailAddress(_settings.User);

           
            var smtp = new SmtpClient(_settings.Host, _settings.Port)
            {
                EnableSsl = _settings.EnableSsl,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_settings.User, _settings.Password)
            };
            try
            {
                smtp.Send(message);
                Console.WriteLine("Reminder email sent.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Email send failed: " + ex.Message);
            }
        }
    }
}