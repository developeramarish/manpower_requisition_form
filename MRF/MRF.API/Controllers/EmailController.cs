using Microsoft.AspNetCore.Mvc;
using MRF.DataAccess.Repository;
using MRF.DataAccess.Repository.IRepository;
using MRF.Models.Models;
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
        private readonly IUnitOfWork _unitOfWork;
        public EmailController(IEmailService emailService, ILoggerService logger, IUnitOfWork unitOfWork)
        {
            _emailService = emailService;
            _logger = logger;
            _unitOfWork = unitOfWork;
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
        public async Task<IActionResult> SendEmail(String Status)
        {
            emailmaster emailRequest = _unitOfWork.emailmaster.Get(u => u.status == Status);
            if (emailRequest != null)
            {   await _emailService.SendEmailAsync(emailRequest.emailTo, emailRequest.Subject, emailRequest.Content);
                _logger.LogInfo("Email sent successfully.");
                return Ok("Email sent successfully.");
            }
            else { return Ok("Email not sent."); }
        }
    }

   
}
