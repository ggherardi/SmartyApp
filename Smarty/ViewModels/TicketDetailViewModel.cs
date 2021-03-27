using Smarty.Models;
using Smarty.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Smarty.ViewModels
{
    [QueryProperty(nameof(TicketId), nameof(TicketId))]
    public class TicketDetailViewModel : BaseViewModel
    {
        private string _ticketId;
        private SmartTicket _selectedTicket;
        public string TicketId { get { return _ticketId; } set { _ticketId = value; GetItemById(value); } }
        public SmartTicket SelectedTicket { get => _selectedTicket; set => SetProperty(ref _selectedTicket, value); }

        public Command UnbindTicketCommand => new Command(UnbindTicket);
        public Command ValidateTicketCommand => new Command(ValidateTicket);
        public Command AddCreditCommand => new Command(async () => await Shell.Current.GoToAsync($"{nameof(AddCreditPage)}?{nameof(AddCreditViewModel.TicketId)}={_ticketId}"));
        public Command DeleteTicketCommand => new Command(DeleteTicket);

        public TicketDetailViewModel() { }        

        public async void GetItemById(string itemId)
        {
            try
            {
                SelectedTicket = await TicketStore.GetItemAsync(itemId);
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
            }
        }

        private async void ValidateTicket()
        {
            bool confirmValidation = await Application.Current.MainPage.DisplayAlert("Conferma obliterazione", @"Vuoi obliterare il biglietto?", "Conferma", "Annulla");
            if (confirmValidation)
            {
                string location = "app_without_geolocation";
                Location geolocation = await Geolocation.GetLocationAsync();
                if(geolocation != null)
                {
                    location = $"{geolocation.Latitude}_{geolocation.Longitude}_{geolocation.Altitude}";
                }
                HttpResponseMessage response = await RestClient.PostJsonAsync("https://10.0.2.2:5001/api/smartticket/validate", new ValidationRegistration() { TicketId = _ticketId, Location = location });
                if (response.IsSuccessStatusCode)
                {
                    await Application.Current.MainPage.DisplayAlert("Successo", "Obliterazione avvenuta con successo", "Ok");
                    await Shell.Current.GoToAsync($"//{nameof(TicketPage)}");
                }
                else
                {
                    DisplayHttpErrorMessage(response);
                }
            }
        }

        private async void DeleteTicket()
        {
            if (!SelectedTicket.Virtual)
            {
                return;
            }
            bool confirmDelete = await Application.Current.MainPage.DisplayAlert("Conferma cancellazione", @"Proseguendo con la cancellazione, eliminerai permanentemente questo biglietto virtuale. Eventuale credito rimanente verrà cancellato assieme al biglietto.", "Conferma", "Annulla");
            if (confirmDelete)
            {
                HttpResponseMessage response = await RestClient.PostJsonAsync("https://10.0.2.2:5001/api/smartticket/deleteticket", _ticketId);
                if(response.IsSuccessStatusCode)
                {
                    await Application.Current.MainPage.DisplayAlert("Successo", "Il biglietto è stato correttamente rimosso dall'account.", "Ok");
                    await Shell.Current.GoToAsync($"//{nameof(TicketPage)}");
                }
                else
                {
                    DisplayHttpErrorMessage(response);
                }
            }
        }

        private async void UnbindTicket()
        {
            if (SelectedTicket.Virtual)
            {
                return;
            }
            bool confirmUnbind = await Application.Current.MainPage.DisplayAlert("Conferma rimozione", @"Proseguendo con questa operazione, il biglietto fisico verrà rimosso dal tuo account ma rimarrà valido per essere utilizzato.", "Conferma", "Annulla");
            if (confirmUnbind)
            {
                HttpResponseMessage response = await RestClient.PostJsonAsync("https://10.0.2.2:5001/api/smartticket/unbindticket", _ticketId);
                if (response.IsSuccessStatusCode)
                {
                    await Application.Current.MainPage.DisplayAlert("Successo", "Il biglietto è stato correttamente rimosso dall'account.", "Ok");
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
