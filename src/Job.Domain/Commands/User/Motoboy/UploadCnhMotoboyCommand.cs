using Microsoft.AspNetCore.Http;

namespace Job.Domain.Commands.User.Motoboy;

public class UploadCnhMotoboyCommand
{
    public IFormFile FileDetails { get; set; } = default!;
}