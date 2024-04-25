using System.ComponentModel.DataAnnotations;

namespace Rent.API.ViewModels;

public class ResultViewModel
{
    [Required]
    public string Message { get; set; }
    [Required]
    public bool Success { get; set; }
    [Required]
    public dynamic Data { get; set; }
}