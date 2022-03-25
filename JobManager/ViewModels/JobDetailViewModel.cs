using MvvmHelpers.Commands;
using JobManager.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Forms;
using System.Diagnostics;
using JobManager.Services;

namespace JobManager.ViewModels
{
    [QueryProperty(nameof(JobId), nameof(JobId))]
    public class JobDetailViewModel : JobManagerBase
    {

        public AsyncCommand SaveCommand { get; }
        public AsyncCommand TakePictureCommand { get; }

        private int jobId;
        private string name;
        private string description;
        private ImageSource picture;

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

        public ImageSource Picture
        {
            get => picture;
            set => SetProperty(ref picture, value);
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
            TakePictureCommand = new AsyncCommand(TakePicture);
        }

        async Task TakePicture()
        {
            var service = DependencyService.Get<IMediaService>();
            Image image = await service.CapturePhotoAsync();
            Picture = image.Source;
        }

        async Task Save()
        {
            Job job = new Job
            {
                Id = jobId,
                Name = name,
                Description = description
            };

            if (jobId != 0)
                await JobDataStore.UpdateJobAsync(job);
            else
                await JobDataStore.AddJobAsync(job);

            await Shell.Current.GoToAsync("..");
        }

        public async void LoadJob(int jobId)
        {
            try
            {
                Job job = await JobDataStore.GetJobAsync(jobId);
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
