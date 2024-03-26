using System;
using MottuRentalApp.Domain;

namespace MottuRentalApp.Application.UseCases
{
  public struct RegisterUserDto
  {
    public RegisterUserDto(string name, string birthDate, int type, IList<Document> documents)
    {
      Name = name;
      BirthDate = birthDate;
      Type = type;
      Documents = documents;
    }

    public string Name { get; }
    public string BirthDate { get; }
    public int Type { get; set; }
    public IList<Document> Documents { get; set; }
  }
}
