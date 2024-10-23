using MediatR;
using Microsoft.AspNetCore.Mvc;
using MT.Backend.Challenge.Api.Handlers;
using MT.Backend.Challenge.Application.Commands.DeliveryDrivers.AddDeliveryDriver;
using MT.Backend.Challenge.Application.Commands.DeliveryDrivers.AddDeliveryDriverLicense;
using Swashbuckle.AspNetCore.Annotations;

namespace MT.Backend.Challenge.Api.Controllers
{
    [Route("entregadores")]
    [ApiController]
    public class DeliveryDriverController : ControllerBase
    {
        private readonly ILogger<DeliveryDriverController> Logger;
        private IMediator Mediator { get; }

        public DeliveryDriverController(
            ILogger<DeliveryDriverController> logger,
            IMediator mediator
            )
        {
            Logger = logger;
            Mediator = mediator;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cadastra Entregadores", Description = "Insere um entregador")]
        [SwaggerResponse(201, Type = typeof(AddDeliveryDriverResponse))]
        [SwaggerResponse(400, Type = typeof(AddDeliveryDriverResponse))]
        [SwaggerResponse(500, Type = typeof(AddDeliveryDriverResponse))]
        public async Task<ActionResult> AddMotorcycle([FromBody] AddDeliveryDriverRequest request)
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

        [HttpPost("{id}/cnh")]
        [SwaggerOperation(Summary = "Envio CNH", Description = "Envia a imagem de CNH de um entregador")]
        [SwaggerResponse(201, Type = typeof(AddDeliveryDriverLicenseResponse))]
        [SwaggerResponse(400, Type = typeof(AddDeliveryDriverLicenseResponse))]
        [SwaggerResponse(404)]
        [SwaggerResponse(500, Type = typeof(AddDeliveryDriverLicenseResponse))]
        public async Task<ActionResult> AddDriverLicense(string id, [FromBody] AddDeliveryDriverLicenseRequest request)
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

    }
}
