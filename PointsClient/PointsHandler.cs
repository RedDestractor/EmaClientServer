 using PointsClient.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace PointsClient
{
    public class PointsHandler : IPointsHandler
    {
        public const string NoDataMessage = "Must have data to update";

        private INotesWebClient _webClient;
        private INoteParser _parser;
        private readonly Uri _localHostUri = new Uri("http://localhost:61029/api/points");

        public PointsHandler(INotesWebClient webclient, INoteParser parser)
        {
            _webClient = webclient;
            _parser = parser;
        }

        public async Task<List<Point>> GetAllPoints()
        {
            var pointsAsJson = await _webClient.CallRequestGet(_localHostUri);

            if (string.IsNullOrWhiteSpace(pointsAsJson))
                return null;

            return _parser.ParseMany(pointsAsJson);
        }

        public async Task<bool> SavePoints(List<Point> points)
        {
            if (points.Count == 0)
                throw new ArgumentNullException("points", NoDataMessage);

            var pointsAsJson = _parser.ToJASONMany(points);
            var response = await _webClient.CallRequestPut(_localHostUri, pointsAsJson);

            if (string.IsNullOrWhiteSpace(response))
                return false;

            return true;
        }
    }
}
