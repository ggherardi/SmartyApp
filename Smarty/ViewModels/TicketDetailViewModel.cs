using Smarty.Models;
using Smarty.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    }
}
