using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using PointsServer.Interfaces.Mongo;
using PointsServer.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PointsServer.MongoDb
{
    public class MongoAdapter : IMongoAdapter
    {
        private IMongoClient _client;
        private IMongoDatabase _database;
        private IMongoCollection<Point> _collection;
        private IMongoWrapper<Point> _mongoWrapper;

        public MongoAdapter()
        {
            _client = new MongoClient();
            _database = _client.GetDatabase("SinusDatabase");
            _collection = _database.GetCollection<Point>("Points");
            _mongoWrapper = new MongoWrapper<Point>(_collection);
        }

        public IMongoWrapper<Point> GetWrapper()
        {
            return _mongoWrapper;
        }
    }
}