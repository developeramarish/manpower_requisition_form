using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MRF.DataAccess.Repository.IRepository;
using MRF.Models.DTO;
using MRF.Models.ViewModels;
using MRF.Utility;
using Swashbuckle.AspNetCore.Annotations;
using iText.Kernel.Pdf;
using iText.Html2pdf;

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
        public  MrfdetailsPDFRequestModel GetRequisition(int MrfId)
        {
            MrfdetailsPDFRequestModel mrfdetailpdf = _unitOfWork.MrfdetailsPDFRepository.GetRequisition(MrfId);

            if (mrfdetailpdf == null)
            {
                _logger.LogError($"No result found by this Id:{MrfId}");
                MrfdetailsPDFRequestModel blankData = new MrfdetailsPDFRequestModel();
                return blankData;
            }
            else
            {
                var pathToFile = $"{_hostEnvironment.ContentRootPath}/EmailTemplate/MRFDetail.html";
                var pathToOutputPdfFile = $"{_hostEnvironment.ContentRootPath}/PDFs/" + mrfdetailpdf.ReferenceNo.Replace("/", "_") + ".pdf";
                string htmlBody;

                using (var sourceReader = System.IO.File.OpenText(pathToFile))
                {
                    var builder = new BodyBuilder();
                    builder.HtmlBody = sourceReader.ReadToEnd();
                    htmlBody = GetHtmlMessageBody(builder.HtmlBody, mrfdetailpdf);
                }

                ConvertHtmlToPdf(htmlBody, pathToOutputPdfFile);
                
                return mrfdetailpdf;
            }
        }

        static void ConvertHtmlToPdf(string htmlString, string outputFile)
        {
            using (PdfWriter pdfWriter = new PdfWriter(outputFile))
            {
                using (PdfDocument pdfDocument = new PdfDocument(pdfWriter))
                {
                    ConverterProperties converterProperties = new ConverterProperties();
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
                 .Replace("{PositionTitle}", mrfdetailpdf.PositionTitle)
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
    }
}
