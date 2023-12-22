using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MRF.DataAccess.Repository.IRepository;
using MRF.Models.DTO;
using MRF.Models.ViewModels;
using MRF.Utility;
using Swashbuckle.AspNetCore.Annotations;
using System.Text;


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
            
            var pathToFile = _hostEnvironment.ContentRootPath
                          
                           + Path.DirectorySeparatorChar.ToString()
                           + "EmailTemplate"
                           + Path.DirectorySeparatorChar.ToString()
                           + "MRFDetail.html";
            var builder = new BodyBuilder();
            using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
            {
                builder.HtmlBody = SourceReader.ReadToEnd();
            }        

            _logger.LogInfo($"Fetching All MRF Details by Id: {MrfId}");
            MrfdetailsPDFRequestModel mrfdetailpdf = _unitOfWork.MrfdetailsPDFRepository.GetRequisition(MrfId);
            string messageBody = builder.HtmlBody.Replace("{ReferenceNo}", mrfdetailpdf.ReferenceNo)
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
                
            if (mrfdetailpdf == null)
            {
                _logger.LogError($"No result found by this Id:{MrfId}");

                MrfdetailsPDFRequestModel blankData = new MrfdetailsPDFRequestModel();
                return blankData;
            }
            else
            {
                string folderPath = Path.Combine(_hostEnvironment.ContentRootPath, "PDFs");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                // Create a PDF document
                string pdfFileName = mrfdetailpdf.ReferenceNo.Replace("/", "_"); // Removing '/'
                string pdfFilePath = Path.Combine(folderPath, $"{pdfFileName}.pdf"); // Set your desired path
                string htmlFilePath = Path.Combine(folderPath, $"{pdfFileName}.html"); // HTML file path

                // Write HTML content to a file
                System.IO.File.WriteAllText(htmlFilePath, messageBody, Encoding.UTF8); // Write the messageBody to the file

                using (FileStream stream = new FileStream(pdfFilePath, FileMode.Create))
                {
                    //Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    //PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                    //pdfDoc.Open();

                    //// Read HTML content from the file and convert it to PDF
                    //StreamReader srHtml = new StreamReader(htmlFilePath, Encoding.UTF8); // Read from the HTML file
                    //XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, srHtml);

                    //pdfDoc.Close();
                }

                return mrfdetailpdf;
            }
        }
    }
}
