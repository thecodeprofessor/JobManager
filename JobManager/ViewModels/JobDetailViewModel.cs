using MvvmHelpers.Commands;
using JobManager.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Forms;

namespace JobManager.ViewModels
{
    class JobDetailViewModel : JobManagerBase
    {
        private int JobId { get; set; }

        private Job selectedJob;

        public Job SelectedJob
        {
            get => selectedJob;
            set => SetProperty(ref selectedJob, value);
        }

        public JobDetailViewModel(int JobId)
        {
            Title = "Job";

            this.JobId = JobId;

            LoadJob();

        }

        public async void LoadJob()
        {
            SelectedJob = await JobDataStore.GetJob(JobId);
        }

    }
}
