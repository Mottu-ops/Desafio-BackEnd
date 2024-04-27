using Job.Domain.Commands.User.Manager;
using Job.Domain.Services.Interfaces;
using Job.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Job.WebApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
[AllowAnonymous]
public class ManagerController(
    ILogger<ManagerController> logger,
    IManagerService managerService,
    TokenService tokenService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Authentication(AuthenticationManagerCommand command)
    {
        logger.LogInformation("Iniciado autenticação de admin");
        var response = await managerService.GetManager(command);

        if (response.Success is false)
            return BadRequest(response.Errors);

        var query = response.Data;

        if (query is null) return NotFound();

        var token = tokenService.GenerateToken(query.Email, "admin");

        return Ok(token);
    }
}