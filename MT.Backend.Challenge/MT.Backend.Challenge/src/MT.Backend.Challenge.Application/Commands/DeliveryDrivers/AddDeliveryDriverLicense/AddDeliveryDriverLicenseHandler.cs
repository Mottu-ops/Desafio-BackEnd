using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MT.Backend.Challenge.Application.Helpers;
using MT.Backend.Challenge.Domain.Constants;
using MT.Backend.Challenge.Domain.Entities;
using MT.Backend.Challenge.Domain.Entities.configs;
using MT.Backend.Challenge.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MT.Backend.Challenge.Application.Commands.DeliveryDrivers.AddDeliveryDriverLicense
{
    public class AddDeliveryDriverLicenseHandler : IRequestHandler<AddDeliveryDriverLicenseRequest, AddDeliveryDriverLicenseResponse>
    {
        private ILogger<AddDeliveryDriverLicenseHandler> Logger { get; }
        private IWriteRepository<DeliveryDriver> WriteRepository { get; }
        private IReadRepository<DeliveryDriver> ReadRepository { get; }
        private IMapper Mapper { get; }

        private readonly HandleResponseExceptionHelper HandleResponseExceptionHelper;

        private readonly ImageService ImageService;
        public AddDeliveryDriverLicenseHandler(
            IOptions<ImageServiceConfig> config,
            IWriteRepository<DeliveryDriver> writeRepository,
            IReadRepository<DeliveryDriver> readRepository,
            ILogger<AddDeliveryDriverLicenseHandler> logger,
            IMapper mapper,
            IMediator mediator
            )
        {
            Logger = logger;
            WriteRepository = writeRepository;
            ReadRepository = readRepository;
            Mapper = mapper;
            HandleResponseExceptionHelper = new HandleResponseExceptionHelper(logger);

            ImageService = new ImageService(config.Value);
        }

        public async Task<AddDeliveryDriverLicenseResponse> Handle(AddDeliveryDriverLicenseRequest request, CancellationToken cancellationToken)
        {
            Logger.LogInformation("Handling GetDeliveryDriverRequest");
            var response = new AddDeliveryDriverLicenseResponse()
            {
                Message = ServiceConstants.DefaultSuccessMessage,
                StatusCode = 201
            };

            try
            {
                var operationResult = await ReadRepository.GetByIdAsync(request.Id);

                if (operationResult.Result is null)
                {
                    response.Message = ServiceConstants.NotFoundMessage;
                    response.StatusCode = 404;
                    return response;
                }

                var deliveryDriver = operationResult.Result;

                var imageUrl = ImageService.UploadImage(request.DriversLicenseImage);

                deliveryDriver.DriversLicenseImageUrl = imageUrl;


                var result = await WriteRepository.Update(deliveryDriver);

                if (!result.Success)
                {
                    HandleResponseExceptionHelper.HandleResponseException(response, result);
                }
            }
            catch (Exception ex)
            {
                HandleResponseExceptionHelper.HandleException(response, ex);
            }

            return response;
        }
    }
}
