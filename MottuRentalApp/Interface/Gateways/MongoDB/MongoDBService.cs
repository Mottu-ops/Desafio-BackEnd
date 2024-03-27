using MongoDB.Driver;
using MottuRentalApp.Interface.Gateways.Interfaces;

namespace MottuRentalApp.Interface.Gateways.MongoDB
{
  public class MongoDBService : IMongoService
  {
    private readonly IConfiguration _configuration;

    public MongoDBService(IConfiguration configuration)
    {
      this._configuration = configuration;
    }

    public IMongoDatabase GetDbConnection() {
      string connString = this._configuration.GetConnectionString("MongoDB") ?? string.Empty;
      var mongoUrl = MongoUrl.Create(connString);
      var client = new MongoClient(mongoUrl);

      return client.GetDatabase(mongoUrl.DatabaseName);
    }
  }
}