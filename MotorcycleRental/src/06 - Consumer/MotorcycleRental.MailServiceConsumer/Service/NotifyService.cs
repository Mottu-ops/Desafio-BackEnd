using Microsoft.Extensions.Options;
using MotorcycleRental.MailServiceConsumer.Config;
using System.Net;
using System.Net.Mail;

namespace MotorcycleRental.MailServiceConsumer.Service
{
    public class NotifyService : INotifyService
    {
        private readonly SendMailConfig _sendMailConfig;

        public NotifyService(IOptions<SendMailConfig> sendMailConfig, IConfiguration configuration)
        {
            _sendMailConfig = sendMailConfig.Value;
        }

        public async Task SendMail(string[] emails, string subject, string body, bool isHtml = false)
        {
            MailMessage mailMessage = new MailMessage();

            mailMessage.From = new MailAddress(_sendMailConfig.EmailFromAddress, _sendMailConfig.DisplayName);
            AddEmailsToMailMessage(ref mailMessage, emails);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;
            mailMessage.Priority = MailPriority.Normal;

            try
            {
                using var smtp = new SmtpClient(_sendMailConfig.SmtpAddress, _sendMailConfig.PortNumber);

                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(_sendMailConfig.Login, _sendMailConfig.PassWord);

                smtp.Send(mailMessage);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void AddEmailsToMailMessage(ref MailMessage mailMessage, string[] emails)
        {
            foreach (var email in emails)
            {
                mailMessage.To.Add(email);
            }
        }
    }
}