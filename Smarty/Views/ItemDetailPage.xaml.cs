using Smarty.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Smarty.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}