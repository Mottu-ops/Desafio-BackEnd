using System;
using MottuRentalApp.Domain;

namespace MottuRentalApp.Application.Ports
{
  public interface IUsersPort
  {
    public User SaveUser(User user);
    public User? FindUser(string Identifier);
    public User PatchUser(User user);
  }
}
