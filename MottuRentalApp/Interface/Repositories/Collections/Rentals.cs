using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MottuRentalApp.Interface.Repositories.Collections
{
  public class Rentals
  {
    [BsonId]
    [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    [BsonElement("identifier"), BsonRepresentation(BsonType.String)]
    public string? Identifier { get; set; }
    [BsonElement("userId"), BsonRepresentation(BsonType.String)]
    public string? UserId { get; set; }
    [BsonElement("vehicleId"), BsonRepresentation(BsonType.String)]
    public string? VehicleId { get; set; }
    [BsonElement("startTerm"), BsonRepresentation(BsonType.DateTime)]
    public DateTime? StartTerm { get; set; }
    [BsonElement("endTerm"), BsonRepresentation(BsonType.DateTime)]
    public DateTime? EndTerm { get; set; }
    [BsonElement("status"), BsonRepresentation(BsonType.String)]
    public string? Status { get; set; }
    [BsonElement("totalFare"), BsonRepresentation(BsonType.Decimal128)]
    public decimal? TotalFare { get; set; }
  }
}
