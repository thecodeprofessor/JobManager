using Azure.Storage.Blobs;
using JobManager.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace JobManager.Services
{
    class JobDataStoreBlobStorageJson : IJobDataStore<Job>
    {

        //Related Documentation:
        //https://docs.microsoft.com/en-us/azure/storage/blobs/storage-quickstart-blobs-dotnet
        //https://docs.microsoft.com/en-us/azure/storage/blobs/storage-quickstart-blobs-xamarin
        //https://docs.microsoft.com/en-us/visualstudio/data-tools/how-to-save-and-edit-connection-strings?view=vs-2019

        private readonly BlobServiceClient service = new BlobServiceClient(ConnectionString);

        private static string ConnectionString => "DefaultEndpointsProtocol=https;AccountName=abourbihjobmanager;AccountKey=/McaX8bxkMrN/NSNgazwXLopvuSPXlVSXgN/pcG6g9mT8dG/wjM1lMJkWXUVRPiPbRK1ExVRclyJ+AStdSeZZg==;EndpointSuffix=core.windows.net";
        private static string Container => "data";
        private static string FileName => "Jobs.json";

        public async Task WriteFile(List<Job> jobs)
        {
            var jsonString = JsonConvert.SerializeObject(jobs);

            var stream = new MemoryStream();

            var writer = new StreamWriter(stream);
            writer.Write(jsonString);
            writer.Flush();

            stream.Position = 0;

            BlobClient blob = service.GetBlobContainerClient(Container).GetBlobClient(FileName);

            await blob.UploadAsync(stream);
        }
        public async Task<List<Job>> ReadFile()
        {
            BlobClient blob = service.GetBlobContainerClient(Container).GetBlobClient(FileName);

            if (blob.Exists())
            {
                var stream = new MemoryStream();
                await blob.DownloadToAsync(stream);

                stream.Position = 0;

                var jsonString = new StreamReader(stream).ReadToEnd();

                var jobs = JsonConvert.DeserializeObject<List<Job>>(jsonString);

                return jobs;

            }
            else
            {
                var defaultJobs = GetDefaultJobs();

                await WriteFile(defaultJobs);

                return defaultJobs;
            }
        }
        private List<Job> GetDefaultJobs()
        {
            var jobs = new List<Job>()
            {
                new Job { Id = 1, Name = "Job A Azure Blob File", Description = "This is job a." },
                new Job { Id = 2, Name = "Job B Azure Blob File", Description = "This is job b." },
                new Job { Id = 3, Name = "Job C Azure Blob File", Description = "This is job c." },
                new Job { Id = 4, Name = "Job D Azure Blob File", Description = "This is job d." }
            };

            return jobs;
        }

        public async Task AddJobAsync(Job job)
        {
            var jobs = await ReadFile();
            jobs.Add(job);

            await WriteFile(jobs);
        }

        public async Task<Job> GetJobAsync(int jobId)
        {
            var jobs = await ReadFile();

            var job = jobs.Find(p => p.Id == jobId);

            return job;
        }

        public async Task<IEnumerable<Job>> GetJobsAsync()
        {
            var jobs = await ReadFile();

            return jobs;
        }

        public async Task UpdateJobAsync(Job job)
        {
            var jobs = await ReadFile ();
            jobs[jobs.FindIndex(p => p.Id == job.Id)] = job;

            await WriteFile (jobs);
        }
    }
}
