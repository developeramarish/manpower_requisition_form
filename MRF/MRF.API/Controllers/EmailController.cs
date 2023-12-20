using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using MRF.DataAccess.Repository.IRepository;
using MRF.Models.Models;
using MRF.Utility;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mail;
using Document = iTextSharp.text.Document;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;


namespace MRF.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IEmailService _emailService;
        private readonly ILoggerService _logger;
        private readonly IUnitOfWork _unitOfWork;
        public EmailController(IEmailService emailService, ILoggerService logger, IUnitOfWork unitOfWork, IHostingEnvironment hostingEnvironment)
        {
            _emailService = emailService;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
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

        [HttpPost("sendwithpdf")]
        [SwaggerOperation(Summary = "Sends an email with PDF.")]
        [SwaggerResponse(200, "Email sent successfully.")]
        [SwaggerResponse(500, "Internal server error.")]
        public async Task<IActionResult> SendEmailWithPDF(string Status)
        {
            string attachmentPath = null;
            emailmaster emailRequest = _unitOfWork.emailmaster.Get(u => u.status == Status);
            if (emailRequest != null)
            {
                // Create a new MemoryStream to hold the PDF data
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    // Create a new document
                    Document doc = new Document();
                    PdfWriter.GetInstance(doc, memoryStream);

                    // Open the document for writing
                    doc.Open();

                    // Add content to the document
                    doc.Add(new Paragraph(emailRequest.Content));

                    // Close the document
                    doc.Close();

                    // Convert the MemoryStream to a byte array
                    byte[] pdfData = memoryStream.ToArray();

                    string startupPath = "wwwroot\\PDF\\"; // Adjust the path as needed
                    string fileName = "output.pdf";
                    string filePath = Path.Combine(_hostingEnvironment.ContentRootPath, startupPath, fileName);

                    // Ensure the directory exists, create if not
                    if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                    }

                    // Save the PDF to disk
                    
                   System.IO.File.WriteAllBytes(filePath, pdfData);

                    // Create an Attachment with the PDF data
                    Attachment attachment = null;
                    if (attachmentPath != null)
                    {
                        byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
                        attachment = new Attachment(new MemoryStream(fileBytes), Path.GetFileName(filePath), "application/pdf");
                    }

                    // Send email with attached PDF
                    await _emailService.SendEmailAsync(emailRequest.emailTo, emailRequest.Subject, emailRequest.Content, filePath);

                    // Dispose the attachment after sending the email
                    attachment?.Dispose();

                    _logger.LogInfo("Email sent successfully.");
                    return Ok("Email sent successfully.");
                }
            }
            else
            {
                return Ok("Email not sent.");
            }
        }

    }


}
