using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MT.Backend.Challenge.Application.Commands.Rentals.ChangeRental;
using MT.Backend.Challenge.Application.Helpers;
using MT.Backend.Challenge.Domain.Constants;
using MT.Backend.Challenge.Domain.Entities;
using MT.Backend.Challenge.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MT.Backend.Challenge.Application.Queries.Rentals.GetRentalById
{
    public class GetRentalByIdHandler : IRequestHandler<GetRentalByIdRequest, GetRentalByIdResponse>
    {
        private ILogger<GetRentalByIdHandler> Logger { get; }        
        private IReadRepository<Rental> ReadRepository { get; }
        
        private readonly HandleResponseExceptionHelper HandleResponseExceptionHelper;

        public GetRentalByIdHandler(ILogger<GetRentalByIdHandler> logger, IReadRepository<Rental> readRepository)
        {
            Logger = logger;
            ReadRepository = readRepository;
            HandleResponseExceptionHelper = new HandleResponseExceptionHelper(logger);
        }

        public async Task<GetRentalByIdResponse> Handle(GetRentalByIdRequest request, CancellationToken cancellationToken)
        {
            Logger.LogInformation("Handling GetRentalByIdRequest");
            var response = new GetRentalByIdResponse()
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

                response.Data = operationResult.Result;
            }
            catch (Exception ex)
            {
                HandleResponseExceptionHelper.HandleException(response, ex);
            }

            return response;
        }
    }
}
