using JobManager.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobManager.Services
{
    //Related Documentation:
    //https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/dependency-service/introduction
    //https://github.com/xamarin/xamarin-forms-samples/tree/main/DependencyService

    public interface IDeviceBatteryService
    {
        DeviceBattery GetBattery();
    }
}
