using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MoodleHack.API.Services
{
    public class EmailClient : IDisposable
    {
        private readonly SmtpClient _smtpClient;
        private readonly EmailConfiguration _configuration;

        public EmailClient(EmailConfiguration configuration)
        {
            _configuration = configuration;

            _smtpClient = new SmtpClient
            {
                Host = configuration.Host,
                Port = configuration.Port,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(configuration.Email, configuration.Password)
            };
        }
        public Task SendMessage(string toEmail, string toDisplayName, string message)
        {
            try
            {
                var mail = new MailMessage
                {
                    From = _configuration.From,
                    Subject = _configuration.Subject,
                    SubjectEncoding = Encoding.UTF8,
                    Body = message,
                    IsBodyHtml = true,
                    BodyEncoding = Encoding.UTF8
                };
                
                var to = new MailAddress(toEmail, toDisplayName);
                mail.To.Add(to);

                _smtpClient.Send(mail);

                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void Dispose()
        {
            _smtpClient?.Dispose();
        }
    }
}