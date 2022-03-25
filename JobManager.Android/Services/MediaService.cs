using System;
using System.IO;
using System.Threading.Tasks;
using Android.App;
using JobManager.Droid.Services;
using JobManager.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(MediaService))]

// Needed for Picking photo/video
[assembly: UsesPermission(Android.Manifest.Permission.ReadExternalStorage)]

// Needed for Taking photo/video
[assembly: UsesPermission(Android.Manifest.Permission.WriteExternalStorage)]
[assembly: UsesPermission(Android.Manifest.Permission.Camera)]

// Add these properties if you would like to filter out devices that do not have cameras, or set to false to make them optional.
[assembly: UsesFeature("android.hardware.camera", Required = true)]
[assembly: UsesFeature("android.hardware.camera.autofocus", Required = true)]

namespace JobManager.Droid.Services
{
    public class MediaService : IMediaService
    {
        //Related Documentation:
        //https://docs.microsoft.com/en-us/xamarin/essentials/get-started?tabs=windows%2Candroid
        //https://docs.microsoft.com/en-us/xamarin/essentials/media-picker?tabs=android
        public async Task<Image> CapturePhotoAsync()
        {
            try
            {
                FileResult result = await MediaPicker.CapturePhotoAsync();
                //await LoadPhotoAsync(result);

                Stream stream = await result.OpenReadAsync();

                Image image = new Image
                {
                    Source = ImageSource.FromStream(() => stream)
                };

                return image;
            }
            catch (FeatureNotSupportedException ex)
            {
                // Feature is not supported on the device
            }
            catch (PermissionException ex)
            {
                // Permissions not granted
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }

            return null;
        }
    }
}