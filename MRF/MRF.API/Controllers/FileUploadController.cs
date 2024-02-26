using Microsoft.AspNetCore.Mvc;
using MRF.Utility;

namespace MRF.API.Controllers
{
    [Route("api/upload")]
    [ApiController]
    public class FileUploadController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly string _rootPath;
        private readonly string _fallbackPath;
        
        public FileUploadController(IWebHostEnvironment env, IConfiguration configuration)
        {
            _env = env ?? throw new ArgumentNullException(nameof(env));
            _rootPath = _env.WebRootPath;
            _fallbackPath = configuration["FileUploadSettings:FallbackPath"];
        }
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file, string ResumeOrAssign, string FileName)
        {
            string directory = string.Empty;
            string filePath= string.Empty;
            // Check if a file was sent
            if (file == null || file.Length == 0)
                return BadRequest("No file received.");

            if (_rootPath == null)
                directory = Path.Combine(_fallbackPath, ResumeOrAssign);
            else
                directory = Path.Combine(_rootPath, ResumeOrAssign);

            directory = Path.Combine(directory, DateTime.Now.ToString("yyyy-MM-dd"));
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            // Make sure to validate the file type and size.




            if (!string.IsNullOrEmpty(FileName))
            {
                string fileNameWithoutExtension = FileName;
                string fileExtension = Path.GetExtension(file.FileName);
                string newFileName = $"{fileNameWithoutExtension}{fileExtension}";
                filePath = Path.Combine(directory, newFileName);
            }
            else
            {
                filePath = Path.Combine(directory, file.FileName);
            }
            // Example: Save the file to a specific path
            
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok("File uploaded successfully.");
        }


        
    }
}
