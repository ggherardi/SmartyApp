using Newtonsoft.Json;
using Smarty.Models;
using Smarty.Utilities;
using Smarty.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Smarty.ViewModels
{
    public class TicketViewModel : BaseViewModel
    {
        private ObservableCollection<SmartTicket> _tickets;
        private bool _canAddVirtualTicket = false;
        private bool _canAssociateTicket = false;
        public ObservableCollection<SmartTicket> Tickets { get => _tickets; set => SetProperty(ref _tickets, value); }
        public bool CanAddVirtualCard { get => _canAddVirtualTicket; set => SetProperty(ref _canAddVirtualTicket, value); }
        public bool CanAssociateTicket { get => _canAssociateTicket; set => SetProperty(ref _canAssociateTicket, value); }
        public Command GetTickets => new Command(GetTicketsAsync);
        public Command<SmartTicket> ItemTapped => new Command<SmartTicket>(OnItemSelected);
        public Command GoToAssociatePhysicalTicketCommand => new Command(async () => await Shell.Current.GoToAsync($"{nameof(AssociatePhysicalTicketPage)}"));
        public Command GoToCreateVirtualTicketCommand => new Command(async () => await Shell.Current.GoToAsync($"{nameof(CreateVirtualTicketPage)}"));

        public TicketViewModel()
        {
            GetTicketsAsync();
        }                

        public void OnAppearing()
        {
            GetTicketsAsync();
        }

        private async void GetTicketsAsync()
        {
            IsBusy = true;
            try
            {
                HttpResponseMessage response = await RestClient.GetAsync("https://10.0.2.2:5001/api/smartticket/gettickets");
                List<SmartTicket> ticketsList = await Utility.DeserializeObjectFromHttpResponse<List<SmartTicket>>(response);
                await TicketStore.ClearAsync();
                foreach (SmartTicket ticket in ticketsList)
                {
                    await TicketStore.AddItemAsync(ticket);
                }
                Tickets = new ObservableCollection<SmartTicket>(TicketStore.GetItemsAsync().Result);
                CanAddVirtualCard = !TicketStore.GetItemsAsync().Result.ToList().Any(t => t.Virtual);
                CanAssociateTicket = !TicketStore.GetItemsAsync().Result.ToList().Any(t => !t.Virtual);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void OnItemSelected(SmartTicket ticket)
        {
            if (ticket == null)
            {
                return;
            }
            await Shell.Current.GoToAsync($"{nameof(TicketDetailPage)}?{nameof(TicketDetailViewModel.TicketId)}={ticket.CardId}");
        }
    }
}

