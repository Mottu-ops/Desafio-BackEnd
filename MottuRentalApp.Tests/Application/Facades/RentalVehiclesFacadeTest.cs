using Moq;
using MottuRentalApp.Domain;
using MottuRentalApp.Application.Facades;
using MottuRentalApp.Application.Exceptions;
using MottuRentalApp.Application.Ports;

namespace MottuRentalApp.Tests.Application.Facades
{
  public class RentalVehiclesFacadeTest
  {
    private readonly Mock<IVehiclesPort> _vehiclesPort = new Mock<IVehiclesPort>();
    private readonly Mock<IRentalsPort> _rentalsPort = new Mock<IRentalsPort>();
    private readonly RentalVehiclesFacade _underTest;

    public RentalVehiclesFacadeTest()
    {
      this._underTest = new RentalVehiclesFacade(this._vehiclesPort.Object, this._rentalsPort.Object);
    }

    [Fact]
    public void ShouldCheckAvailableVehicleSuccessfully()
    {
      string validPlate = "RHZ2G39";
      var expectedVehicle = new Vehicle(validPlate, 2023, "");
      this._vehiclesPort.Setup((port) => port.FindVehicleByPlate(validPlate)).Returns(expectedVehicle);
      this._rentalsPort.Setup((port) => port.FindByVehiclePlate(expectedVehicle.LicensePlate)).Returns(() => null);

      var result = this._underTest.IsVehicleAvailable(validPlate);

      Assert.True(result);
    }

    [Fact]
    public void ShouldReturnFalseWhenNotAvailable()
    {
      string validPlate = "RHZ2G39";
      var expectedVehicle = new Vehicle(validPlate, 2023, "");
      this._vehiclesPort.Setup((port) => port.FindVehicleByPlate(validPlate)).Returns(expectedVehicle);
      var expectedRental = new Rental(Guid.NewGuid().ToString(), expectedVehicle.LicensePlate, "2024-03-30");
      this._rentalsPort.Setup((port) => port.FindByVehiclePlate(expectedVehicle.LicensePlate)).Returns(expectedRental);

      var result = this._underTest.IsVehicleAvailable(validPlate);

      Assert.False(result);
    }
  }
}
