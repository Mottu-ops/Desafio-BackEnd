using MottuRentalApp.Domain;

namespace MottuRentalApp.Application.Ports
{
  public interface IVehiclesPort
  {
    public Vehicle SaveVehicle(Vehicle vehicle);
    public Vehicle? FindVehicleByPlate(string licensePlate);
    public void RemoveVehicle(string licensePlate);
    public Vehicle PatchVehicle(PatchVehicleDto dto);
    public IList<Vehicle> FetchVehiclesExcept(IList<string> vehicleIds);
  }
}
