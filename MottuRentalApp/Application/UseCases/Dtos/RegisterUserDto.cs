using System;
using MottuRentalApp.Domain;

namespace MottuRentalApp.Application.UseCases
{
  public struct RegisterUserDto
  {
    public RegisterUserDto(string name, string birthDate, int type, string documentNumber, string documentType)
    {
      Name = name;
      BirthDate = birthDate;
      Type = type;
      DocumentNumber = documentNumber;
      DocumentType = documentType;
    }

    public string Name { get; }
    public string BirthDate { get; }
    public int Type { get; set; }
    public string DocumentNumber { get; }
    public string DocumentType { get; }
  }
}