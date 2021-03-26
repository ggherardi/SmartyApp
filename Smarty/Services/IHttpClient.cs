using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Smarty.Services
{
    public interface IHttpClient
    {
        HttpClient GetClient();
        Task<HttpResponseMessage> PostAsync(string requestUrl);
        Task<HttpResponseMessage> PostAsync(string requestUrl, string content);
        Task<HttpResponseMessage> PostJsonAsync(string requestUrl, object bodyContent);
        Task<HttpResponseMessage> GetAsync(string requestUrl);
        void SetOauthToken(string token);        
    }
}
