using Moq;
using MottuRentalApp.Application.Ports;
using MottuRentalApp.Application.Facades;
using MottuRentalApp.Application.UseCases;
using MottuRentalApp.Application.Exceptions;

namespace MottuRentalApp.Tests.Application.UseCases
{
  public class RemoveVehicleUseCaseTest
  {
    private readonly Mock<IVehiclesPort> _vehiclesPort = new Mock<IVehiclesPort>();
    private readonly Mock<IRentalsPort> _rentalsPort = new Mock<IRentalsPort>();
    private readonly Mock<IRentalVehiclesFacade> _rentalVehiclesFacade= new Mock<IRentalVehiclesFacade>();
    private readonly RemoveVehicleUseCase _underTest;

    public RemoveVehicleUseCaseTest()
    {
      this._underTest = new RemoveVehicleUseCase(this._vehiclesPort.Object, this._rentalVehiclesFacade.Object);
    }

    [Fact]
    public void ShouldRemoveVehicleSuccessfully()
    {
      string validPlate = "RHZ2G39";
      this._rentalVehiclesFacade.Setup((port) => port.IsVehicleAvailable(validPlate)).Returns(true);
      this._vehiclesPort.Setup((port) => port.RemoveVehicle(validPlate));

      this._underTest.Execute(validPlate);
    }

    [Fact]
    public void ShouldThrowWhenVehicleNotAvailable()
    {
      string validPlate = "RHZ2G39";
      this._rentalVehiclesFacade.Setup((port) => port.IsVehicleAvailable(validPlate)).Returns(false);
      this._vehiclesPort.Setup((port) => port.RemoveVehicle(validPlate));

      Assert.Throws<UnavailableVehicleException>(() => this._underTest.Execute(validPlate));
    }
  }
}
