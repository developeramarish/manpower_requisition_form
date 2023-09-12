using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MRF.DataAccess.Repository.IRepository;
using MRF.Models.DTO;
using MRF.Models.Models;
using MRF.Models.ViewModels;
using MRF.Utility;
using Swashbuckle.AspNetCore.Annotations;

namespace MRF.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    
    public class DashboardController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private ResponseDTO _response;
        private readonly ILoggerService _logger;

        public DashboardController(IUnitOfWork unitOfWork, ILoggerService logger)
        {
            _unitOfWork = unitOfWork;
            _response = new ResponseDTO();
            _logger = logger;
        }


        // GET: api/<MrfstatusController>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Successful response", Type = typeof(IEnumerable<MrfSummaryViewModel>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad Request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal Server Error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public ResponseDTO GetMrfStatusSummary()
        {


            _logger.LogInfo("Fetching All Mrf resume reviewer map");
            List<MrfSummaryViewModel> MrfStatusSummary = _unitOfWork.Dashboard.GroupByMrfStatus().ToList();
            if (MrfStatusSummary == null)
            {
                _logger.LogError("No record is found");
            }
            _response.Result = MrfStatusSummary;
            _logger.LogInfo($"Total Mrf resume reviewer map count: {MrfStatusSummary.Count}");
            return _response;
        }

        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Successful response", Type = typeof(MrfResumeSummaryViewModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad Request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal Server Error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public ResponseDTO GetMrfResumeSummary()
        {
            _logger.LogInfo("Fetching All Mrf resume reviewer map");
            List<MrfResumeSummaryViewModel> mrfresumereviewermapList = _unitOfWork.Dashboard.GetCountByMrfIdAndResumeStatus().ToList();
            if (mrfresumereviewermapList == null)
            {
                _logger.LogError("No record is found");
            }
            _response.Result = mrfresumereviewermapList;
            _logger.LogInfo($"Total Mrf resume reviewer map count: {mrfresumereviewermapList.Count}");
            return _response;
        }


        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Successful response", Type = typeof(MrfInterviewSummaryViewModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad Request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal Server Error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public ResponseDTO GetMrfInterviewSummary()
        {
            _logger.LogInfo("Fetching All Mrf resume reviewer map");
            List<MrfInterviewSummaryViewModel> mrfInterviewSummary = _unitOfWork.Dashboard.GroupByMrfInterviewStatus().ToList();
            if (mrfInterviewSummary == null)
            {
                _logger.LogError("No record is found");
            }
            _response.Result = mrfInterviewSummary;
            _logger.LogInfo($"Total Mrf resume reviewer map count: {mrfInterviewSummary.Count}");
            return _response;
        }

    }
}
