using JobManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobManager.Services
{
    public interface IJobDataStore<T>
    {
        Task<IEnumerable<Job>> GetJobsAsync();
        Task<Job> GetJobAsync(int jobId);
        Task AddJobAsync(Job job);
        Task UpdateJobAsync(Job job);
    }
}