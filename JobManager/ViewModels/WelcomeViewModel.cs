using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JobManager.Services;
using Xamarin.Forms;
using MvvmHelpers.Commands;


namespace JobManager.ViewModels
{
    class WelcomeViewModel : JobManagerBase
    {
        public AsyncCommand GetOrientationCommand { get; }

        private string orientation;
        public string Orientation
        {
            get => orientation;
            set => SetProperty(ref orientation, value);
        }

        public WelcomeViewModel()
        {
            Title = "Welcome";

            GetOrientationCommand = new AsyncCommand(GetOrientation);
        }

        async Task GetOrientation()
        {
            var service = DependencyService.Get<IDeviceOrientationService>();
            var deviceOrientation = service.GetOrientation();

            Orientation = deviceOrientation.ToString();

            //await Application.Current.MainPage.DisplayAlert("Orientation", deviceOrientation.ToString(), "OK");
        }
    }
}
