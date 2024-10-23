using MediatR;
using Microsoft.Extensions.Logging;
using MT.Backend.Challenge.Application.Helpers;
using MT.Backend.Challenge.Application.Queries.Motorcycles.GetMotorCycleById;
using MT.Backend.Challenge.Domain.Constants;
using MT.Backend.Challenge.Domain.Entities;
using MT.Backend.Challenge.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MT.Backend.Challenge.Application.Queries.Motorcycles.GetMotorCycle
{
    public class GetMotorCycleHandler : IRequestHandler<GetMotorCycleRequest, GetMotorCycleResponse>
    {
        private ILogger<GetMotorCycleHandler> Logger { get; }
        private IReadRepository<Motorcycle> ReadRepository { get; }

        private readonly HandleResponseExceptionHelper HandleResponseExceptionHelper;

        public GetMotorCycleHandler(ILogger<GetMotorCycleHandler> logger, IReadRepository<Motorcycle> readRepository)
        {
            Logger = logger;
            ReadRepository = readRepository;
            HandleResponseExceptionHelper = new HandleResponseExceptionHelper(logger);
        }

        public async Task<GetMotorCycleResponse> Handle
            (
                GetMotorCycleRequest request,
                CancellationToken cancellationToken
            )
        {
            Logger.LogInformation("Handling GetMotorCycleRequest");
            var response = new GetMotorCycleResponse()
            {
                Message = ServiceConstants.DefaultSuccessMessage
            };

            try
            {
                var operationResult = await ReadRepository.Find(x => x.LicensePlate == request.LicensePlate);

                if (operationResult.Result is null || operationResult.Result.Count==0)
                {
                    response.Message = "Not Found";
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
