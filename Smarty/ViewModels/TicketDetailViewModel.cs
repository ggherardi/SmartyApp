using Smarty.Models;
using Smarty.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
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

        private async void DeleteTicket()
        {
            if (!SelectedTicket.Virtual)
            {
                return;
            }
            bool confirmDelete = await Application.Current.MainPage.DisplayAlert("Conferma cancellazione", @"Proseguendo con la cancellazione, eliminerai dal tuo account questo biglietto virtuale. Per recuperare il credito presente sul biglietto, dovrai inviare una richiesta all'assistenza", "Conferma", "Annulla");
            if (confirmDelete)
            {
                HttpResponseMessage response = await RestClient.PostJsonAsync("https://10.0.2.2:5001/api/smartticket/deactivateticket", _ticketId);
                if(response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    await Application.Current.MainPage.DisplayAlert("Successo", "Il biglietto è stato correttamente rimosso dall'account.", "Ok");
                    await Shell.Current.GoToAsync($"//{nameof(TicketPage)}");
                }
            }
        }
    }
}
