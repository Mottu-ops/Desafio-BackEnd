using MottuRentalApp.Domain;

namespace MottuRentalApp.Application.Ports
{
  public interface IRentalsPort
  {
    public Rental StartRental(Rental rental);
    public Rental? FindByUser(string userId);
    public Rental? FindByVehiclePlate(string licensePlate);
    public IList<RentalPeriod> FetchPeriods();
    public IList<Rental> FetchOngoing();
  }
}
