namespace User.API.Models
{
    public class BaseResultModel
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public dynamic MetaData { get; set; }
    }
}
