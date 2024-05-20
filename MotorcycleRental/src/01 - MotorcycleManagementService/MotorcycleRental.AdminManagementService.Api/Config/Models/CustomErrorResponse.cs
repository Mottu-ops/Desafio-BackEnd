namespace MotorcycleRental.AdminManagementService.Api.Config.Models
{
    public class CustomErrorResponse
    {
        public CustomErrorResponse() { }

        public CustomErrorResponse(int status, ErrorDetail errors)
        {
            Title = "One or more validation errors occurred.";
            Status = status;
            Errors = errors;
        }

        public string Title { get; set; }
        public int Status { get; set; }
        public ErrorDetail Errors { get; set; }
    }

    public class ErrorDetail
    {
        public ErrorDetail() { }

        public ErrorDetail(List<string> messages)
        {
            Messages = messages;
        }

        public List<string> Messages { get; set; }
    }
}
