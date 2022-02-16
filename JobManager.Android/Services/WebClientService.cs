    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Android.Content;
    using JobManager.Droid.Services;
    using JobManager.Services;
    using Xamarin.Forms;

[assembly: Dependency(typeof(WebClientService))]
namespace JobManager.Droid.Services
    {
        public class WebClientService : IWebClientService
        {

            //Related Documentation:
            //https://docs.microsoft.com/en-us/xamarin/xamarin-forms/data-cloud/web-services/rest

            public async Task<string> GetString(string Uri)
            {
                try
                {
                    HttpClient client;
                    client = new HttpClient();

                    HttpResponseMessage response = await client.GetAsync(Uri);
                    return response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync() : null;
                }
                catch
                {
                    return null;
                }
            }
        }
    }