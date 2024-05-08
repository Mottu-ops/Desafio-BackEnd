using Job.Domain.Commands.User.Manager;
using Job.Domain.Services.Interfaces;
using Job.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Job.WebApi.Controllers;

[AllowAnonymous]
public class ManagerController(
    ILogger<ManagerController> logger,
    IManagerService managerService,
    TokenService tokenService) : BaseController
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Authentication(AuthenticationManagerCommand command)
    {
        logger.LogInformation("Iniciado autenticação de admin");
        var response = await managerService.GetManager(command);
        return HandleResponse(response, response.Data?.Email, "admin", tokenService);
    }
}