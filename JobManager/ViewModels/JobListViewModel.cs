using JobManager.Models;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace JobManager.ViewModels
{
    class JobListViewModel : JobManagerBase
    {

        public ObservableRangeCollection<Job> Jobs { get; set; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand<Job> SelectedCommand { get; }
        public AsyncCommand AddCommand { get; }

        private Job selectedJob;

        public Job SelectedJob
        {
            get => selectedJob;
            set => SetProperty(ref selectedJob, value);
        }

        public JobListViewModel()
        {
            Title = "Jobs";

            Jobs = new ObservableRangeCollection<Job>();

            LoadJobs();

            RefreshCommand = new AsyncCommand(Refresh);
            SelectedCommand = new AsyncCommand<Job>(Selected);
            AddCommand = new AsyncCommand(Add);
        }

        async Task Add()
        {
            string route = $"{nameof(Views.JobDetailPage)}";
            await Shell.Current.GoToAsync(route);
        }

        async Task Selected(Job job)
        {
            string route = $"{nameof(Views.JobDetailPage)}?JobId={job.Id}";
            await Shell.Current.GoToAsync(route);
        }

        private async Task Refresh()
        {
            IsBusy = true;

            Jobs.Clear();
            LoadJobs();

            IsBusy = false;
        }

        public async void LoadJobs()
        {
            IEnumerable<Job> jobs = await JobDataStore.GetJobsAsync();
            Jobs.AddRange(jobs);
        }
    }
}
