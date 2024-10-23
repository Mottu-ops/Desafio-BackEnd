using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MT.Backend.Challenge.Application.Commands.Motorcycles.AddMotorcycle;
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

namespace MT.Backend.Challenge.Application.Commands.Rentals.AddRental
{
    public class AddRentalHandler : IRequestHandler<AddRentalRequest, AddRentalResponse>
    {
        private ILogger<AddRentalHandler> Logger { get; }
        private IWriteRepository<Rental> WriteRepository { get; }
        private IReadRepository<DeliveryDriver> ReadRepository { get; }
        private IMapper Mapper { get; }

        private readonly HandleResponseExceptionHelper HandleResponseExceptionHelper;


        public AddRentalHandler(
            IWriteRepository<Rental> writeRepository,
            IReadRepository<DeliveryDriver> readRepository,
            ILogger<AddRentalHandler> logger,
            IMapper mapper
            )
        {
            Logger = logger;
            WriteRepository = writeRepository;
            ReadRepository = readRepository;
            Mapper = mapper;
            HandleResponseExceptionHelper = new HandleResponseExceptionHelper(logger);
        }

        public async Task<AddRentalResponse> Handle
            (
                AddRentalRequest request,
                CancellationToken cancellationToken
            )
        {
            Logger.LogInformation("Handling AddRentalRequest");
            var response = new AddRentalResponse()
            {
                Message = ServiceConstants.DefaultSuccessMessage,
                StatusCode = 201
            };

            try
            {
                var operationFindResult = await ReadRepository.Find(x => x.Id == request.DeliveryDriverId);

                if (operationFindResult.Result is null || operationFindResult.Result.Count == 0)
                {
                    response.Message = ServiceConstants.DeliveryDriverNotFound;
                    response.StatusCode = 404;
                    return response;
                }

                var deliverDriver = operationFindResult.Result.FirstOrDefault();

                if (deliverDriver?.DriversLicenseCategory != DriversLicenseCategory.A )
                {
                    response.Message = ServiceConstants.DeliveryDriverLicenseCategoryInvalid;
                    response.StatusCode = 404;
                    return response;
                }

                var entity = Mapper.Map<Rental>(request);
                entity.Id = Guid.NewGuid().ToString();
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

            Logger.LogInformation($"{ServiceConstants.MotorcycleService} Finish Handle request");
            return response;
        }
    }
}
