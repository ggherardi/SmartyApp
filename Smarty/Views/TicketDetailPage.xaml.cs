using Smarty.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smarty.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TicketDetailPage : ContentPage
    {
        public TicketDetailPage()
        {
            InitializeComponent();
            this.BindingContext = new TicketDetailViewModel();
        }
    }
}