using MongoDB.Driver;
using MottuRentalApp.Application.Exceptions;
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
        new Vehicles() { LicensePlate = vehicle.LicensePlate, Year = vehicle.Year, Model = vehicle.Model }
      );

      return vehicle;
    }
    public Vehicle? FindVehicleByPlate(string licensePlate) {
      var filter = Builders<Vehicles>.Filter.Eq(v => v.LicensePlate, licensePlate);
      
      var doc = this._vehicles.Find(filter).FirstOrDefault();
      if (doc == null) {
        return null;
      } else {
        var vehicle = new Vehicle(doc.LicensePlate, doc.Year, doc.Model);

        return vehicle;
      }
    }
    public void RemoveVehicle(string licensePlate) {
      var filter = Builders<Vehicles>.Filter.Eq(v => v.LicensePlate, licensePlate);

      this._vehicles.DeleteOne(filter);
    }
    public Vehicle PatchVehicle(PatchVehicleDto dto) {
      var filter = Builders<Vehicles>.Filter.Eq(v => v.LicensePlate, dto.Identifier);

      if (dto.LicensePlate != null) {
        var vehicle = FindVehicleByPlate(dto.Identifier);
        RemoveVehicle(dto.Identifier);
        vehicle.LicensePlate = dto.LicensePlate;

        return SaveVehicle(vehicle);
      } else {
        this._vehicles.UpdateOne(
          filter,
          Builders<Vehicles>.Update.Set(v => v.Year, dto.Year).Set(v => v.Model, dto.Model)
        );

        return FindVehicleByPlate(dto.Identifier);
      }
    }

    public IList<Vehicle> FetchVehiclesExcept(IList<string> vehicleIds)
    {
      throw new NotImplementedException();
    }
  }
}