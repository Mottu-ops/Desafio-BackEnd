using MediatR;
using Microsoft.AspNetCore.Mvc;
using MT.Backend.Challenge.Api.Handlers;
using MT.Backend.Challenge.Application.Commands.Rentals.AddRental;
using MT.Backend.Challenge.Application.Commands.Rentals.ChangeRental;
using MT.Backend.Challenge.Application.Queries.Rentals.GetRentalById;
using Swashbuckle.AspNetCore.Annotations;

namespace MT.Backend.Challenge.Api.Controllers
{
    [Route("locacao")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly ILogger<RentalController> Logger;
        private IMediator Mediator { get; }

        public RentalController(
            ILogger<RentalController> logger,
            IMediator mediator
            )
        {
            Logger = logger;
            Mediator = mediator;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Locação", Description = "Realiza a locação de uma moto")]
        [SwaggerResponse(201, Type = typeof(AddRentalResponse))]
        [SwaggerResponse(400, Type = typeof(AddRentalResponse))]
        [SwaggerResponse(500, Type = typeof(AddRentalResponse))]
        public async Task<ActionResult> AddRental([FromBody] AddRentalRequest request)
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

        [HttpPut("{id}/devolucao")]
        [SwaggerOperation(Summary = "Finaliza Locação", Description = "Finaliza a locação de uma moto")]
        [SwaggerResponse(200, Type = typeof(ChangeRentalResponse))]
        [SwaggerResponse(400, Type = typeof(ChangeRentalResponse))]
        [SwaggerResponse(404)]
        [SwaggerResponse(500, Type = typeof(ChangeRentalResponse))]
        public async Task<ActionResult> ChangeRentalStatus(string id, [FromBody] ChangeRentalRequest request)
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

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Detalhes da Locação", Description = "Obtém detalhes de uma locação pelo ID")]
        [SwaggerResponse(200, Type = typeof(GetRentalByIdResponse))]
        [SwaggerResponse(400, Type = typeof(GetRentalByIdResponse))]
        [SwaggerResponse(404)]
        [SwaggerResponse(500, Type = typeof(GetRentalByIdResponse))]
        public async Task<ActionResult> GetRentalById(string id)
        {
            try
            {
                var request = new GetRentalByIdRequest { Id = id };
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
