namespace MotorcycleRental.DeliveryManagementService.Service.DTOs
{
    public class DeliverymanAddPhotoDto
    {
        public Guid Id { get; set; }
        public byte[] CnhImageBase64 { get; set; }
        public string CnhImageUrl { get; private set; } = string.Empty;

        public void SetUrlImage(string url)
        {
            CnhImageUrl = url;
        }
    }
}
