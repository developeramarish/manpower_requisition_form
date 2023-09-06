using Microsoft.AspNetCore.Mvc;
using MRF.DataAccess.Repository.IRepository;
using MRF.Models.DTO;
using MRF.Models.Models;
using MRF.Utility;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MRF.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReplacementmrfdetailController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private ResponseDTO _response;
        private ReplacementmrfdetailResponseModel _responseModel;
        private readonly ILoggerService _logger;

        public ReplacementmrfdetailController(IUnitOfWork unitOfWork, ILoggerService logger)
        {
            _unitOfWork = unitOfWork;
            _response = new ResponseDTO();
            _responseModel = new ReplacementmrfdetailResponseModel();
            _logger = logger;
        }
        // GET: api/<ReplacementmrfdetailController>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Successful response", Type = typeof(IEnumerable<Replacementmrfdetails>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad Request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal Server Error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public ResponseDTO Get()
        {
            _logger.LogInfo("Fetching All Replacement mrf detail");
            List<Replacementmrfdetails> replacementmrfdetailList = _unitOfWork.Replacementmrfdetail.GetAll().ToList();
            if (replacementmrfdetailList == null)
            {
                _logger.LogError("No record is found");
            }
            _response.Result = replacementmrfdetailList;
            _logger.LogInfo($"Total Replacement mrf detail count: {replacementmrfdetailList.Count}");
            return _response;
        }

        // GET api/<ReplacementmrfdetailController>/5
        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Successful response", Type = typeof(Replacementmrfdetails))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad Request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal Server Error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public ResponseDTO Get(int id)
        {
            _logger.LogInfo($"Fetching All Replacement mrf detail by Id: {id}");
            Replacementmrfdetails  replacementmrfdetail = _unitOfWork.Replacementmrfdetail.Get(u => u.Id == id);
            if (replacementmrfdetail == null)
            {
                _logger.LogError($"No result found by this Id: {id}");
            }
            _response.Result = replacementmrfdetail;
            return _response;
        }

        // POST api/<ReplacementmrfdetailController>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, Description = "Item created successfully", Type = typeof(ReplacementmrfdetailResponseModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad Request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, Description = "Unprocessable entity")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal Server Error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public ReplacementmrfdetailResponseModel Post([FromBody] ReplacementmrfdetailRequestModel request)
        {
            var replacementmrfdetail = new Replacementmrfdetails
            {
                MrfId = request.MrfId,
                EmployeeName = request.EmployeeName,
                EmailId = request.EmailId,
                EmployeeCode = request.EmployeeCode,
                LastWorkingDate = request.LastWorkingDate,
                AnnualCtc = request.AnnualCtc,
                AnnualGross = request.AnnualGross,
                GradeId = request.GradeId,
                CreatedByEmployeeId = request.CreatedByEmployeeId,
                CreatedOnUtc = request.CreatedOnUtc,
                UpdatedByEmployeeId = request.UpdatedByEmployeeId,
                UpdatedOnUtc = request.UpdatedOnUtc
            };

            _unitOfWork.Replacementmrfdetail.Add(replacementmrfdetail);
            _unitOfWork.Save();

            _responseModel.Id = replacementmrfdetail.Id;
            return _responseModel;
        }

        // PUT api/<ReplacementmrfdetailController>/5
        [HttpPut("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Item updated successfully", Type = typeof(ReplacementmrfdetailResponseModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad request")]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content (successful update)")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, Description = "Unprocessable entity")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal server error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public ReplacementmrfdetailResponseModel Put(int id, [FromBody] ReplacementmrfdetailRequestModel request)
        {
            var existingStatus = _unitOfWork.Replacementmrfdetail.Get(u => u.Id == id);

            if (existingStatus != null)
            {
                existingStatus.MrfId = request.MrfId;
                existingStatus.EmployeeName = request.EmployeeName;
                existingStatus.EmailId = request.EmailId;
                existingStatus.EmployeeCode = request.EmployeeCode;
                existingStatus.LastWorkingDate = request.LastWorkingDate;
                existingStatus.AnnualCtc = request.AnnualCtc;
                existingStatus.AnnualGross = request.AnnualGross;
                existingStatus.GradeId = request.GradeId;
                existingStatus.UpdatedByEmployeeId = request.UpdatedByEmployeeId;
                existingStatus.UpdatedOnUtc = request.UpdatedOnUtc;

                _unitOfWork.Replacementmrfdetail.Update(existingStatus);
                _unitOfWork.Save();
                _responseModel.Id = existingStatus.Id;                
            }
            else
            {
                _logger.LogError($"No result found by this Id: {id}");
                _responseModel.Id = 0;                
            }
            return _responseModel;
        }

        // DELETE api/<ReplacementmrfdetailController>/5
        [HttpDelete("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Item deleted successfully", Type = typeof(ReplacementmrfdetailResponseModel))]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content (successful deletion)")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal server error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public void Delete(int id)
        {
            Replacementmrfdetails? obj = _unitOfWork.Replacementmrfdetail.Get(u => u.Id == id);
            if (obj == null)
            {
                _logger.LogError($"No result found by this Id: {id}");
            }
            _unitOfWork.Replacementmrfdetail.Remove(obj);
            _unitOfWork.Save();
        }
    }
}
