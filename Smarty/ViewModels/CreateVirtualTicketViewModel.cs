using Smarty.Utilities;
using Smarty.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace Smarty.ViewModels
{
    public class CreateVirtualTicketViewModel : BaseViewModel
    {
        public Command CreateVirtualTicketCommand => new Command(CreateAndStoreVirtualTicket);

        public CreateVirtualTicketViewModel() { }

        private async void CreateAndStoreVirtualTicket()
        {
            bool confirm = await Application.Current.MainPage.DisplayAlert("Conferma creazione", $"Confermi la creazione di un biglietto virtuale?", "Conferma", "Annulla");
            if (confirm)
            {
                HttpResponseMessage response = await RestClient.PostAsync("https://10.0.2.2:5001/api/smartticket/createvirtualticket");
                if (response.IsSuccessStatusCode)
                {
                    await Xamarin.Essentials.SecureStorage.SetAsync("VirtualTicket", response.Content.ReadAsStringAsync().Result);
                    await Shell.Current.GoToAsync($"//{nameof(TicketPage)}");
                }
                else
                {
                    DisplayHttpErrorMessage(response);
                }
            }            
        }
    }

    
}
