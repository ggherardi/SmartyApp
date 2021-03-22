using Smarty.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smarty.Views
{
    public partial class TicketPage : ContentPage
    {
        public TicketPage()
        {
            InitializeComponent();
            this.BindingContext = new TicketViewModel();        
        }
    }
}