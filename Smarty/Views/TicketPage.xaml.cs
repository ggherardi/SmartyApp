using Smarty.Models;
using Smarty.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smarty.Views
{
    public partial class TicketPage : ContentPage
    {
        TicketViewModel _viewModel;

        public TicketPage()
        {
            InitializeComponent();
            TicketViewModel viewModel = new TicketViewModel();
            this.BindingContext = viewModel;
            _viewModel = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}