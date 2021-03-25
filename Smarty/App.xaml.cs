using Smarty.Models;
using Smarty.Services;
using Smarty.Views;
using System;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smarty
{
    public partial class App : Application
    {
        public HttpClient RestClient { get; set; }

        public App()
        {
            InitializeComponent();

            DependencyService.RegisterSingleton(new DataStore<SmartTicket>());
            DependencyService.RegisterSingleton(new RestClient());

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
