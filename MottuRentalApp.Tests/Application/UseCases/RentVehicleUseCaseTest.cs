using MottuRentalApp.Application.Ports;
using MottuRentalApp.Application.Facades;
using MottuRentalApp.Domain;
using Moq;
using MottuRentalApp.Application.UseCases;
using MottuRentalApp.Application.Exceptions;

namespace MottuRentalApp.Tests.Application.UseCases
{
  public class RentVehicleUseCaseTest
  {
    private readonly Mock<IRentalsPort> _rentalsPort = new Mock<IRentalsPort>();
    private readonly Mock<IRentalVehiclesFacade> _rentalVehiclesFacade = new Mock<IRentalVehiclesFacade>();
    private readonly RentVehicleUseCase _underTest;

    public RentVehicleUseCaseTest()
    {
      this._underTest = new RentVehicleUseCase(this._rentalsPort.Object, this._rentalVehiclesFacade.Object);
    }

    [Fact]
    public void ShouldRentVehicleWhenAvailable()
    {
      string userId = "123";
      string vehicleId = "RHZ2G59";
      string pastEndTerm = "2024-03-26";
      string endTerm = "2024-04-05";
      Rental doneRental = new Rental(userId, vehicleId, pastEndTerm) { StartTerm = DateTime.Parse(pastEndTerm) };
      Rental expectedRental = new Rental(userId, vehicleId, endTerm);

      this._rentalsPort.Setup((port) => port.FindByUser(userId)).Returns(doneRental);
      this._rentalVehiclesFacade.Setup((f) => f.RentAvailableVehicle(userId, endTerm)).Returns(expectedRental);

      this._underTest.Execute(userId, endTerm);
    }

    [Fact]
    public void ShouldThrowWhenUnavailable()
    {
      string userId = "123";
      string vehicleId = "RHZ2G59";
      string pastEndTerm = "2024-03-26";
      string endTerm = "2024-04-05";
      Rental doneRental = new Rental(userId, vehicleId, pastEndTerm);
      Rental expectedRental = new Rental(userId, vehicleId, endTerm);

      this._rentalsPort.Setup((port) => port.FindByUser(userId)).Returns(doneRental);
      this._rentalVehiclesFacade.Setup((f) => f.RentAvailableVehicle(userId, endTerm)).Returns(expectedRental);

      Assert.Throws<InvalidRentException>(() => this._underTest.Execute(userId, endTerm));
    }
  }
}
