using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Smarty.Services
{
    public class RestClient : IHttpClient
    {
        private readonly HttpClient _httpClient;
        private string _jwtToken;

        public RestClient()
        {
            var httpClientHandler = new HttpClientHandler() { PreAuthenticate = true };
            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };
            _httpClient = new HttpClient(httpClientHandler);
        }

        public HttpClient GetClient()
        {
            return _httpClient;
        }

        public void SetOauthToken(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);            
            _jwtToken = token;
        }

        public string GetToken()
        {
            return _jwtToken;
        }

        public Task<HttpResponseMessage> PostJsonAsync(string requestUrl, object bodyContent)
        {
            StringContent content = new StringContent(JsonSerializer.Serialize(bodyContent));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return _httpClient.PostAsync(requestUrl, content);
        }

        public async Task<HttpResponseMessage> Get(string requestUrl)
        {
            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtToken);                        
            var response_1 = await _httpClient.GetAsync(requestUrl);
            var b = response_1.Content;
            var httpClientHandler = new HttpClientHandler() { };
            HttpClient client = new HttpClient(httpClientHandler);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtToken);
            var response = await client.GetAsync(requestUrl);
            var a = response.Content;
            return response;
        }
    }
}
