using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PointsServer.Interfaces.Mongo
{
    public interface IMongoWrapper<TEntity>
    {
        Task<TEntity> Insert(TEntity entity);
        Task<List<TEntity>> GetAll();
        Task<UpdateResult> Update(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update);
    }
}