using MongoDB.Driver;
using PointsServer.Models;
using System.Linq;

namespace PointsServer.Interfaces.Mongo
{
    public interface IMongoAdapter
    {
        IMongoWrapper<Point> GetWrapper();
    }
}