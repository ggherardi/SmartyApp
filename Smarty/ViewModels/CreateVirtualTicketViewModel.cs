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
            HttpResponseMessage response = await RestClient.PostEmptyAsync("https://10.0.2.2:5001/api/smartticket/createvirtualticket");
            await Xamarin.Essentials.SecureStorage.SetAsync("VirtualTicket", response.Content.ReadAsStringAsync().Result);
            var a = await Xamarin.Essentials.SecureStorage.GetAsync("VirtualTicket");
            Debug.WriteLine(a);
            await Shell.Current.GoToAsync($"//{nameof(TicketPage)}");
        }
    }

    
}
