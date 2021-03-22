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

        string credit = string.Empty;
        public string Credit
        {
            get { return credit; }
            set { SetProperty(ref credit, value); }
        }

        public AboutViewModel()
        {            
            Title = "About";
            GetTicket = new Command(async () => 
            { 
                var result = await RestClient.Get("http://10.0.2.2:5000/api/smartticket");
                Credit = result.Content.ReadAsStringAsync().Result;
            });
            //GetTicket = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));            
        }

        public ICommand GetTicket { get; }
    }
}

