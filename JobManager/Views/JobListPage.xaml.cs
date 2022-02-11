
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JobManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JobListPage : ContentPage
    {
        public JobListPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            //int.TryParse(JobId, out var jobId);
            //BindingContext = new ParkDetailViewModel(parkId);
        }
    }
}