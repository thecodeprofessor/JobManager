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

        public async Task AddJobAsync(Job job)
        {
            var service = DependencyService.Get<IWebClientService>();
            
            var jsonString = await service.PostAsync($"{API}/Jobs", JsonConvert.SerializeObject(job), "application/json");
            
            if (jsonString != null)
            {
                var newJob = JsonConvert.DeserializeObject<Job>(jsonString);
            }

            //Consider returning a bool to verify if the job was created.
        }

        public async Task<Job> GetJobAsync(int jobId)
        {
            var service = DependencyService.Get<IWebClientService>();
            var jsonString = await service.GetAsync($"{API}/Jobs/{jobId}");

            var job = JsonConvert.DeserializeObject<Job>(jsonString);

            return job;
        }

        public async Task<IEnumerable<Job>> GetJobsAsync()
        {
            var service = DependencyService.Get<IWebClientService>();
            var jsonString = await service.GetAsync($"{API}/Jobs");

            var jobs = JsonConvert.DeserializeObject<List<Job>>(jsonString);

            return jobs;
        }

        public async Task UpdateJobAsync(Job job)
        {
            var service = DependencyService.Get<IWebClientService>();

            await service.PutAsync($"{API}/Jobs/{job.Id}", JsonConvert.SerializeObject(job), "application/json");

            //Consider returning a bool to verify if the job was updated.
        }
    }
}
