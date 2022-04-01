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
        public async Task<byte[]> CapturePhotoAsync()
        {
            try
            {
                FileResult result = await MediaPicker.CapturePhotoAsync();

                Stream stream = await result.OpenReadAsync();

                MemoryStream memory = new MemoryStream();
                stream.CopyTo(memory);

                return memory.ToArray();
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