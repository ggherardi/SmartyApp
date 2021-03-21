using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Smarty.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        HttpClient _client;
        public AboutViewModel()
        {
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };
            _client = new HttpClient(httpClientHandler);

            Title = "About";            
            //OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
            OpenWebCommand = new Command(async () => 
            {
                HttpRequestMessage me = new HttpRequestMessage();
                StringContent content = new StringContent(JsonSerializer.Serialize(new UserCredentials() { Username = "ggherardi", Password = "gg" }));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var result = await _client.PostAsync("http://10.0.2.2:5000/api/smartticket", content);
                var a = result.Content;
            }
            );
        }

        public ICommand OpenWebCommand { get; }
    }

    public class UserCredentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}

