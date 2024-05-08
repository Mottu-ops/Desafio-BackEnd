using System.Security.Claims;
using Job.Domain.Commands;
using Job.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace Job.WebApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class BaseController : ControllerBase
{
    protected IActionResult HandleResponse<T>(CommandResponse<T> response)
    {
        if (response.Success is false)
            return BadRequest(response.Errors);

        if(response.Id == Guid.Empty && response.Data is null)
            return NotFound();

        return Ok(response);
    }

    protected IActionResult HandleResponse<T>(T response)
    {
        if (response is null)
            return NotFound();

        return Ok(response);
    }

    protected IActionResult HandleResponse<T>(CommandResponse<T> response, string? name, string role, TokenService tokenService)
    {
        if (response.Success is false)
            return BadRequest(response.Errors);

        var query = response.Data;

        if (query is null) return Unauthorized();

        var token = tokenService.GenerateToken(name!, role);

        return Ok(new
        {
            token,
            Data = query
        });
    }

    protected string? GetCnpj()
    {
        var cnpj = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
        return cnpj;
    }
}