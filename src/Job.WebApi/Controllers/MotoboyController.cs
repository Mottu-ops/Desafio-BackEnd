using System.Security.Claims;
using Job.Domain.Commands.User.Motoboy;
using Job.Domain.Services.Interfaces;
using Job.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Job.WebApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class MotoboyController(
    ILogger<MotoboyController> logger,
    IMotoboyService motoboyService,
    TokenService tokenService) : ControllerBase
{
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Authentication(AuthenticationMotoboyCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Iniciado autenticação de motoboy");
        var motoboy = await motoboyService.GetMotoboy(command, cancellationToken);

        if (!motoboy.Success) return BadRequest(motoboy.Errors);

        if (motoboy.Data is null)
            return NotFound();

        var token = tokenService.GenerateToken(motoboy.Data.Cnpj, "motoboy");
        return Ok(token);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Create(CreateMotoboyCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Iniciado criação de motoboy");
        var response = await motoboyService.CreateAsync(command, cancellationToken);

        if (!response.Success)
        {
            return BadRequest(response.Errors);
        }

        return Ok(response.Id);
    }

    [HttpPost]
    [Authorize (Roles = "motoboy")]
    public async Task<IActionResult> UploadImage([FromForm] UploadCnhMotoboyCommand file, CancellationToken cancellationToken)
    {
        logger.LogInformation("Iniciado upload de imagem");
        var cnpj = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        var response = await motoboyService.UploadImageAsync(cnpj!, file, cancellationToken);

        if (!response.Success)
        {
            return BadRequest(response.Errors);
        }

        return Ok(response.Id);
    }
}