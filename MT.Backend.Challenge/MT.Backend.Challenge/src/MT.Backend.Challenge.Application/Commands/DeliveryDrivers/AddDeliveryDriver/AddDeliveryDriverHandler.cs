using AutoMapper;
using MediatR;
using MT.Backend.Challenge.Application.Helpers;
using MT.Backend.Challenge.Domain.Constants;
using MT.Backend.Challenge.Domain.Entities;
using MT.Backend.Challenge.Domain.Entities.Response;
using MT.Backend.Challenge.Domain.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MT.Backend.Challenge.Application.Commands.DeliveryDrivers.AddDeliveryDriver
{
    public class AddDeliveryDriverHandler : IRequestHandler<AddDeliveryDriverRequest, AddDeliveryDriverResponse>
    {
        private ILogger<AddDeliveryDriverHandler> Logger { get; }
        private IWriteRepository<DeliveryDriver> WriteRepository { get; }
        private IReadRepository<DeliveryDriver> ReadRepository { get; }
        private IMapper Mapper { get; }

        private readonly HandleResponseExceptionHelper HandleResponseExceptionHelper;

        private readonly ImageService ImageService;

        public AddDeliveryDriverHandler(
            IWriteRepository<DeliveryDriver> writeRepository,
            IReadRepository<DeliveryDriver> readRepository,
            ILogger<AddDeliveryDriverHandler> logger,
            IMapper mapper
            )
        {
            Logger = logger;
            WriteRepository = writeRepository;
            ReadRepository = readRepository;
            Mapper = mapper;
            HandleResponseExceptionHelper = new HandleResponseExceptionHelper(logger);
            ImageService = new ImageService();
        }

        public async Task<AddDeliveryDriverResponse> Handle(AddDeliveryDriverRequest request, CancellationToken cancellationToken)
        {
            Logger.LogInformation("Handling GetDeliveryDriverRequest");
            var response = new AddDeliveryDriverResponse()
            {
                Message = ServiceConstants.DefaultSuccessMessage,
                StatusCode = 201
            };

            try
            {
                var operationFindResult = await ReadRepository.Find(x => x.Document == request.Document);

                if (operationFindResult.Result is not null && operationFindResult.Result.Count > 0)
                {
                    response.Message = ServiceConstants.DocumentAlreadyExists;
                    response.StatusCode = 400;
                    return response;
                }
                var imageUrl = ImageService.UploadImage(request.DriversLicenseImage);

                var entity = Mapper.Map<DeliveryDriver>(request);
                entity.DriversLicenseImageUrl = imageUrl;
               
                var result = await WriteRepository.Add(entity);

                if (!result.Success)
                {
                    HandleResponseExceptionHelper.HandleResponseException(response, result);

                }

            }
            catch (Exception ex)
            {
                HandleResponseExceptionHelper.HandleException(response, ex);
            }

            Logger.LogInformation($"{ServiceConstants.DeliveryDriverService} Finish Handle request");
            return response;
        }
    }
}
