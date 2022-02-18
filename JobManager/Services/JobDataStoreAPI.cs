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
        public async Task AddJob(Job job)
        {
            throw new NotImplementedException();
        }

        public async Task<Job> GetJob(int jobId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Job>> GetJobs()
        {
            var service = DependencyService.Get<IWebClientService>();
            var jsonString = await service.GetString("https://jobmanagerdevapi.azurewebsites.net/Jobs");

            var jobs = JsonConvert.DeserializeObject<List<Job>>(jsonString);

            return jobs;
        }

        public async Task UpdateJob(Job job)
        {
            throw new NotImplementedException();
        }
    }
}
