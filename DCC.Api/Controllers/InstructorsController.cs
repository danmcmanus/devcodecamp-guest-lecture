using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DCC.Domain.Services;
using DCC.Domain.DTO;
using AutoMapper;
using DCC.Api.Models;
using DCC.Data.Models;
using DCC.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace DCC.Api.Controllers
{
    public class InstructorsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IInstructorsService _instructorsService;

        private static readonly string storageConnectionString =
            "DefaultEndpointsProtocol=https;AccountName=cs2bd73b13d5638x471cx8b3;AccountKey=8H79sbL/IiKrEEhxOapbERUpZgZaOb606FdczAv7wqG4dXromZYSriwqfV3CM8kw8CffFZVdnLFwQlS4XRyf1A==;EndpointSuffix=core.windows.net";

        public InstructorsController(IInstructorsService instructorsService, IMapper mapper)
        {
            _mapper = mapper;
            _instructorsService = instructorsService;
        }

        // GET: Instructors
        public async Task<IActionResult> Index()
        {
            var instructors = await _instructorsService.GetAllInstructorsAsync();
            var model = _mapper.Map<IEnumerable<InstructorDTO>>(instructors);
            return View(model);
        }

        // GET: Instructors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Instructors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Image")] InstructorVM instructor)
        {
            CloudStorageAccount account = CloudStorageAccount.Parse(storageConnectionString);
            CloudBlobClient serviceClient = account.CreateCloudBlobClient();

            // Create container. Name must be lower case.
            Console.WriteLine("Creating container...");
            var container = serviceClient.GetContainerReference("instructor-images");
            container.CreateIfNotExistsAsync().Wait();

            // write a blob to the container
            CloudBlockBlob blob = container.GetBlockBlobReference("helloworld.txt");
            
            using (var memoryStream = new MemoryStream())
            {
                await instructor.Image.CopyToAsync(memoryStream);
                await blob.UploadFromStreamAsync(memoryStream);
                
            }

            if (ModelState.IsValid)
            {
                await _instructorsService.AddInstructorAsync(new InstructorRequest
                {
                    FirstName = instructor.FirstName,
                    LastName = instructor.LastName
                });

                return RedirectToAction(nameof(Index));
            }
            return View(instructor);
        }
        public static async Task<bool> UploadFileToStorage(Stream fileStream, string fileName, AzureStorageConfig _storageConfig)
        {
            // Create storagecredentials object by reading the values from the configuration (appsettings.json)
            StorageCredentials storageCredentials = new StorageCredentials(_storageConfig.AccountName, _storageConfig.AccountKey);

            // Create cloudstorage account by passing the storagecredentials
            CloudStorageAccount storageAccount = new CloudStorageAccount(storageCredentials, true);

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Get reference to the blob container by passing the name by reading the value from the configuration (appsettings.json)
            CloudBlobContainer container = blobClient.GetContainerReference(_storageConfig.ImageContainer);

            // Get the reference to the block blob from the container
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);

            // Upload the file
            await blockBlob.UploadFromStreamAsync(fileStream);

            return await Task.FromResult(true);
        }
        //// GET: Instructors/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var instructor = await _context.Instructors.FindAsync(id);
        //    if (instructor == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(instructor);
        //}

        //// POST: Instructors/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,JobTitle,Image,AverageRating,AggregateRatings,NumberOfRatings,IsDeleted")] Instructor instructor)
        //{
        //    if (id != instructor.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(instructor);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!InstructorExists(instructor.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(instructor);
        //}

        //// GET: Instructors/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var instructor = await _context.Instructors
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (instructor == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(instructor);
        //}

        //// POST: Instructors/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var instructor = await _context.Instructors.FindAsync(id);
        //    _context.Instructors.Remove(instructor);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool InstructorExists(int id)
        //{
        //    return _context.Instructors.Any(e => e.Id == id);
        //}
    }
}
