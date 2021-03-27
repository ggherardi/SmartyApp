using Smarty.Models;
using Smarty.Views;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace Smarty.ViewModels
{
    [QueryProperty(nameof(TicketId), nameof(TicketId))]
    public class AddCreditViewModel : BaseViewModel
    {
        private string _ticketId;
        private decimal _amount;
        public string TicketId { get => _ticketId; set => _ticketId = value; }
        public decimal Amount { get => _amount; set => SetProperty(ref _amount, value); }

        public Command SelectCredit => new Command((object value) => { Amount = decimal.Parse(value as string); });
        public Command AddCreditCommand => new Command(AddCredit);

        private async void AddCredit()
        {
            HttpResponseMessage response = await RestClient.PostJsonAsync("https://10.0.2.2:5001/api/smartticket/addcredit", new CreditRecharge() { TicketId = _ticketId, Amount = Amount });
            if(response.IsSuccessStatusCode)
            {
                await Application.Current.MainPage.DisplayAlert("Operazione completata", $"Hai aggiunto {Amount}€ al credito del tuo biglietto virtuale {_ticketId}", "Ok");
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
