using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PointsClient.Interface
{
    public interface IPointsHandler
    {
        Task<List<Point>> GetAllPoints();
        Task<bool> SavePoints(List<Point> points);
    }
}
