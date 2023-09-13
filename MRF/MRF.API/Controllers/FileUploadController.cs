using Microsoft.AspNetCore.Mvc;

namespace MRF.API.Controllers
{
    [Route("api/upload")]
    [ApiController]
    public class FileUploadController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            // Check if a file was sent
            if (file == null || file.Length == 0)
                return BadRequest("No file received.");

            // You can handle the uploaded file here, for example, save it to disk
            // or process it in some way.
            // Make sure to validate the file type and size.

            // Example: Save the file to a specific path
            var filePath = Path.Combine("your_upload_directory", file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok("File uploaded successfully.");
        }
    }
}
