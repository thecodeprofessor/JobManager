using JobManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobManager.Services
{
    public interface IJobDataStore<T>
    {
        Task<IEnumerable<Job>> GetJobs();
        Task<Job> GetJob(int jobId);
        Task AddJob(Job job);
        Task UpdateJob(Job job);
    }
}