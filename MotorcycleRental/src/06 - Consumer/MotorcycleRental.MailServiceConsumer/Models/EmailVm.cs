namespace MotorcycleRental.MailServiceConsumer.Models
{
    public class EmailVm
    {
        public string[] Emails { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtml { get; set; } = false;
    }
}
