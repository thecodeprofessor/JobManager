using JobManager.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace JobManager.Views
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