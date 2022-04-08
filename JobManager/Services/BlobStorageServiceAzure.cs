using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using JobManager.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace JobManager.Services
{
    class BlobStorageServiceAzure : IBlobStorageService
    {

        //Related Documentation:
        //https://docs.microsoft.com/en-us/azure/storage/blobs/storage-quickstart-blobs-dotnet
        //https://docs.microsoft.com/en-us/azure/storage/blobs/storage-quickstart-blobs-xamarin
        //https://docs.microsoft.com/en-us/azure/storage/blobs/storage-blobs-list
        //https://docs.microsoft.com/en-us/visualstudio/data-tools/how-to-save-and-edit-connection-strings?view=vs-2019

        private readonly BlobServiceClient service = new BlobServiceClient(ConnectionString);

        private static string ConnectionString => "connectionstring";
        private static string Container => "studentnumber";

        public async Task<MemoryStream> DownloadStreamAsync(string name)
        {
            try
            {
                BlobClient blob = service.GetBlobContainerClient(Container).GetBlobClient(name);

                if (blob.Exists())
                {
                    var stream = new MemoryStream();
                    await blob.DownloadToAsync(stream);

                    stream.Position = 0;
                    return stream;
                }
            }
            catch (Exception e)
            {
                //Catch errors here.
            }

            return null;
        }

        public async Task<List<string>> ListBlobsAsync(string prefix = "", int? size = null)
        {
            List<string> blobs = new List<string>();
            try
            {
                var results = service.GetBlobContainerClient(Container).GetBlobsAsync().AsPages(default, size);

                await foreach (Azure.Page<BlobItem> blobPage in results)
                {
                    blobs.AddRange(from BlobItem blob in blobPage.Values
                                   where prefix != "" && blob.Name.StartsWith(prefix)
                                   select blob.Name);
                }

                return blobs;
            }
            catch (Exception e)
            {
                //Catch errors here.
            }
            return null;
        }

        public async Task UploadStreamAsync(string name, MemoryStream stream)
        {
            try
            {
                stream.Position = 0;
                BlobClient blob = service.GetBlobContainerClient(Container).GetBlobClient(name);
                await blob.UploadAsync(stream);
            }
            catch (Exception e)
            {
                //Catch errors here.
            }
        }
    }
}
