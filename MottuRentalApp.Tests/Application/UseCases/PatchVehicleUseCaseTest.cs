using Moq;
using MottuRentalApp.Application.Ports;
using MottuRentalApp.Application.UseCases;
using MottuRentalApp.Application.Exceptions;
using MottuRentalApp.Domain;

namespace MottuRentalApp.Tests.Application.UseCases
{
  public class PatchVehicleUseCaseTest
  {
    private readonly Mock<IVehiclesPort> _vehiclePort = new Mock<IVehiclesPort>();
    private readonly PatchVehicleUseCase _underTest;
    public PatchVehicleUseCaseTest()
    {
      this._underTest = new PatchVehicleUseCase(this._vehiclePort.Object);
    }

    [Fact]
    public void ShouldPatchWhenValidDto()
    {
      PatchVehicleDto expectedDto = new PatchVehicleDto(Guid.NewGuid().ToString(), "RFH2G09", null, null);
      Vehicle expectedVehicle = new Vehicle(expectedDto.LicensePlate ?? "", expectedDto.Year ?? 2023, expectedDto.Model ?? "");
      this._vehiclePort.Setup((port) => port.PatchVehicle(expectedDto)).Returns(expectedVehicle);

      var result = this._underTest.Execute(expectedDto);

      Assert.NotNull(result);
    }

    [Fact]
    public void ShouldThrowWhenINvalidDto()
    {
      PatchVehicleDto expectedDto = new PatchVehicleDto(Guid.NewGuid().ToString(), " ", null, null);
      this._vehiclePort.Setup((port) => port.PatchVehicle(expectedDto)).Returns(It.IsAny<Vehicle>());

      Assert.Throws<InvalidVehicleException>(() => this._underTest.Execute(expectedDto));
    }
  }
}