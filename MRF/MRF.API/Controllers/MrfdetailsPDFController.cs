using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MRF.DataAccess.Repository.IRepository;
using MRF.Models.DTO;
using MRF.Models.ViewModels;
using MRF.Utility;
using Swashbuckle.AspNetCore.Annotations;
using iText.Kernel.Pdf;
using iText.Html2pdf;
using MRF.Models.Models;

namespace MRF.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MrfdetailsPDFController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private ResponseDTO _response;
        private readonly ILoggerService _logger;
        private readonly IEmailService _emailService;
        private readonly IHostEnvironment _hostEnvironment;
        
        public MrfdetailsPDFController(IUnitOfWork unitOfWork, ILoggerService logger, IEmailService emailService, IHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _response = new ResponseDTO();
            _logger = logger;
            _emailService = emailService;
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet("{MrfId}")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Successful response", Type = typeof(MrfDetailsViewModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad Request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal Server Error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public MrfdetailsPDFRequestModel GetRequisition(int MrfId)
        {
            var mrfdetailpdf = _unitOfWork.MrfdetailsPDFRepository.GetRequisition(MrfId);

            if (mrfdetailpdf == null)
            {
                _logger.LogError($"No result found by this Id:{MrfId}");
                return new MrfdetailsPDFRequestModel();
            }

            //var emailTemplatePath = Path.Combine(_hostEnvironment.ContentRootPath, "EmailTemplate", "MRFDetail.html");
            var emailTemplatePath = Path.Combine(_hostEnvironment.ContentRootPath, "EmailTemplate", "MRFEmailTemplate.html");
            var pdfDirectory = Path.Combine(_hostEnvironment.ContentRootPath, "PDFs");
            var pdfFileName = $"{mrfdetailpdf.ReferenceNo.Replace("/", "_")}.pdf";
            var pathToOutputPdfFile = Path.Combine(pdfDirectory, pdfFileName);

            if (!Directory.Exists(pdfDirectory))
            {
                Directory.CreateDirectory(pdfDirectory);
            }

            string htmlBody;
            using (var sourceReader = System.IO.File.OpenText(emailTemplatePath))
            {
                var builder = new BodyBuilder();
                builder.HtmlBody = sourceReader.ReadToEnd();
                //htmlBody = GetHtmlMessageBody(builder.HtmlBody, mrfdetailpdf);
                htmlBody = GetHtmlTemplateBody(builder.HtmlBody, mrfdetailpdf);
                
            }

            ConvertHtmlToPdf(htmlBody, pathToOutputPdfFile);

            var emailRequest = _unitOfWork.emailmaster.Get(u => u.status == "Awaiting COO Approval");
            if (emailRequest != null)
            {
                _emailService.SendEmailAsync(emailRequest.emailTo, emailRequest.Subject, emailRequest.Content, pathToOutputPdfFile);
            }

            return mrfdetailpdf;
        }
        static void ConvertHtmlToPdf(string htmlString, string outputFile)
        {
            using (var pdfWriter = new PdfWriter(outputFile))
            {
                using (var pdfDocument = new PdfDocument(pdfWriter))
                {
                    var converterProperties = new ConverterProperties();
                    HtmlConverter.ConvertToPdf(htmlString, pdfDocument, converterProperties);
                }
            }
        }
        private string GetHtmlMessageBody(string htmlBody, MrfdetailsPDFRequestModel mrfdetailpdf)
        {
            // Replace placeholders in HTML with data
            string messageBody = htmlBody
                .Replace("{ReferenceNo}", mrfdetailpdf.ReferenceNo)
                 .Replace("{RequisitionType}", mrfdetailpdf.RequisitionType)
                 .Replace("{PositionTitleId}", Convert.ToString(mrfdetailpdf.PositionTitleId))
                 .Replace("{Department}", mrfdetailpdf.Department)
                 .Replace("{SubDepartment}", mrfdetailpdf.SubDepartment)
                 .Replace("{Project}", mrfdetailpdf.Project)
                 .Replace("{Location}", mrfdetailpdf.Location)
                 .Replace("{PositionReportingto}", mrfdetailpdf.PositionReportingto)
                 .Replace("{HiringInitiationDate}", Convert.ToString(mrfdetailpdf.HiringInitiationDate))
                 .Replace("{GradeMin}", mrfdetailpdf.GradeMin)
                 .Replace("{GradeMax}", mrfdetailpdf.GradeMax)
                 .Replace("{TypeOfEmployment}", mrfdetailpdf.TypeOfEmployment)
                 .Replace("{ReplacementForThEmployee}", Convert.ToString(mrfdetailpdf.ReplacementForThEmployee))
                 .Replace("{NumberOfVacancies}", Convert.ToString(mrfdetailpdf.NumberOfVacancies))
                 .Replace("{TypeOfVacancy}", Convert.ToString(mrfdetailpdf.TypeOfVacancy))
                 .Replace("{ExperienceMin}", Convert.ToString(mrfdetailpdf.ExperienceMin))
                 .Replace("{ExperienceMax}", Convert.ToString(mrfdetailpdf.ExperienceMax))
                 .Replace("{Gender}", mrfdetailpdf.Gender)
                 .Replace("{Qualification}", mrfdetailpdf.Qualification)
                 .Replace("{JobDescription}", Convert.ToString(mrfdetailpdf.JobDescription))
                 .Replace("{Skills}", mrfdetailpdf.Skills)
                 .Replace("{Justification}", mrfdetailpdf.Justification)
                 .Replace("{MinTargetSalary}", Convert.ToString(mrfdetailpdf.MinTargetSalary))
                 .Replace("{MaxTargetSalary}", Convert.ToString(mrfdetailpdf.MaxTargetSalary));              

            return messageBody;
        }
        private string GetHtmlTemplateBody(string htmlBody, MrfdetailsPDFRequestModel mrfdetailpdf)
        {
            // Replace placeholders in HTML with data
            string messageBody = htmlBody
              .Replace("{ReferenceNo}", mrfdetailpdf.ReferenceNo)
              .Replace("{NumberOfVacancies}", Convert.ToString(mrfdetailpdf.NumberOfVacancies))
              .Replace("{MaxTargetSalary}", Convert.ToString((mrfdetailpdf.MaxTargetSalary) / 100000))
              .Replace("{TotalTargetSalary}", Convert.ToString((mrfdetailpdf.MaxTargetSalary * mrfdetailpdf.NumberOfVacancies) / 100000))
              .Replace("{GradeMin}", mrfdetailpdf.GradeMin)
              .Replace("{GradeMax}", mrfdetailpdf.GradeMax)
              .Replace("{PositionName}", mrfdetailpdf.PositionName)
              .Replace("{Department}", mrfdetailpdf.Department)
              .Replace("{SubDepartment}", mrfdetailpdf.SubDepartment)
              .Replace("{Project}", mrfdetailpdf.Project)
              .Replace("{Justification}", mrfdetailpdf.Justification)
              .Replace("{MRFRaisedBy}", Convert.ToString(mrfdetailpdf.MRFRaisedBy));

            return messageBody;
        }
    }
}
