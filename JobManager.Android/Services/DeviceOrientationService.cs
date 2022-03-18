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

[assembly: Dependency(typeof(DeviceOrientationService))]
namespace JobManager.Droid.Services
{
    class DeviceOrientationService : IDeviceOrientationService
    {
        public DeviceOrientation GetOrientation()
        {
            IWindowManager windowManager = Android.App.Application.Context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();

            SurfaceOrientation orientation = windowManager.DefaultDisplay.Rotation;

            if (orientation == SurfaceOrientation.Rotation90 || orientation == SurfaceOrientation.Rotation270)
            {
                return DeviceOrientation.Landscape;
            }
            else if (orientation == SurfaceOrientation.Rotation0 || orientation == SurfaceOrientation.Rotation180)
            {
                return DeviceOrientation.Portrait;
            }
            else
            {
                return DeviceOrientation.Undefined;
            }
        }
    }
}