using System;
using PointsClient.Interface;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PointsClient
{
    public class PointsWebClient : INotesWebClient
    {
        public async Task<string> CallRequestGet(Uri queryUri)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(queryUri);
                return GetHttpResponse(response);
            }
        }

        public async Task<string> CallRequestPut(Uri queryUri, string queryBody)
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(queryBody);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await client.PutAsync(queryUri, content);
                return GetHttpResponse(response);
            }
        }

        private string GetHttpResponse(HttpResponseMessage response)
        {
            CheckHttpResponse(response);
            var responseContent = response.Content;
            return responseContent.ReadAsStringAsync().Result;
        }

        private void CheckHttpResponse(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
                return;

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                throw new Exception(response.RequestMessage.RequestUri.ToString());

            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                throw new Exception(response.ReasonPhrase + " during " + response.RequestMessage.Method + " request.");

            throw new Exception(string.Format("Call of {0} was not successful.\nStatusCode: {1}\nReasonPhrase: {2}", response.RequestMessage.RequestUri.AbsolutePath, response.StatusCode, response.ReasonPhrase));
        }
    }
}
