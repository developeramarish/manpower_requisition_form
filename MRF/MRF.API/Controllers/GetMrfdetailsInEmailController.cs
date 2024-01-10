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
    public class GetMrfdetailsInEmailController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private ResponseDTO _response;
        private readonly ILoggerService _logger;
        private readonly ISmtpEmailService _emailService;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly IHTMLtoPDF _hTMLtoPDF;
        private readonly IConfiguration _configuration;
        public GetMrfdetailsInEmailController(IUnitOfWork unitOfWork, ILoggerService logger, ISmtpEmailService emailService, IHostEnvironment hostEnvironment, IHTMLtoPDF hTMLtoPDF, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _response = new ResponseDTO();
            _logger = logger;
            _emailService = emailService;
            _hostEnvironment = hostEnvironment;
            _hTMLtoPDF = hTMLtoPDF;
            _configuration = configuration;
        }

        [HttpGet("{MrfId}")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Successful response", Type = typeof(MrfDetailsViewModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad Request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal Server Error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public MrfdetailsEmailRequestModel GetRequisition(int MrfId)
        {
            var mrfdetail = _unitOfWork.MrfdetailsEmailRepository.GetRequisition(MrfId);
            if (mrfdetail == null)
            {
                _logger.LogError($"No result found by this Id:{MrfId}");
                return new MrfdetailsEmailRequestModel();
            }

            var emailTemplatePath = Path.Combine(_hostEnvironment.ContentRootPath, "EmailTemplate", "MRFEmailTemplate.html");
            var pdfFileName = $"{mrfdetail.ReferenceNo.Replace("/", "_")}.pdf";
            string htmlBody;
            using (var sourceReader = System.IO.File.OpenText(emailTemplatePath))
            {
                var builder = new BodyBuilder();
                builder.HtmlBody = sourceReader.ReadToEnd();
                htmlBody = GetHtmlTemplateBody(builder.HtmlBody, mrfdetail);
            }

            //Commented code to convert html to pdf
            //_hTMLtoPDF.CovertHtmlToPDF(htmlBody, pdfFileName);

            var emailRequest = _unitOfWork.emailmaster.Get(u => u.status == "Awaiting COO Approval");
            if (emailRequest != null)
            {
               _emailService.SendEmail(emailRequest.emailTo, emailRequest.Subject, htmlBody);
            }
            return mrfdetail;
        }
        private string GetHtmlTemplateBody(string htmlBody, MrfdetailsEmailRequestModel mrfdetailemail)
        {
            string base_url = _configuration["Links:BaseUrl"];
          
            int MrfId = mrfdetailemail.Id;
            int EmpId = mrfdetailemail.ApproverId;
            int StatusId = 7;
            string strApprovalLink = $"{base_url}/approve/MrfId={MrfId}/EmpId={EmpId}/StatusId={StatusId}";
            string strRejectionLink = $"{base_url}/reject/MrfId={MrfId}/EmpId={EmpId}/StatusId={StatusId}";
            string strByPassLink = $"{base_url}/bypass/MrfId={MrfId}/EmpId={EmpId}/StatusId={StatusId}";

            // Replace placeholders in HTML with data
            string messageBody = htmlBody
              .Replace("{ReferenceNo}", mrfdetailemail.ReferenceNo)
              .Replace("{NumberOfVacancies}", Convert.ToString(mrfdetailemail.NumberOfVacancies))
              .Replace("{MaxTargetSalary}", Convert.ToString((mrfdetailemail.MaxTargetSalary) / 100000))
              .Replace("{TotalTargetSalary}", Convert.ToString((mrfdetailemail.MaxTargetSalary * mrfdetailemail.NumberOfVacancies) / 100000))
              .Replace("{GradeMin}", mrfdetailemail.GradeMin)
              .Replace("{GradeMax}", mrfdetailemail.GradeMax)
              .Replace("{PositionName}", mrfdetailemail.PositionName)
              .Replace("{Department}", mrfdetailemail.Department)
              .Replace("{SubDepartment}", mrfdetailemail.SubDepartment)
              .Replace("{Project}", mrfdetailemail.Project)
              .Replace("{Justification}", mrfdetailemail.Justification)
              .Replace("{MRFRaisedBy}", Convert.ToString(mrfdetailemail.MRFRaisedBy))
              .Replace("{approvalLink}", strApprovalLink)
              .Replace("{rejectLink}", strRejectionLink)
              .Replace("{bypassLink}", strByPassLink);

            return messageBody;
        }
    }
}
