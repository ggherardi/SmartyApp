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
        private string _username;
        private string _password;
        private bool _isLoginButtonVisibile = true;
        public string Username { get => _username; set => SetProperty(ref _username, value); }
        public string Password { get => _password; set => SetProperty(ref _password, value); }
        public bool IsLoginButtonVisibile { get => _isLoginButtonVisibile; set => SetProperty(ref _isLoginButtonVisibile, value); }

        public Command LoginCommand => new Command(Login);
        public Command GoToRegisterCommand => new Command(async () => { await Shell.Current.GoToAsync(nameof(RegistrationPage)); });

        public LoginViewModel()
        {
        }        

        public async void Login()
        {
            IsLoginButtonVisibile = false;
            bool authtenticated = await AuthenticateUser(new Models.UserCredentials() { Username = _username, Password = _password });
            if (authtenticated)
            {
                // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
                await Shell.Current.GoToAsync($"//{nameof(TicketPage)}");
            }
            else
            {
                IsLoginButtonVisibile = true;
            }
        }
    }
}
