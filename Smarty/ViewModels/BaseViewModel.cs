using Smarty.Models;
using Smarty.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Smarty.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IHttpClient RestClient => DependencyService.Get<IHttpClient>();

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        protected async Task<bool> AuthenticateUser(string username, string password)
        {
            bool authenticated = false;
            HttpResponseMessage response = await RestClient.PostJsonAsync("http://10.0.2.2:5000/api/smartticket", new UserCredentials() { Username = username, Password = password });
            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string token = await response.Content.ReadAsStringAsync();
                RestClient.SetOauthToken(token);
                authenticated = true;
            }            
            return authenticated;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
