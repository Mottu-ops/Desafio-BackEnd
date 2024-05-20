namespace MotorcycleRental.DeliveryManagementService.Service.Exceptions
{
    public class InvalidImageException : Exception
    {
        public InvalidImageException()
        {
        }

        public InvalidImageException(string message)
            : base(message)
        {
        }
    }
}

