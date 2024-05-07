using Job.Domain.Commands.Moto;
using Job.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Job.WebApi.Controllers;

public class MotoController(
    ILogger<MotoController> logger,
    IMotoService motoService) : BaseController
{
    [HttpGet]
    [Authorize(Roles = "admin,motoboy")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        logger.LogInformation("Recuperando todos as motos cadastradas");
        var motos = await motoService.GetAllAsync(cancellationToken);
        return HandleResponse(motos);
    }

    [HttpGet]
    [Authorize(Roles = "admin,motoboy")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromQuery] Guid id, CancellationToken cancellationToken)
    {
        logger.LogInformation("Recuperando moto por id {id}", id);
        var moto = await motoService.GetByIdAsync(id, cancellationToken);
        return HandleResponse(moto);
    }

    [HttpGet]
    [Authorize(Roles = "admin,motoboy")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByPlate([FromQuery] string plate, CancellationToken cancellationToken)
    {
        logger.LogInformation("Recuperando moto por placa {plate}", plate);
        var moto = await motoService.GetByPlateAsync(plate, cancellationToken);
        return HandleResponse(moto);
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create([FromBody] CreateMotoCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Criando moto");
        var response = await motoService.CreateAsync(command, cancellationToken);
        return HandleResponse(response);
    }

    [HttpPut]
    [Authorize(Roles = "admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromBody] UpdateMotoCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Atualizando moto");
        var response = await motoService.UpdateAsync(command, cancellationToken);
        return HandleResponse(response);
    }

    [HttpDelete]
    [Authorize(Roles = "admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromQuery] Guid id, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deletando moto");
        var response = await motoService.DeleteAsync(id, cancellationToken);
        return HandleResponse(response);
    }
}