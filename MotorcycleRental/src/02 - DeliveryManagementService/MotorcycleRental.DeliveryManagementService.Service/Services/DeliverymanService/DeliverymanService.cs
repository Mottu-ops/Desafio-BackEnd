using AutoMapper;
using MotorcycleRental.DeliveryManagementService.Service.DTOs;
using MotorcycleRental.DeliveryManagementService.Service.Exceptions;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Interfaces;

namespace MotorcycleRental.DeliveryManagementService.Service.Services.DeliverymanService
{
    public class DeliverymanService : IDeliverymanService
    {
        private readonly IDeliverymanRepository _deliverymanRespository;
        private readonly IMapper _mapper;

        public DeliverymanService(IDeliverymanRepository deliverymanRespository, IMapper mapper)
        {
            _deliverymanRespository = deliverymanRespository;
            _mapper = mapper;
        }

        public async Task AddAsync(DeliverymanAddDto deliverymanDto)
        {
            Validations(deliverymanDto);
            Deliveryman deliveryman = _mapper.Map<Deliveryman>(deliverymanDto);
            deliveryman.SetNewId();
            var url = SaveImageToLocalStorage(deliveryman.Id, deliverymanDto.CNHImageBase64, deliverymanDto.CNHImageUrl);

            deliveryman.SetCnhImageUrl(url);
            try
            {
                await _deliverymanRespository.AddAsync(deliveryman);
            }
            catch (Exception)
            {
                RemoveImageToLocalStorage(deliveryman.CNHImageUrl);
                throw;
            }
        }

        public async Task AddPhotoCnh(DeliverymanAddPhotoDto addPhoto)
        {
            var deleiveryman = await _deliverymanRespository.GetByIdAsync(addPhoto.Id);

            var url = SaveImageToLocalStorage(addPhoto.Id, addPhoto.CnhImageBase64, addPhoto.CnhImageUrl);
            deleiveryman.SetCnhImageUrl(url);
            try
            {
                await _deliverymanRespository.AddAsync(deleiveryman);
            }
            catch (Exception)
            {
                RemoveImageToLocalStorage(addPhoto.CnhImageUrl);
                throw;
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            Deliveryman deliveryman = await _deliverymanRespository.GetByIdAsync(id);

            if (deliveryman == null)
                throw new NotFoundException("This mentioned deliveryman doesn't exist");

            await _deliverymanRespository.DeleteAsync(deliveryman);
        }

        public async Task<List<DeliverymanFullDto>> GetAllAsync()
        {
            var resul = await _deliverymanRespository.GetAllAsync();

            List<DeliverymanFullDto> list = _mapper.Map<List<DeliverymanFullDto>>(resul);

            return list;
        }

        public async Task<DeliverymanFullDto> GetByIdAsync(Guid id)
        {
            Deliveryman deliveryman = await _deliverymanRespository.GetByIdAsync(id);

            DeliverymanFullDto output = _mapper.Map<DeliverymanFullDto>(deliveryman);

            return output;
        }

        public async Task UpdateAsync(DeliverymanFullDto deliverymanDto)
        {
            Deliveryman deliveryman = _mapper.Map<Deliveryman>(deliverymanDto);

            await _deliverymanRespository.UpdateAsync(deliveryman);

        }

        private void Validations(DeliverymanAddDto deliverymanDto)
        {
            deliverymanDto.IsValidDriverLicenseType();

            if (!IsImageTypeValid(deliverymanDto.CNHImageBase64))
                throw new InvalidImageException("The file format must be png or bmp.");
        }

        private string SaveImageToLocalStorage(Guid driverId, byte[] imageBytes, string imagePath)
        {
            if (!Directory.Exists(imagePath))
            {
                Directory.CreateDirectory(imagePath);
            }

            imagePath = Path.Combine(imagePath, $"{driverId}.jpg");

            File.WriteAllBytes(imagePath, imageBytes);
            return imagePath;
        }

        private void RemoveImageToLocalStorage(string imagePath)
        {
            File.Delete(imagePath);
        }

        public static bool IsImageTypeValid(byte[] imageBytes)
        {
            bool isvalid = false;
            // Verificar os primeiros bytes (magic numbers) para identificar o tipo de imagem
            if (imageBytes.Length >= 4)
            {
                // PNG: bytes de assinatura são 137 80 78 71 (ou 0x89 0x50 0x4E 0x47)
                if (imageBytes[0] == 0x89 && imageBytes[1] == 0x50 && imageBytes[2] == 0x4E && imageBytes[3] == 0x47)
                    isvalid = true;
                // BMP: bytes de assinatura são 66 77 (ou 0x42 0x4D)
                else if (imageBytes[0] == 0x42 && imageBytes[1] == 0x4D)
                    isvalid = true;
                // JPEG: bytes de assinatura são 255 216 255 (ou 0xFF 0xD8 0xFF)
                //else if (imageBytes[0] == 0xFF && imageBytes[1] == 0xD8 && imageBytes[2] == 0xFF)            
            }
            return isvalid;
        }
    }
}
