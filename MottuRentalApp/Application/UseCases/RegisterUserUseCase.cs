using System;
using MottuRentalApp.Domain;
using MottuRentalApp.Application.Ports;

namespace MottuRentalApp.Application.UseCases
{
  public class RegisterUserUseCase(IUsersPort usersPort)
  {
    private readonly IUsersPort _usersPort = usersPort;

    public User Execute(RegisterUserDto dto)
    {
      checkDto(dto);

      return this._usersPort.saveUser(
        new User(dto.DocumentNumber, dto.DocumentType, dto.Name, dto.BirthDate, (UserType) dto.Type)
      );      
    }

    private void checkDto(RegisterUserDto dto)
    {
      if (
          String.IsNullOrEmpty(dto.Name) ||
            String.IsNullOrEmpty(dto.BirthDate) ||
              String.IsNullOrEmpty(dto.DocumentNumber) ||
                String.IsNullOrEmpty(dto.DocumentType)
        )
      {
        throw new ArgumentException("BAD_PARAMS");
      }

      if (dto.Type < 1 || dto.Type > 2)
      {
        dto.Type = 2;
      }
    }
  }
}
