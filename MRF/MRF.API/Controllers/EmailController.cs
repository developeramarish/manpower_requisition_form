using Microsoft.AspNetCore.Mvc;
using MRF.Utility;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MRF.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly ILoggerService _logger;
        public EmailController(IEmailService emailService, ILoggerService logger)
        {
            _emailService = emailService;
            _logger = logger;

        }

        /// <summary>
        /// Sends an email.
        /// </summary>
        /// <param name="emailRequest">Email details</param>
        /// <returns>Result of the email sending operation.</returns>
        [HttpPost("send")]
        [SwaggerOperation(Summary ="Sends an email.")]
        [SwaggerResponse(200, "Email sent successfully.")]
        [SwaggerResponse(500, "Internal server error.")]
        public async Task<IActionResult> SendEmail([FromBody] EmailRequest emailRequest)
        {
            await _emailService.SendEmailAsync(emailRequest.ToEmail, emailRequest.Subject, emailRequest.Content);
            _logger.LogInfo("Email sent successfully.");
            return Ok("Email sent successfully.");
        }
    }

    public class EmailRequest
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
