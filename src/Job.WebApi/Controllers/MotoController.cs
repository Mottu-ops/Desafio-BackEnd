using Job.Domain.Commands.Moto;
using Job.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Job.WebApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class MotoController(
    ILogger<MotoController> logger,
    IMotoService motoService) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = "admin,motoboy")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        logger.LogInformation("Recuperando todos as motos cadastradas");
        var lista = await motoService.GetAllAsync(cancellationToken);
        return Ok(lista);
    }

    [HttpGet]
    [Authorize(Roles = "admin,motoboy")]
    public async Task<IActionResult> GetById([FromQuery] Guid id, CancellationToken cancellationToken)
    {
        logger.LogInformation("Recuperando moto por id {id}", id);
        var moto = await motoService.GetByIdAsync(id, cancellationToken);
        return moto is null ? NotFound() : Ok(moto);
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Create([FromBody] CreateMotoCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Criando moto");
        var response = await motoService.CreateAsync(command, cancellationToken);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpPut]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Update([FromBody] UpdateMotoCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Atualizando moto");
        var response = await motoService.UpdateAsync(command, cancellationToken);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpDelete]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Delete([FromQuery] Guid id, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deletando moto");
        var response = await motoService.DeleteAsync(id, cancellationToken);
        return response.Success ? Ok(response) : BadRequest(response);
    }
}