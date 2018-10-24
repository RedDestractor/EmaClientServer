using System.Collections.Generic;

namespace PointsClient.Interface
{
    public interface INoteParser
    {
        List<Point> ParseMany(string notesAsJson);
        Point ParseResponse(string postResponse);
        string ToJASONMany(List<Point> points);
    }
}