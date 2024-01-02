using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MRF.DataAccess.Repository.IRepository;
using MRF.Models.DTO;
using MRF.Models.ViewModels;
using MRF.Utility;
using Swashbuckle.AspNetCore.Annotations;

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
        private readonly IHTMLtoPDF _hTMLtoPDF;
        
        public MrfdetailsPDFController(IUnitOfWork unitOfWork, ILoggerService logger, IEmailService emailService, IHostEnvironment hostEnvironment, IHTMLtoPDF hTMLtoPDF)
        {
            _unitOfWork = unitOfWork;
            _response = new ResponseDTO();
            _logger = logger;
            _emailService = emailService;
            _hostEnvironment = hostEnvironment;
            _hTMLtoPDF= hTMLtoPDF;
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

            var emailTemplatePath = Path.Combine(_hostEnvironment.ContentRootPath, "EmailTemplate", "MRFEmailTemplate.html");
            var pdfFileName = $"{mrfdetailpdf.ReferenceNo.Replace("/", "_")}.pdf";
            string htmlBody;
            using (var sourceReader = System.IO.File.OpenText(emailTemplatePath))
            {
                var builder = new BodyBuilder();
                builder.HtmlBody = sourceReader.ReadToEnd();
                htmlBody = GetHtmlTemplateBody(builder.HtmlBody, mrfdetailpdf);
            }

            //Commented code to convert html to pdf
            //_hTMLtoPDF.CovertHtmlToPDF(htmlBody, pdfFileName);

            var emailRequest = _unitOfWork.emailmaster.Get(u => u.status == "Awaiting COO Approval");
            if (emailRequest != null)
            {
                _emailService.SendEmailAsync(emailRequest.emailTo, emailRequest.Subject, htmlBody);
            }
            return mrfdetailpdf;
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
