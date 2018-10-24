using MongoDB.Bson;
using MongoDB.Driver;
using PointsServer.Interfaces.Mongo;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PointsServer.MongoDb
{
    public class MongoWrapper<TEntity> : IMongoWrapper<TEntity>
    {
        private readonly IMongoCollection<TEntity> _collection;

        public MongoWrapper(IMongoCollection<TEntity> collection)
        {
            if (collection != null)
                _collection = collection;
            else throw new ArgumentNullException("collection");
        }

        public async Task<TEntity> Insert(TEntity entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task<List<TEntity>> GetAll()
        {
            var filter = Builders<TEntity>.Filter.Empty;
            return await _collection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<UpdateResult> Update(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update)
        {
            return await _collection.UpdateOneAsync(filter, update);
        }

    }
}