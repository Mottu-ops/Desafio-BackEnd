using MediatR;
using Microsoft.Extensions.Logging;
using MT.Backend.Challenge.Application.Helpers;
using MT.Backend.Challenge.Domain.Constants;
using MT.Backend.Challenge.Domain.Entities;
using MT.Backend.Challenge.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MT.Backend.Challenge.Application.Queries.Motorcycles.GetMotorCycleById
{
    public class GetMotorCycleByIdHandler : IRequestHandler<GetMotorCycleByIdRequest, GetMotorCycleByIdResponse>
    {


        private ILogger<GetMotorCycleByIdHandler> Logger { get; }
        private IReadRepository<Motorcycle> ReadRepository { get; }

        private readonly HandleResponseExceptionHelper HandleResponseExceptionHelper;

        public GetMotorCycleByIdHandler(ILogger<GetMotorCycleByIdHandler> logger, IReadRepository<Motorcycle> readRepository)
        {
            Logger = logger;
            ReadRepository = readRepository;
            HandleResponseExceptionHelper = new HandleResponseExceptionHelper(logger);
        }
        public async Task<GetMotorCycleByIdResponse> Handle(GetMotorCycleByIdRequest request, CancellationToken cancellationToken)
        {
            Logger.LogInformation("Handling GetMotorCycleByIdRequest");
            var response = new GetMotorCycleByIdResponse()
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
