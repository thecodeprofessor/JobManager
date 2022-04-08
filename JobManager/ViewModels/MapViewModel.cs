using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JobManager.Services;
using Xamarin.Forms;
using MvvmHelpers.Commands;


namespace JobManager.ViewModels
{
    //Related Documentation:
    //https://docs.microsoft.com/en-us/xamarin/android/platform/maps-and-location/maps/obtaining-a-google-maps-api-key?tabs=windows
    //https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/map/

    class MapViewModel : JobManagerBase
    {
        public AsyncCommand GetLocationCommand { get; }

        public MapViewModel()
        {
            Title = "Map";

            GetLocationCommand = new AsyncCommand(GetLocation);
        }

        async Task GetLocation()
        {
            var service = DependencyService.Get<IDeviceLocationService>();
            var deviceLocation = service.GetLastLocationAsync();


            //await Application.Current.MainPage.DisplayAlert("Orientation", deviceOrientation.ToString(), "OK");
        }
    }
}
