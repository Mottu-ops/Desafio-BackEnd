using MottuRentalApp.Domain;

namespace MottuRentalApp.Application.Ports
{
  public interface IVehiclesPort
  {
    public Vehicle saveVehicle(Vehicle vehicle);
    public Vehicle? findVehicleByPlate(string licensePlate);
    public void removeVehicle(string licensePlate);
    public Vehicle patchVehicle(PatchVehicleDto dto);
  }
}
