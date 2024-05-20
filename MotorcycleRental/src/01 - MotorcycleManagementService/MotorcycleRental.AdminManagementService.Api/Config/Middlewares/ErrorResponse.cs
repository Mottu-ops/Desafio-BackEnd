using System.Net;

namespace MotorcycleRental.AdminManagementService.Api.Config.Middlewares
{
    public class ErrorResponse
    {
        public ErrorResponse(HttpStatusCode status, string type, string detail)
        {
            TraceId = Guid.NewGuid();
            Status = status;
            Type = type;
            Detail = detail;
        }

        public Guid TraceId { get; private set; }
        public HttpStatusCode Status { get; private set; }
        public string Type { get; private set; }
        public string Detail { get; private set; }

    }
}
