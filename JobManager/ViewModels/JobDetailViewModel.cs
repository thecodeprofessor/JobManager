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
using System.IO;
using MvvmHelpers;

namespace JobManager.ViewModels
{
    [QueryProperty(nameof(JobId), nameof(JobId))]
    public class JobDetailViewModel : JobManagerBase
    {
        public ObservableRangeCollection<Picture> Pictures { get; set; }

        public AsyncCommand RefreshCommand { get; }

        public AsyncCommand SaveCommand { get; }
        public AsyncCommand TakePictureCommand { get; }
        public AsyncCommand PageAppearingCommand { get; }
        public AsyncCommand PageDisappearingCommand { get; }

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
            }
        }

        public JobDetailViewModel()
        {
            Pictures = new ObservableRangeCollection<Picture>();
            Pictures.Add(new Picture() { Name = "Loading..." });

            SaveCommand = new AsyncCommand(Save);
            TakePictureCommand = new AsyncCommand(TakePicture);

            RefreshCommand = new AsyncCommand(Refresh);

            PageAppearingCommand = new AsyncCommand(PageAppearing);
            PageDisappearingCommand = new AsyncCommand(PageDisappearing);
        }
        private async Task Refresh()
        {
            if (!IsBusy)
            {
                IsBusy = true;

                await LoadJob(JobId);

                IsBusy = false;
            }
        }

        async Task PageAppearing()
        {
            await Refresh();
        }

        async Task PageDisappearing()
        {
        }

        async Task TakePicture()
        {
            var service = DependencyService.Get<IMediaService>();
            var bytes = await service.CapturePhotoAsync();

            //Picture = ;
            
            string name = $"Jobs/Pictures/{JobId}/{Guid.NewGuid()}.png";

            var blob = DependencyService.Get<IBlobStorageService>();
            await blob.UploadStreamAsync(name, new MemoryStream(bytes));

            await Refresh();
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

        public async Task LoadJob(int jobId)
        {
            try
            {
                Job job = await JobDataStore.GetJobAsync(jobId);
                //JobId = job.Id;
                Name = job.Name;
                Description = job.Description;

                var blobService = DependencyService.Get<IBlobStorageService>();
                List<string> blobs = await blobService.ListBlobsAsync($"Jobs/Pictures/{jobId}/");

                Pictures.Clear();
                foreach (string blob in blobs)
                {
                    MemoryStream stream = await blobService.DownloadStreamAsync(blob);

                    Pictures.Add(new Picture
                    {
                        Name = blob,
                        Source = ImageSource.FromStream(() => new MemoryStream(stream.ToArray()))
                    });
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
