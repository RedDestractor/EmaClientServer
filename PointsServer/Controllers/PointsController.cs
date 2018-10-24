using System.Collections.Generic;
using System.Web.Http;
using PointsServer.Models;
using PointsServer.MongoDb;
using System.Threading.Tasks;
using System.Web.Http.Results;
using MongoDB.Driver;
using System;
using System.IO;
using System.Text;
using MongoDB.Bson.Serialization;
using Newtonsoft.Json;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;
using System.Linq;

namespace PointsServer.Controllers
{
    public class PointsController : ApiController
    {
        [HttpGet]
        public async Task<JsonResult<List<Point>>> Get()
        {
            var dbController = new MongoController(new MongoAdapter());
            var allPoints = await dbController.GetAll();
            return Json(allPoints);
        }

        public async Task<UpdateResult> Put()
        {
            var content = Request.Content.ReadAsStringAsync().Result;
            List<Point> points = null;
            try
            {
                var pointsBson = BsonSerializer.Deserialize<BsonArray>(content).ToList();
                points = pointsBson.Select(x => BsonSerializer.Deserialize<Point>(x.ToString())).ToList();
            }
            catch (Exception ex)
            {
            }
            var dbController = new MongoController(new MongoAdapter());
            var result = await dbController.UpdatePoints(points);
            return result;
        }
    }
}
