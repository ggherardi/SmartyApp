using Smarty.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Smarty.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        public Command LogoutCommand => new Command(async() => await Shell.Current.GoToAsync($"//{nameof(LoginPage)}"));        
    }
}
