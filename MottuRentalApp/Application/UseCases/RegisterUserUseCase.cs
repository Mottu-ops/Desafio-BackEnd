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
      CheckDto(dto);

      return this._usersPort.SaveUser(
        new User(dto.Name, dto.BirthDate, (UserType) dto.Type, dto.Documents)
      );
    }

    private void CheckDto(RegisterUserDto dto)
    {
      if (
          String.IsNullOrEmpty(dto.Name) ||
            String.IsNullOrEmpty(dto.BirthDate) ||
              dto.Documents.Count < 1
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
