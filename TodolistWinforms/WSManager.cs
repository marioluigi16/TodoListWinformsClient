using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TodolistWinforms
{
    public class WSManager
    {
        public async Task<T> HTTPGet<T>(string uri)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(uri);
                    response.EnsureSuccessStatusCode();

                    string json = Encoding.UTF8.GetString(response.Content.ReadAsByteArrayAsync().Result);
                    return JsonConvert.DeserializeObject<T>(json);
                }
            }
            catch(Exception ex)
            {
                return default;
            }
        }

        public async Task<bool> HTTPPost<T>(string uri, T obj) where T: class
        {
            return await SendHTTPRequest(uri, obj, HttpMethod.Post);
        }

        public async Task<bool> HTTPPut<T>(string uri, T obj) where T: class
        {
            return await SendHTTPRequest(uri, obj, HttpMethod.Put);
        }

        public async Task<bool> HTTPDelete<T>(string uri, T obj) where T : class
        {
            return await SendHTTPRequest(uri, obj, HttpMethod.Delete);
        }

        private async Task<bool> SendHTTPRequest<T>(string uri, T obj, HttpMethod httpMethod) where T: class
        {
            try
            {
                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage(httpMethod, uri))
                {
                    string json = JsonConvert.SerializeObject(obj);
                    using (var stringContent = new StringContent(json, Encoding.UTF8, "application/json"))
                    {
                        request.Content = stringContent;

                        using (var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false))
                        {
                            response.EnsureSuccessStatusCode();

                            return response.IsSuccessStatusCode;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
