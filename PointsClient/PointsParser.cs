using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using PointsClient.Interface;
using System.Text;

namespace PointsClient
{
    public class PointsParser : INoteParser
    {
        public List<Point> ParseMany(string pointsAsJson)
        {
            JArray pointsJArray = JArray.Parse(pointsAsJson);

            return pointsJArray.Children().Select(point => ParsePointFromJToken(point)).ToList();
        }

        public string ToJASONMany(List<Point> points)
        {
            if (points == null)
                throw new ArgumentNullException("note", "Can not be null");
            var jarray = new JArray();

            points.ForEach(x =>
            {
                var single = ToJASONSingle(x);
                jarray.Add(single);
            });

            var result =  jarray.ToString();
            return result;
        }

        public Point ParseResponse(string postResponse)
        {
            JObject noteJObject = JObject.Parse(postResponse);

            return ParsePointFromJToken(noteJObject);
        }

        private string ToJASONSingle(Point point)
        {
            if (point == null)
                throw new ArgumentNullException("note", "Can not be null");

            var pointAsJson = new JObject();
            pointAsJson.Add("_id", point.Id);
            pointAsJson.Add("Value", point.Value);
            pointAsJson.Add("Time", point.Time);

            return pointAsJson.ToString();
        }

        private Point ParsePointFromJToken(JToken pointJObject)
        {
            var id = pointJObject.Value<string>("_id");
            var value = pointJObject.Value<double>("Value");
            var time = pointJObject.Value<DateTime>("Time");

            return new Point{Id = id, Value = value, Time = time};
        }
    }
}
