using MediatR;
using Microsoft.AspNetCore.Mvc;
using MT.Backend.Challenge.Api.Handlers;
using MT.Backend.Challenge.Application.Commands.Motorcycles.AddMotorcycle;
using MT.Backend.Challenge.Application.Commands.Motorcycles.ChangeMotorcyclePlate;
using MT.Backend.Challenge.Application.Commands.Motorcycles.RemoveMotorcycle;
using MT.Backend.Challenge.Application.Queries.Motorcycles.GetMotorCycle;
using MT.Backend.Challenge.Application.Queries.Motorcycles.GetMotorCycleById;
using Swashbuckle.AspNetCore.Annotations;

namespace MT.Backend.Challenge.Api.Controllers
{
    [ApiController]
    [Route("motos")]
    public class MotorcycleController : ControllerBase
    {
        private readonly ILogger<MotorcycleController> Logger;
        private IMediator Mediator { get; }

        public MotorcycleController(
            ILogger<MotorcycleController> logger,
            IMediator mediator
            )
        {
            Logger = logger;
            Mediator = mediator;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Motos", Description = "Lista as motos Cadastradas")]
        [SwaggerResponse(200, Type = typeof(GetMotorCycleResponse))]
        [SwaggerResponse(400, Type = typeof(GetMotorCycleResponse))]
        [SwaggerResponse(404)]
        [SwaggerResponse(500, Type = typeof(GetMotorCycleResponse))]

        public async Task<ActionResult> GetAllMotorcycles([FromQuery] GetMotorCycleRequest request)
        {
            try
            {
                var response = await Mediator.Send(request);

                if (response.Data.Any())
                {
                    return ResponseHandler.HandleResponse(response.Data);
                }

                return ResponseHandler.HandleResponse(response);
            }
            catch (Exception ex)
            {
                return ResponseHandler.HandleException(Logger, ex);
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Detalhes da Moto", Description = "Obtém detalhes de uma moto pelo ID")]
        [SwaggerResponse(200, Type = typeof(GetMotorCycleByIdResponse))]
        [SwaggerResponse(400, Type = typeof(GetMotorCycleByIdResponse))]
        [SwaggerResponse(404)]
        [SwaggerResponse(500, Type = typeof(GetMotorCycleByIdResponse))]
        public async Task<ActionResult> GetMotorcycleById(string id)
        {
            try
            {
                var request = new GetMotorCycleByIdRequest { Id = id };
                var response = await Mediator.Send(request);
                
                return ResponseHandler.HandleResponse(response);
            }
            catch (Exception ex)
            {
                return ResponseHandler.HandleException(Logger, ex);
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cadastra Moto", Description = "Cadastra uma moto")]
        [SwaggerResponse(201, Type = typeof(AddMotorcycleResponse))]
        [SwaggerResponse(400, Type = typeof(AddMotorcycleResponse))]
        [SwaggerResponse(500, Type = typeof(AddMotorcycleResponse))]
        public async Task<ActionResult> AddMotorcycle([FromBody] AddMotorcycleRequest request)
        {
            try
            {
                var response = await Mediator.Send(request);
                return ResponseHandler.HandleResponse(response);
            }
            catch (Exception ex)
            {
                return ResponseHandler.HandleException(Logger, ex);
            }
        }

        [HttpPut("{id}/placa")]
        [SwaggerOperation(Summary = "Atualiza Placa da Moto", Description = "Atualiza a placa de uma moto pelo ID")]
        [SwaggerResponse(200, Type = typeof(ChangeMotorcyclePlateResponse))]
        [SwaggerResponse(400, Type = typeof(ChangeMotorcyclePlateResponse))]
        [SwaggerResponse(404)]
        [SwaggerResponse(500, Type = typeof(ChangeMotorcyclePlateResponse))]
        public async Task<ActionResult> UpdateMotorcycleLicensePlate
            (
                string id,
                [FromBody] ChangeMotorcyclePlateRequest request
            )
        {
            try
            {
                request.Id = id;
                var response = await Mediator.Send(request);
                return ResponseHandler.HandleResponse(response);
            }
            catch (Exception ex)
            {
                return ResponseHandler.HandleException(Logger, ex);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Remove Moto", Description = "Remove uma moto pelo ID")]
        [SwaggerResponse(200, Type = typeof(RemoveMotorcycleResponse))]
        [SwaggerResponse(400, Type = typeof(RemoveMotorcycleResponse))]
        [SwaggerResponse(404)]
        [SwaggerResponse(500, Type = typeof(RemoveMotorcycleResponse))]
        public async Task<ActionResult> RemoveMotorcycle(string id)
        {
            try
            {
                var request = new RemoveMotorcycleRequest { Id = id };
                var response = await Mediator.Send(request);
                return ResponseHandler.HandleResponse(response);
            }
            catch (Exception ex)
            {
                return ResponseHandler.HandleException(Logger, ex);
            }
        }
    }
}
