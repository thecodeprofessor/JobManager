using MvvmHelpers.Commands;
using JobManager.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Forms;
using System.Diagnostics;

namespace JobManager.ViewModels
{
    [QueryProperty(nameof(JobId), nameof(JobId))]
    public class JobDetailViewModel : JobManagerBase
    {

        public AsyncCommand SaveCommand { get; }

        private int jobId;
        private string name;
        private string description;

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public int JobId
        {
            get
            {
                return jobId;
            }
            set
            {
                jobId = value;
                LoadJob(value);
            }
        }

        public JobDetailViewModel()
        {
            SaveCommand = new AsyncCommand(Save);
        }

        async Task Save()
        {
            Job job = new Job
            {
                Id = jobId,
                Name = Name,
                Description = Description
            };

            await JobDataStore.UpdateJob(job);

            await Shell.Current.GoToAsync("..");
        }

        public async void LoadJob(int jobId)
        {
            try
            {
                Job job = await JobDataStore.GetJob(jobId);
                //JobId = job.Id;
                Name = job.Name;
                Description = job.Description;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
