namespace MotorcycleRental.MailServiceConsumer.Service
{
    public interface INotifyService
    {
        Task SendMail(string[] emails, string subject, string body, bool isHtml = false);
    }
}
