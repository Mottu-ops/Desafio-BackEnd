using Microsoft.Extensions.DependencyInjection;

namespace Motorent.Presentation.Common.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public abstract class ApiController : ControllerBase
{
    private ISender? sender;

    private ISender Sender => sender ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    protected Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request,
        CancellationToken cancellationToken = default) => Sender.Send(request, cancellationToken);
}