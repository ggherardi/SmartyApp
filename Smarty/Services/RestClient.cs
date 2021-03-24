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
            _httpClient.Timeout = new TimeSpan(0, 0, 5);
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

        public Task<HttpResponseMessage> PostJsonAsync(string requestUrl, object bodyContent)
        {
            string contentAsString = string.Empty;
            if(bodyContent.GetType() != typeof(string))
            {
                contentAsString = JsonSerializer.Serialize(bodyContent);
            }
            StringContent content = new StringContent(contentAsString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return _httpClient.PostAsync(requestUrl, content);
        }

        public async Task<HttpResponseMessage> GetAsync(string requestUrl)
        {            
            HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);
            Uri finalRequestUri = response.RequestMessage.RequestUri;
            if (finalRequestUri != new Uri(requestUrl))
            {
                if (IsHostTrusted(finalRequestUri))
                {
                    response = _httpClient.GetAsync(finalRequestUri).Result;
                }
            }
            return response;
        }

        private bool IsHostTrusted(Uri uri)
        {
            return uri.Host == "10.0.2.2";
        }
    }
}
