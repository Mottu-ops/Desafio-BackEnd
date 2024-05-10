using Microsoft.AspNetCore.Http;

namespace Job.Domain.Commands.User.Motoboy;

public sealed class UploadCnhMotoboyCommand
{
    public IFormFile FileDetails { get; set; } = default!;
}