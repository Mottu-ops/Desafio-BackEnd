using MongoDB.Driver;
using MottuRentalApp.Application.Ports;
using MottuRentalApp.Domain;
using MottuRentalApp.Interface.Gateways.Interfaces;
using MottuRentalApp.Interface.Repositories.Collections;

namespace MottuRentalApp.Interface.Repositories
{
  public class VehiclesRepository : IVehiclesPort
  {
    private readonly IMongoService _mongoDBService;
    private readonly IMongoCollection<Vehicles> _vehicles;

    public VehiclesRepository(IMongoService mongoDBService)
    {
      this._mongoDBService = mongoDBService;
      this._vehicles = this._mongoDBService.GetDbConnection().GetCollection<Vehicles>("vehicles") ??
        throw new IOException("BAD_DATABASE_CONNECTION");
    }

    public Vehicle SaveVehicle(Vehicle vehicle) {
      this._vehicles.InsertOne(
        new Vehicles() { Identifier = vehicle.Identifier, LicensePlate = vehicle.LicensePlate, Year = vehicle.Year, Model = vehicle.Model }
      );

      return vehicle;
    }
    public Vehicle? FindVehicleByPlate(string licensePlate) {
      // var filter = Builders<Vehicles>.Filter.Eq(v => v.LicensePlate, licensePlate);
      
      // var doc = this._vehicles.Find(filter).FirstOrDefault();

      // return doc is not null ? new Vehicle() : null;

      throw new NotImplementedException();
    }
    public void RemoveVehicle(string licensePlate) {
      throw new NotImplementedException();
    }
    public Vehicle PatchVehicle(PatchVehicleDto dto) {
      throw new NotImplementedException();
    }
  }
}