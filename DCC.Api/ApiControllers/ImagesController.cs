using System;
using System.Web;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using DCC.Api.Helpers;
using DCC.Api.Models;
using DCC.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Auth;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Options;

namespace DCC.Api.ApiControllers
{
    [Route("api/images")]
    public class ImagesController : Controller
    {
        private AzureStorageConfig storageConfig;
        private IOptions<AzureStorageConfig> azureConfig;
        private string _accountKey = "8H79sbL/IiKrEEhxOapbERUpZgZaOb606FdczAv7wqG4dXromZYSriwqfV3CM8kw8CffFZVdnLFwQlS4XRyf1A==";
        private string _accountName = "cs2bd73b13d5638x471cx8b3";
        static CloudBlobClient blobClient;
        const string blobContainerName = "instructor-images";
        static CloudBlobContainer blobContainer;
        string storageConnectionString = Environment.GetEnvironmentVariable("STORAGE_CONNECTION_STRING");
        public ImagesController(IOptions<AzureStorageConfig> config)
        {
            storageConfig = new AzureStorageConfig()
            {
                AccountKey = _accountKey,
                AccountName = _accountName,
                ImageContainer = "instructor-images",
                ThumbnailContainer = "thumbnails"
            };
        }

        // POST /api/images/upload
        [HttpPost("upload")]
        public async Task<IActionResult> UploadAsync()
        {
            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=devcodecampapi;AccountKey=CIWpQRPHzHmc454moVQyYcMkHTvxfORaGlFi6Imfvdo62iKMWBW93sk2qt+7va/BVMF2GCP3pVsWGA+RUk/SPQ==;EndpointSuffix=core.windows.net");
                
                // Create a blob client for interacting with the blob service.
                blobClient = storageAccount.CreateCloudBlobClient();
                blobContainer = blobClient.GetContainerReference(blobContainerName);
                await blobContainer.CreateIfNotExistsAsync();

                await blobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

                IFormFileCollection files = HttpContext.Request.Form.Files;
                int fileCount = files.Count;

                if (fileCount > 0)
                {
                    for (int i = 0; i < fileCount; i++)
                    {
                        var source = files[i].OpenReadStream();
                        CloudBlockBlob blob = blobContainer.GetBlockBlobReference(GetRandomBlobName(files[i].FileName));
                        try
                        {
                            await blob.UploadFromStreamAsync(source).ConfigureAwait(true);

                            return RedirectToAction("Index", "Instructors", null, null);
                        }
                        catch (Exception e)
                        {
                            return BadRequest(e.Message);
                        }

                    }
                }

                return Ok("Please select an image to upload");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        // GET /api/images/thumbnails
        [HttpGet("thumbnails")]
        public async Task<IActionResult> GetThumbNails()
        {

            try
            {
                if (storageConfig.AccountKey == string.Empty || storageConfig.AccountName == string.Empty)

                    return BadRequest("sorry, can't retrieve your azure storage details from appsettings.js, make sure that you add azure storage details there");

                if (storageConfig.ImageContainer == string.Empty)

                    return BadRequest("Please provide a name for your image container in the azure blob storage");

                List<string> thumbnailUrls = await StorageHelper.GetThumbNailUrls(storageConfig);

                return new ObjectResult(thumbnailUrls);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        /// <summary> 
        /// string GetRandomBlobName(string filename): Generates a unique random file name to be uploaded  
        /// </summary> 
        private string GetRandomBlobName(string filename)
        {
            string ext = Path.GetExtension(filename);
            return string.Format("{0:10}_{1}{2}", DateTime.Now.Ticks, Guid.NewGuid(), ext);
        }
    }
}
