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
    public partial class AddCreditPage : ContentPage
    {
        public AddCreditPage()
        {
            InitializeComponent();
            this.BindingContext = new AddCreditViewModel();
        }
    }
}