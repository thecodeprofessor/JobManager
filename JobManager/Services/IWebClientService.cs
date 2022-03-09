using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace JobManager.Services
{
    public interface IWebClientService
    {
        //Related Documentation:
        //https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/dependency-service/introduction
        //https://github.com/xamarin/xamarin-forms-samples/tree/main/DependencyService

        Task<string> GetAsync(string uri);
        Task<string> PostAsync(string uri, string body, string type);
        Task<string> PutAsync(string uri, string body, string type);
    }
}