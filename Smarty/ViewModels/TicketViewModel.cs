using Newtonsoft.Json;
using Smarty.Models;
using Smarty.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Smarty.ViewModels
{
    public class TicketViewModel : BaseViewModel
    {
        public ObservableCollection<SmartTicket> Tickets { get; }

        public ICommand GetTickets => new Command(GetTicketsAsync);

        public TicketViewModel()
        {
            Tickets = new ObservableCollection<SmartTicket>();
            GetTicketsAsync();
        }        

        private async void GetTicketsAsync()
        {
            IsBusy = true;
            try
            {
                HttpResponseMessage response = await RestClient.GetAsync("https://10.0.2.2:5001/api/smartticket/gettickets");
                List<SmartTicket> ticketsList = await Utility.DeserializeObjectFromHttpResponse<List<SmartTicket>>(response);
                Tickets.Clear();
                foreach (SmartTicket ticket in ticketsList)
                {
                    Tickets.Add(ticket);
                }
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
    }
}

