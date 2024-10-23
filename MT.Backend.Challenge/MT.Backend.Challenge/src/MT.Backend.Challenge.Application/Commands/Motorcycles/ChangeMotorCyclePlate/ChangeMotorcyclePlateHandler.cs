using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MT.Backend.Challenge.Application.Commands.DeliveryDrivers.AddDeliveryDriverLicense;
using MT.Backend.Challenge.Application.Helpers;
using MT.Backend.Challenge.Domain.Constants;
using MT.Backend.Challenge.Domain.Entities;
using MT.Backend.Challenge.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MT.Backend.Challenge.Application.Commands.Motorcycles.ChangeMotorcyclePlate
{
    public class ChangeMotorcyclePlateHandler : IRequestHandler<ChangeMotorcyclePlateRequest, ChangeMotorcyclePlateResponse>
    {
        private ILogger<ChangeMotorcyclePlateHandler> Logger { get; }
        private IWriteRepository<Motorcycle> WriteRepository { get; }
        private IReadRepository<Motorcycle> ReadRepository { get; }
        private IMapper Mapper { get; }

        private readonly HandleResponseExceptionHelper HandleResponseExceptionHelper;

        public ChangeMotorcyclePlateHandler(
            IWriteRepository<Motorcycle> writeRepository,
            IReadRepository<Motorcycle> readRepository,
            ILogger<ChangeMotorcyclePlateHandler> logger,
            IMapper mapper
            )
        {
            Logger = logger;
            WriteRepository = writeRepository;
            ReadRepository = readRepository;
            Mapper = mapper;
            HandleResponseExceptionHelper = new HandleResponseExceptionHelper(logger);
        }

        public async Task<ChangeMotorcyclePlateResponse> Handle
            (
                ChangeMotorcyclePlateRequest request,
                CancellationToken cancellationToken
            )
        {
            Logger.LogInformation("Handling GetDeliveryDriverRequest");
            var response = new ChangeMotorcyclePlateResponse()
            {
                Message = ServiceConstants.DefaultSuccessMessage
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

                var motorcycle = operationResult.Result;
                motorcycle.LicensePlate = request.NewPlateNumber;

                var result = await WriteRepository.Update(motorcycle);

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