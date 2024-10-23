using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MT.Backend.Challenge.Application.Helpers;
using MT.Backend.Challenge.Domain.Constants;
using MT.Backend.Challenge.Domain.Entities;
using MT.Backend.Challenge.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MT.Backend.Challenge.Application.Commands.Rentals.ChangeRental
{
    public class ChangeRentalHandler : IRequestHandler<ChangeRentalRequest, ChangeRentalResponse>
    {
        private ILogger<ChangeRentalHandler> Logger { get; }
        private IWriteRepository<Rental> WriteRepository { get; }
        private IReadRepository<Rental> ReadRepository { get; }
        private IMapper Mapper { get; }

        private readonly HandleResponseExceptionHelper HandleResponseExceptionHelper;

        public ChangeRentalHandler(ILogger<ChangeRentalHandler> logger, IWriteRepository<Rental> writeRepository, IReadRepository<Rental> readRepository, IMapper mapper)
        {
            Logger = logger;
            WriteRepository = writeRepository;
            ReadRepository = readRepository;
            Mapper = mapper;
            HandleResponseExceptionHelper = new HandleResponseExceptionHelper(logger);
        }

        public async Task<ChangeRentalResponse> Handle
            (
                ChangeRentalRequest request,
                CancellationToken cancellationToken
            )
        {
            Logger.LogInformation("Handling ChangeRentalRequest");
            var response = new ChangeRentalResponse()
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

                var rental = operationResult.Result;
                rental.EstimatedEndDate = request.EstimatedEndDate;

                var result = await WriteRepository.Update(rental);


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
