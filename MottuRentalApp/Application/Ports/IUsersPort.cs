using System;
using MottuRentalApp.Domain;

namespace MottuRentalApp.Application.Ports
{
  public interface IUsersPort
  {
    public User saveUser(User user);
  }
}
