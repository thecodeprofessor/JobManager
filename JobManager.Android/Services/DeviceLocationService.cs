using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using JobManager.Services;
using JobManager.Models;
using JobManager.Droid.Services;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System.Threading;

//Needed for accessing hardware location.
[assembly: UsesPermission(Android.Manifest.Permission.AccessCoarseLocation)]
[assembly: UsesPermission(Android.Manifest.Permission.AccessFineLocation)]
[assembly: UsesFeature("android.hardware.location", Required = false)]
[assembly: UsesFeature("android.hardware.location.gps", Required = false)]
[assembly: UsesFeature("android.hardware.location.network", Required = false)]

[assembly: Dependency(typeof(DeviceLocationService))]

namespace JobManager.Droid.Services
{
    class DeviceLocationService : IDeviceLocationService
    {
        //Related Documentation:
        //https://docs.microsoft.com/en-us/xamarin/essentials/geolocation?tabs=android

        public async Task<Location> GetLastLocationAsync()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    return location;
                }
            }
            catch (FeatureNotSupportedException ex)
            {
                //Handle not supported on device exception.
            }
            catch (FeatureNotEnabledException ex)
            {
                //Handle not enabled on device exception.
            }
            catch (PermissionException ex)
            {
                //Handle permission exception.
            }
            catch (Exception ex)
            {
                //Unable to get location.
            }

            return null;
        }

        public async Task<Location> GetCurrentLocationAsync(GeolocationAccuracy accuracy = GeolocationAccuracy.Medium, int timeoutSeconds = 10)
        {
            try
            {
                var request = new GeolocationRequest(accuracy, TimeSpan.FromSeconds(timeoutSeconds));
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                    return location;
                }
            }
            catch (FeatureNotSupportedException ex)
            {
                //Handle not supported on device exception.
            }
            catch (FeatureNotEnabledException ex)
            {
                //Handle not enabled on device exception.
            }
            catch (PermissionException ex)
            {
                //Handle permission exception.
            }
            catch (Exception ex)
            {
                //Unable to get location.
            }

            return null;
        }
    }
}