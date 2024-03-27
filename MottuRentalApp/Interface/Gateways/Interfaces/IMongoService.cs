using MongoDB.Driver;

namespace MottuRentalApp.Interface.Gateways.Interfaces
{
  public interface IMongoService
  {
    public IMongoDatabase GetDbConnection();
  }
}
