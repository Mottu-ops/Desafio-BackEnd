using Motorent.Application.Users.Commands.Register;
using Motorent.Contracts.Users.Requests;
using Motorent.Presentation.Common.Controllers;

namespace Motorent.Presentation.Controllers;

public sealed class UsersController : ApiController
{
    [HttpPost("register"), AllowAnonymous]
    public Task<IActionResult> Register(RegisterRequest request, CancellationToken cancellationToken) =>
        SendAsync(request.Adapt<RegisterCommand>(), cancellationToken)
            .ToResponseAsync(Ok, HttpContext);
}