using System.ComponentModel.DataAnnotations;

namespace User.API.ViewModels;

public class UpdateUserViewModel
{

    public long Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Cnpj { get; set; }
    public DateTime DateBirth { get; set; }
    public string CnhNumber { get; set; }
    public string CnhType { get; set; }
    public string CnhImage { get; set; }
    public string Role { get; set; }
}