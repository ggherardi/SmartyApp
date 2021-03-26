using Smarty.Views;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace Smarty.ViewModels
{
    public class BindPhysicalTicketViewModel : BaseViewModel
    {
        private string _sampleTickedId = "04-15-91-BA-9F-FD-20";
        public Command BindTicketCommand => new Command(BindTicket);

        private async void BindTicket()
        {
            bool confirm = await Application.Current.MainPage.DisplayAlert("Conferma associazione", $"Il biglietto {_sampleTickedId} verrà associato al tuo account", "Conferma", "Annulla");
            if (confirm)
            {
                HttpResponseMessage response = await RestClient.PostJsonAsync("https://10.0.2.2:5001/api/smartticket/associateticket", _sampleTickedId);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    await Application.Current.MainPage.DisplayAlert("Successo", $"Il biglietto {_sampleTickedId} è stato correttamente associato al tuo account", "Ok");
                    await Shell.Current.GoToAsync($"//{nameof(TicketPage)}");
                }
            }
        }
    }
}
