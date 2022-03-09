using JobManager.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace JobManager.Services
{
    class JobDataStoreAPI : IJobDataStore<Job>
    {
        private static string StudentNumber => "studentnumber";
        private static string API => $"https://jobmanagerdevapi.azurewebsites.net/{StudentNumber}";

        public async Task AddJob(Job job)
        {
            throw new NotImplementedException();
        }

        public async Task<Job> GetJob(int jobId)
        {
            var service = DependencyService.Get<IWebClientService>();
            var jsonString = await service.GetAsync($"{API}/Jobs/{jobId}");

            var job = JsonConvert.DeserializeObject<Job>(jsonString);

            return job;
        }

        public async Task<IEnumerable<Job>> GetJobs()
        {
            var service = DependencyService.Get<IWebClientService>();
            var jsonString = await service.GetAsync($"{API}/Jobs");

            var jobs = JsonConvert.DeserializeObject<List<Job>>(jsonString);


            //var service = DependencyService.Get<IWebClientService>();
            //var jsonString = await service.PostAsync($"{API}/Jobs", "{\"id\": 0,\"name\": \"test111\",\"description\": \"testdesc111\"}", "application/json");

            return jobs;
        }

        public async Task UpdateJob(Job job)
        {
            throw new NotImplementedException();
        }
    }
}
