using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace JobManager.Services
{
    //Related Documentation:
    //https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/dependency-service/introduction
    //https://github.com/xamarin/xamarin-forms-samples/tree/main/DependencyService

    public interface IDeviceLocationService
    {
        Task<Location> GetLastLocationAsync();
        Task<Location> GetCurrentLocationAsync(GeolocationAccuracy accuracy = GeolocationAccuracy.Medium, int timeoutSeconds = 10);
    }
}
