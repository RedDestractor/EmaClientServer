using System;
using System.Threading.Tasks;

namespace PointsClient.Interface
{
    public interface INotesWebClient
    {
        Task<string> CallRequestGet(Uri queryUri);
        Task<string> CallRequestPut(Uri queryUri, string queryBody);
    }
}