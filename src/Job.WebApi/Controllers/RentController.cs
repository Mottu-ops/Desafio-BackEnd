using System.Security.Claims;
using Job.Domain.Commands.Rent;
using Job.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Job.WebApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
[Authorize(Roles = "motoboy")]
public sealed class RentController(
    ILogger<RentController> logger,
    IRentService rentService
) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRentCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Criando aluguel");
        var cnpj = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        command.Cnpj = cnpj!;

        var response = await rentService.CreateRentAsync(command, cancellationToken);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpPut]
    public async Task<IActionResult> Cancel([FromBody] CancelRentCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Cancelando aluguel");

        var response = await rentService.CancelRentAsync(command, cancellationToken);
        return response.Success ? Ok(response) : BadRequest(response);
    }
}