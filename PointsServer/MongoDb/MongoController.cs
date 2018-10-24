using MongoDB.Driver;
using PointsServer.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using PointsServer.Interfaces.Mongo;
using System.Linq;
using MongoDB.Bson;
using System;

namespace PointsServer.MongoDb
{
    public class MongoController
    {
        private IMongoAdapter _adapter;

        public MongoController(IMongoAdapter adapter)
        {
            _adapter = adapter;
        }

        public async Task<Point> InsertPoint(Point point)
        {
            return await _adapter.GetWrapper().Insert(point);
        }

        public async Task<List<Point>> GetAll()
        {
            return await _adapter.GetWrapper().GetAll();
        }

        public async Task<UpdateResult> UpdatePoints(List<Point> points)
        {
            UpdateResult result = null;

            foreach(var point in points)
            {
                var filter = Builders<Point>.Filter.Eq("_id", point._id);
                var update = Builders<Point>.Update.Set("Value", point.Value);
                result = await _adapter.GetWrapper().Update(filter, update);
            }
            return result;
        }
    }
}