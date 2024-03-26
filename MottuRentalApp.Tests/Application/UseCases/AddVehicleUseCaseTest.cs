using MottuRentalApp.Application.Ports;
using Moq;
using MottuRentalApp.Domain;
using MottuRentalApp.Application.UseCases;
using MottuRentalApp.Application.Exceptions;

namespace MottuRentalApp.Tests.Application.UseCases
{
  public class AddVehicleUseCaseTest
  {
    private readonly Mock<IVehiclesPort> _vehiclesPort = new Mock<IVehiclesPort>();
    private readonly AddVehicleUseCase _underTest;
    private static readonly string VALID_PLATE = "RHZ4J20";
    private static readonly int VALID_YEAR = 2023;
    private static readonly string VALID_MODEL = "E-Mottu";

    public AddVehicleUseCaseTest()
    {
      this._underTest = new AddVehicleUseCase(this._vehiclesPort.Object);
    }

    [Fact]
    public void ShouldExecuteSuccessfully()
    {
      Vehicle expectedVehicle = new Vehicle(VALID_PLATE, VALID_YEAR, VALID_MODEL);
      this._vehiclesPort.Setup(port => port.FindVehicleByPlate(expectedVehicle.LicensePlate)).Returns(() => null);
      this._vehiclesPort.Setup(port => port.SaveVehicle(expectedVehicle)).Returns(expectedVehicle);

      var result = this._underTest.Execute(expectedVehicle);

      Assert.NotNull(result);
    }

    [Fact]
    public void ShouldThrowWhenLicensePlateAlreadyExists()
    {
      Vehicle expectedVehicle = new Vehicle(VALID_PLATE, VALID_YEAR, VALID_MODEL);
      this._vehiclesPort.Setup(port => port.FindVehicleByPlate(expectedVehicle.LicensePlate))
        .Returns(() => expectedVehicle);
      this._vehiclesPort.Setup(port => port.SaveVehicle(expectedVehicle));

      Assert.Throws<InvalidVehicleException>(() => this._underTest.Execute(expectedVehicle));
    }
  }
}