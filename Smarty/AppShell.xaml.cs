using Smarty.ViewModels;
using Smarty.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Smarty
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(RegistrationPage), typeof(RegistrationPage));
            Routing.RegisterRoute(nameof(TicketDetailPage), typeof(TicketDetailPage));
            Routing.RegisterRoute(nameof(AddCreditPage), typeof(AddCreditPage));
            Routing.RegisterRoute(nameof(CreateVirtualTicketPage), typeof(CreateVirtualTicketPage));
            Routing.RegisterRoute(nameof(BindPhysicalTicketPage), typeof(BindPhysicalTicketPage));
        }

    }
}
