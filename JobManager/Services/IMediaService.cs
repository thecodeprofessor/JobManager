using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace JobManager.Services
{
    public interface IMediaService
    {
        //Related Documentation:
        //https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/dependency-service/introduction
        //https://github.com/xamarin/xamarin-forms-samples/tree/main/DependencyService

        Task<byte[]> CapturePhotoAsync(); //Opens the camera to take a photo.
        //Task PickPhotoAsync(); //Opens the media browser to select a photo.
        //Task PickVideoAsync(); //Opens the media browser to select a video.
        //Task CaptureVideoAsync(); //Opens the camera to take a video.

    }
}