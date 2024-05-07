using Job.Domain.Commands.Rent;
using Job.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Job.WebApi.Controllers;

[Authorize(Roles = "motoboy")]
public sealed class RentalController(
    ILogger<RentalController> logger,
    IRentService rentService
) : BaseController
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create([FromBody] CreateRentCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Criando aluguel");
        var cnpj = GetCnpj();
        if (cnpj is null)
            return Unauthorized();
        command.Cnpj = cnpj;

        var response = await rentService.CreateRentAsync(command, cancellationToken);
        return HandleResponse(response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Cancel([FromBody] CancelRentCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Cancelando aluguel");
        var response = await rentService.CancelRentAsync(command, cancellationToken);
        return HandleResponse(response);
    }
}