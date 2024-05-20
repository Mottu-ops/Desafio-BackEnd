namespace MotorcycleRental.MotorcycleConsumer.Models
{
    public class EmailVm
    {
        public EmailVm(string[] emails, string subject, string body, bool isHtml)
        {
            Emails = emails;
            Subject = subject;
            Body = body;
            IsHtml = isHtml;
        }

        public string[] Emails { get; private set; }
        public string Subject { get; private set; }
        public string Body { get; private set; }
        public bool IsHtml { get; private set; } = false;
    }
}
