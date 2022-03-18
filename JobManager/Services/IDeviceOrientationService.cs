using System;
using System.Collections.Generic;
using System.Text;
using JobManager.Models;

namespace JobManager.Services
{
    public interface IDeviceOrientationService
    {
        //Related Documentation:
        //https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/dependency-service/introduction
        //https://github.com/xamarin/xamarin-forms-samples/tree/main/DependencyService

        DeviceOrientation GetOrientation();
    }
}
