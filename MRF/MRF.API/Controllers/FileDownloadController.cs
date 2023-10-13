using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MRF.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileDownloadController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly string _rootPath;
        private readonly string _fallbackPath;
        public FileDownloadController(IWebHostEnvironment env, IConfiguration configuration)
        {
            _env = env ?? throw new ArgumentNullException(nameof(env));
            _rootPath = _env.WebRootPath;
            _fallbackPath = configuration["FileUploadSettings:FallbackPath"];
        }
    [HttpGet("{fileName}/{ResumeOrAssign}")]
        public IActionResult GetFile(string fileName, string ResumeOrAssign)
        {
            try
            {
                string directory = string.Empty;
                // Check if a file was sent
                if (fileName == null)
                    return BadRequest("No file received.");
                if (_rootPath == null)
                    directory = Path.Combine(_fallbackPath, ResumeOrAssign);
                else
                    directory = Path.Combine(_rootPath, ResumeOrAssign);
                
                    var filePath = Path.Combine(directory, fileName);
                    if (System.IO.File.Exists(filePath))
                    {
                        var fileBytes = System.IO.File.ReadAllBytes(filePath);
                        return File(fileBytes, "application/octet-stream"); // Return as binary data
                    }
                    else
                    {
                        return NotFound("File not found");
                    }
                
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., security, file not found) and return an appropriate response
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
