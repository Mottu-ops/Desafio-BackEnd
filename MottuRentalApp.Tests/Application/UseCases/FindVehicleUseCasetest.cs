using Moq;
using MottuRentalApp.Application.Exceptions;
using MottuRentalApp.Application.Ports;
using MottuRentalApp.Application.UseCases;
using MottuRentalApp.Domain;

namespace MottuRentalApp.Tests.Application.UseCases
{
  public class FindVehicleUseCaseTest
  {
    private readonly Mock<IVehiclesPort> _vehiuclesPort = new Mock<IVehiclesPort>();
    private readonly FindVehicleUseCase _underTest;

    public FindVehicleUseCaseTest()
    {
      this._underTest = new FindVehicleUseCase(this._vehiuclesPort.Object);
    }

    [Fact]
    public void ShouldFindVehicleWhenValidPlate()
    {
      string validPlate = "RHZ2G39";
      Vehicle expectedVehicle = new Vehicle(validPlate, 2023, String.Empty);
      this._vehiuclesPort.Setup((port) => port.FindVehicleByPlate(validPlate)).Returns(expectedVehicle);

      var result = this._underTest.Execute(validPlate);

      Assert.NotNull(result);
    }

    [Fact]
    public void ShouldThrowWhenInvalidPlate()
    {
      string invalidPlate = " ";
      this._vehiuclesPort.Setup((port) => port.FindVehicleByPlate(invalidPlate)).Returns(It.IsAny<Vehicle>());

      Assert.Throws<InvalidVehicleException>(() => this._underTest.Execute(invalidPlate));
    }
  }
}