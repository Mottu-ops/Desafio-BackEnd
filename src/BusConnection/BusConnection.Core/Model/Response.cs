namespace BusConnections.Core.Model
{
    public class Response
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public dynamic Payload { get; set; }

    }

}
