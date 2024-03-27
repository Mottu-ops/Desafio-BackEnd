using Moq;
using MottuRentalApp.Domain;
using MottuRentalApp.Interface.Gateways.Interfaces;
using MottuRentalApp.Interface.Repositories;
using MottuRentalApp.Interface.Repositories.Collections;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

namespace MottuRentalApp.Tests.Interface.Repositories
{
  public class VehiclesRepositoryTest
  {
    private readonly Mock<IMongoService> _mongoDBService = new();
    private readonly Mock<IMongoCollection<Vehicles>> _collection = new();
    private readonly VehiclesRepository _underTest;

    public VehiclesRepositoryTest()
    {
      this._mongoDBService.Setup((svc) => svc.GetDbConnection().GetCollection<Vehicles>("vehicles", null))
        .Returns(this._collection.Object);
      this._underTest = new VehiclesRepository(this._mongoDBService.Object);
    }

    [Fact]
    public void ShouldSaveVehicleSuccessfully()
    {
      var expectedVehicle = BuildValidVehicle();
      var expectedDocument = BuildDocument(expectedVehicle);
      this._collection.Setup((coll) => coll.InsertOne(expectedDocument, null , default(CancellationToken)));

      var result = this._underTest.SaveVehicle(expectedVehicle);

      Assert.NotNull(result);
    }

    private Vehicle BuildValidVehicle()
    {
      return new Vehicle("RHZ2G59", 2024, "");
    }

    private Vehicles BuildDocument(Vehicle vehicle)
    {
      return new Vehicles() { Identifier = vehicle.Identifier, LicensePlate = vehicle.LicensePlate, Year = vehicle.Year, Model = vehicle.Model };
    }
  }
}