using GreenhouseBackend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;


namespace GreenhouseBackend.Services
{
    public class ReadingsService
    {
        private readonly IMongoCollection<Reading> _readingsCollection;
        public ReadingsService(
            IOptions<GreenHouseDatabaseSettings> greenHouseDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                greenHouseDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(
                greenHouseDatabaseSettings.Value.DatabaseName);
            _readingsCollection = mongoDatabase.GetCollection<Reading>(
                greenHouseDatabaseSettings.Value.GreenHouseCollectionName);
        }
        public async Task<List<Reading>> GetAsync() =>
            await _readingsCollection.Find(_ => true).ToListAsync();
        public async Task<Reading?> GetAsync(string id) =>
            await _readingsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        public async Task CreateAsync(Reading newReading) =>
            await _readingsCollection.InsertOneAsync(newReading);
        public async Task UpdateAsync(string id, Reading updatedReading) =>
            await _readingsCollection.ReplaceOneAsync(x => x.Id == id, updatedReading);
        public async Task RemoveAsync(string id) =>
            await _readingsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
