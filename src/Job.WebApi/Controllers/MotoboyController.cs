using System.Security.Claims;
using Job.Domain.Commands.User.Motoboy;
using Job.Domain.Services.Interfaces;
using Job.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Job.WebApi.Controllers;

public class MotoboyController(
    ILogger<MotoboyController> logger,
    IMotoboyService motoboyService,
    TokenService tokenService) : BaseController
{
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Authentication(AuthenticationMotoboyCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Iniciado autenticação de motoboy");
        var response = await motoboyService.GetMotoboy(command, cancellationToken);
        return HandleResponse(response, response.Data?.Cnpj, "motoboy", tokenService);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Create(CreateMotoboyCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Iniciado criação de motoboy");
        var response = await motoboyService.CreateAsync(command, cancellationToken);
        return HandleResponse(response);
    }

    [HttpPost]
    [Authorize (Roles = "motoboy")]
    public async Task<IActionResult> UploadImage([FromForm] UploadCnhMotoboyCommand file, CancellationToken cancellationToken)
    {
        logger.LogInformation("Iniciado upload de imagem");
        var cnpj = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        var response = await motoboyService.UploadImageAsync(cnpj!, file, cancellationToken);
        return HandleResponse(response);
    }
}