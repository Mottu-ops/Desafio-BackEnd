using MongoDB.Bson;
using MongoDB.Driver;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Entities.Mongo;
using MotorcycleRental.Domain.Interfaces.Repositories.MongoDb;

namespace MotorcycleRental.Infraestructure.Repositories.MongoDb
{
    public class MotorcycleMongoRepository : IMotorcycleMongoRepository
    {
        private readonly IMongoCollection<MotorcycleMgDb> _motorcycleCollection;
        public MotorcycleMongoRepository(IMongoDatabase mongoDatabase)
        {
            _motorcycleCollection = mongoDatabase.GetCollection<MotorcycleMgDb>("Motorcycles");
        }
        public async Task AddAsync(MotorcycleMgDb entity)
        {
            await _motorcycleCollection.InsertOneAsync(entity);
        }

        public async Task<IEnumerable<MotorcycleMgDb>> GetAllAsync()
        {
            var result = await _motorcycleCollection.FindAsync(new BsonDocument());
            return result.ToList();
        }

        public async Task<MotorcycleMgDb> GetByIdAsync(Guid id)
        {
            MotorcycleMgDb result = await _motorcycleCollection.Find(m => m.Id == id && m.IsActived == true).SingleOrDefaultAsync();
            return result;
        }

        public async Task UpdateAsync(MotorcycleMgDb entity)
        {
            await _motorcycleCollection.ReplaceOneAsync(m => m.Id == entity.Id, entity);
        }
    }
}
