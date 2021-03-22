using Newtonsoft.Json;
using Smarty.Models;
using Smarty.Utilities;
using System;
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

        SmartTicket _ticket;
        public SmartTicket Ticket
        {
            get { return _ticket; }
            set { SetProperty(ref _ticket, value); }
        }

        public ICommand GetTicket 
        { 
            get 
            {
                return new Command(async () =>
                {                    
                    HttpResponseMessage response = await RestClient.GetAsync("https://10.0.2.2:5001/api/smartticket");
                    Ticket = await Utility.DeserializeObjectFromHttpResponse<SmartTicket>(response);
                });
            } 
        }

        public TicketViewModel()
        {            
            Title = "Il tuo biglietto";
        }        
    }
}

