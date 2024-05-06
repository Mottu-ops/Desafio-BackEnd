namespace User.API.ViewModels
{
    public class JwtTokenResult
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
