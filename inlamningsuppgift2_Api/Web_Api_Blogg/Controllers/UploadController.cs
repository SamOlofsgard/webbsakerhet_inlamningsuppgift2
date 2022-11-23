using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Api_Blogg.Data;
using Web_Api_Blogg.Models;
using Web_Api_Blogg.Models.Entities;

namespace Web_Api_Blogg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : Controller
    {
        private BlobServiceClient serviceClient;
        private BlobContainerClient containerClient;
        private BlobClient blobClient;
                

        public UploadController(IConfiguration configuration)
        {
            serviceClient = new BlobServiceClient(configuration.GetConnectionString("StorageAccount"));
            try
            {
                containerClient = serviceClient.CreateBlobContainer("images");
                containerClient.SetAccessPolicy(Azure.Storage.Blobs.Models.PublicAccessType.BlobContainer);
            }
            catch { containerClient = serviceClient.GetBlobContainerClient("images"); }
        }

        [HttpPost]
        public async Task<IActionResult> Upload([FromForm] FileModel fileModel)
        {
            try
            {
                using var file = fileModel.File.OpenReadStream();
                blobClient = containerClient.GetBlobClient($"IMG_{Guid.NewGuid()}{Path.GetExtension(fileModel.File.FileName)}");
                await blobClient.UploadAsync(file);  

                return new OkObjectResult(blobClient.Uri.AbsoluteUri);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
