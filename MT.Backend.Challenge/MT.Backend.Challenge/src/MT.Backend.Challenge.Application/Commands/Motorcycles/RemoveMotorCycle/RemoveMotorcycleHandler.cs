using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MT.Backend.Challenge.Application.Commands.DeliveryDrivers.AddDeliveryDriverLicense;
using MT.Backend.Challenge.Application.Helpers;
using MT.Backend.Challenge.Domain.Constants;
using MT.Backend.Challenge.Domain.Entities;
using MT.Backend.Challenge.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MT.Backend.Challenge.Application.Commands.Motorcycles.RemoveMotorcycle
{
    public class RemoveMotorcycleHandler : IRequestHandler<RemoveMotorcycleRequest, RemoveMotorcycleResponse>
    {
        private ILogger<RemoveMotorcycleHandler> Logger { get; }
        private IWriteRepository<Motorcycle> WriteRepository { get; }
        private IReadRepository<Motorcycle> ReadRepository { get; }
        private IMapper Mapper { get; }

        private readonly HandleResponseExceptionHelper HandleResponseExceptionHelper;


        public RemoveMotorcycleHandler(
            IWriteRepository<Motorcycle> writeRepository,
            IReadRepository<Motorcycle> readRepository,
            ILogger<RemoveMotorcycleHandler> logger,
            IMapper mapper
            )
        {
            Logger = logger;
            WriteRepository = writeRepository;
            ReadRepository = readRepository;
            Mapper = mapper;
            HandleResponseExceptionHelper = new HandleResponseExceptionHelper(logger);
        }


        public async Task<RemoveMotorcycleResponse> Handle
            (
                RemoveMotorcycleRequest request,
                CancellationToken cancellationToken)
        {
            Logger.LogInformation("Handling RemoveMotorcycleResponse");
            var response = new RemoveMotorcycleResponse()
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


                var result = await WriteRepository.Delete(motorcycle);

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
