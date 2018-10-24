using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;

namespace PointsServer.Models
{
    public class Point
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        public double Value { get; set; }
        public DateTime Time { get; set; } 
    }

    public class PointList
    {
        public List<Point> Points { get; set; }
    }
}