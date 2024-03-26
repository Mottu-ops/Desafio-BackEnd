using MottuRentalApp.Domain;

namespace MottuRentalApp.Application.Ports
{
  public interface IRentalsPort
  {
    public Rental startRental(Rental rental);
    public Rental? findByUser(string userId);
    public Rental? findByVehicleId(string vehicleId);
  }
}