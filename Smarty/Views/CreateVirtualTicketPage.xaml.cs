using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smarty.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smarty.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateVirtualTicketPage : ContentPage
    {
        public CreateVirtualTicketPage()
        {
            InitializeComponent();
            this.BindingContext = new CreateVirtualTicketViewModel();
        }
    }
}