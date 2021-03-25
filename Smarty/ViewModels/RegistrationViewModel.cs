using Smarty.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Smarty.ViewModels
{
    public class RegistrationViewModel : BaseViewModel
    {
        private string _username;
        private string _password;
        private string _email;
        private string _name; 
        private string _surname;
        private bool _isRegistrationButtonVisibile = true;        
        public string Username { get => _username; set => SetProperty(ref _username, value); }
        public string Password { get => _password; set => SetProperty(ref _password, value); }
        public string Email { get => _email; set => SetProperty(ref _email, value); }
        public string Name { get => _name; set => SetProperty(ref _name, value); }
        public string Surname { get => _surname; set => SetProperty(ref _surname, value); }
        public bool IsRegistrationButtonVisibile { get => _isRegistrationButtonVisibile; set => SetProperty(ref _isRegistrationButtonVisibile, value); }
        public Command RegisterCommand => new Command(Register);

        public RegistrationViewModel()
        {
            
        }

        private async void Register()
        {
            IsRegistrationButtonVisibile = false;
            bool authtenticated = await RegisterUser(new Models.UserRegistration()
            {
                Credentials = new Models.UserCredentials() { Username = _username, Password = _password },
                Email = _email,
                Name = _name,
                Surname = _surname
            });
            if (authtenticated)
            {
                // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
                await Shell.Current.GoToAsync($"//{nameof(TicketPage)}");
            }
            else
            {
                IsRegistrationButtonVisibile = true;
            }
        }
    }
}