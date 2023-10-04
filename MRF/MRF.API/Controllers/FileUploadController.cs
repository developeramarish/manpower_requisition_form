using Microsoft.AspNetCore.Mvc;

namespace MRF.API.Controllers
{
    [Route("api/upload")]
    [ApiController]
    public class FileUploadController : Controller
    {
        public string _rootPath;
 
        [Obsolete]
        public FileUploadController(Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            _rootPath = env.WebRootPath;
        }
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file,string ResumeOrAssign)
        { 
            // Check if a file was sent
            if (file == null || file.Length == 0)
                return BadRequest("No file received.");
           
            string directory = Path.Combine(_rootPath, ResumeOrAssign);

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            // Make sure to validate the file type and size.

            // Example: Save the file to a specific path
            var filePath = Path.Combine(directory, file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok("File uploaded successfully.");
        }
    }
}
