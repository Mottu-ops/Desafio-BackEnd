namespace MotorcycleRental.MailServiceConsumer.Config
{
    public class SendMailConfig
    {
        public string SmtpAddress { get; set; }
        public int PortNumber { get; set; }
        public string DisplayName { get; set; }
        public string EmailFromAddress { get; set; }
        public string Login { get; set; }
        public string PassWord { get; set; }
    }
}
