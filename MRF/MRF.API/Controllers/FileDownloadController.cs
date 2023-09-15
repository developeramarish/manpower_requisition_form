using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MRF.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileDownloadController : ControllerBase
    {
        public string _rootPath;
        [Obsolete]
        public FileDownloadController(Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            _rootPath = env.WebRootPath;
        }
        [HttpGet("{fileName}{ResumeOrAssign}")]
        public IActionResult GetFile(string fileName, string ResumeOrAssign)
        {
            try
            {

                // Check if a file was sent
                if (fileName == null)
                    return BadRequest("No file received.");

                string directory = Path.Combine(_rootPath, ResumeOrAssign);

               
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
