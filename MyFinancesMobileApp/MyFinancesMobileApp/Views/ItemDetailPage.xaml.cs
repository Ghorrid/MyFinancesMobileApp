using MyFinancesMobileApp.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace MyFinancesMobileApp.Views
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