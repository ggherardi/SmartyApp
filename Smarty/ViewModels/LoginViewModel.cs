using Smarty.Views;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace Smarty.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }

        public LoginViewModel()
        {            
            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            HttpResponseMessage response = await RestClient.PostJsonAsync("http://10.0.2.2:5000/api/smartticket", new UserCredentials() { Username = "ggherardi", Password = "gg" });
            string token = await response.Content.ReadAsStringAsync();
            RestClient.SetOauthToken(token);
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        }
    }
}
