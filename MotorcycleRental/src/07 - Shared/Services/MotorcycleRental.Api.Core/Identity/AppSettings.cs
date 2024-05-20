namespace MotorcycleRental.Api.Core.Identity
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public int ExpirationHours { get; set; }
        public string Issuer { get; set; }
        public string ValidatedOn { get; set; }
    }
}
