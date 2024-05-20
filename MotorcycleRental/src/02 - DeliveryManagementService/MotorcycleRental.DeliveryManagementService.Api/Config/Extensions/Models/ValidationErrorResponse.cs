namespace MotorcycleRental.DeliveryManagementService.Api.Config.Extensions.Models
{
    public class ValidationErrorResponse
    {
        public ValidationErrorResponse(string propertyName, string errorMessage)
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
        }

        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }
    }
}
