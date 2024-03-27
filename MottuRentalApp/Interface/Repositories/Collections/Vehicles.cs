using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MottuRentalApp.Interface.Repositories.Collections
{
  public class Vehicles
  {
    [BsonId]
    [BsonElement("LicensePlate"), BsonRepresentation(BsonType.String)]
    public string? LicensePlate { get; set; }
    [BsonElement("Year"), BsonRepresentation(BsonType.Int32)]
    public int? Year { get; set; }
    [BsonElement("Model"), BsonRepresentation(BsonType.String)]
    public string? Model { get; set; }
  }
}
