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

namespace MT.Backend.Challenge.Application.Commands.Motorcycles.AddMotorcycle
{
    public class AddMotorcycleHandler : IRequestHandler<AddMotorcycleRequest, AddMotorcycleResponse>
    {
        private ILogger<AddMotorcycleHandler> Logger { get; }
        private IWriteRepository<Motorcycle> Repository { get; }
        private IMapper Mapper { get; }

        private readonly HandleResponseExceptionHelper HandleResponseExceptionHelper;


        public AddMotorcycleHandler(
            IWriteRepository<Motorcycle> repository,
            ILogger<AddMotorcycleHandler> logger,
            IMapper mapper
            )
        {
            Logger = logger;
            Repository = repository;
            Mapper = mapper;
            HandleResponseExceptionHelper = new HandleResponseExceptionHelper(logger);
        }

        public async Task<AddMotorcycleResponse> Handle(AddMotorcycleRequest request, CancellationToken cancellationToken)
        {
            Logger.LogInformation("Handling AddMotorcycleRequest");
            var response = new AddMotorcycleResponse()
            {
                Message = ServiceConstants.DefaultSuccessMessage,
                StatusCode = 201
            };

            try
            {
                var entity = Mapper.Map<Motorcycle>(request);
                entity.CreatedAt = DateTime.Now;
                var result = await Repository.Add(entity);

                if (!result.Success)
                {
                    HandleResponseExceptionHelper.HandleResponseException(response, result);
                    // envia para as filas
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
