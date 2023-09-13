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
    public class MrfstatusController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private ResponseDTO _response;
        private MrfstatusmasterResponseModel _responseModel;
        private readonly ILoggerService _logger;

        public MrfstatusController(IUnitOfWork unitOfWork, ILoggerService logger)
        {
            _unitOfWork = unitOfWork;
            _response = new ResponseDTO();
            _responseModel = new MrfstatusmasterResponseModel();
            _logger = logger;
        }
        // GET: api/<MrfstatusController>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Successful response", Type = typeof(IEnumerable<Mrfstatusmaster>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad Request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal Server Error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public ResponseDTO Get()
        {
           

            _logger.LogInfo("Fetching All MRF Status");
            List<Mrfstatusmaster> mrfStatusList = _unitOfWork.Mrfstatusmaster.GetAll().ToList();
            if (mrfStatusList.Count == 0)
            {
                _logger.LogError("No record is found");
            }
            _response.Result = mrfStatusList;
            _response.Count=mrfStatusList.Count;
            _logger.LogInfo($"Total MRF Status  count: {mrfStatusList.Count}");
            return _response;
        }

        // GET api/<MrfstatusController>/5
        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Successful response", Type = typeof(Mrfstatusmaster))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad Request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal Server Error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public ResponseDTO Get(int id)
        {
            _logger.LogInfo($"Fetching All MRF Status by Id: {id}");
            Mrfstatusmaster mrfstatusmaster = _unitOfWork.Mrfstatusmaster.Get(u => u.Id == id);
            if (mrfstatusmaster == null)
            {
                _logger.LogError($"No result found by this Id:{id}");
            }
            _response.Result = mrfstatusmaster;
            return _response;
        }

        // POST api/<MrfstatusController>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, Description = "Item created successfully", Type = typeof(MrfstatusmasterResponseModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad Request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, Description = "Unprocessable entity")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal Server Error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public MrfstatusmasterResponseModel Post([FromBody] MrfstatusmasterRequestModel request)
        {
            var mrfStatus = new Mrfstatusmaster
            {
                Status = request.Status,
                IsActive = request.IsActive,
                CreatedByEmployeeId = request.CreatedByEmployeeId,
                CreatedOnUtc = request.CreatedOnUtc,
                UpdatedByEmployeeId = request.UpdatedByEmployeeId,
                UpdatedOnUtc = request.UpdatedOnUtc
            };

            _unitOfWork.Mrfstatusmaster.Add(mrfStatus);
            _unitOfWork.Save();

            _responseModel.Id = mrfStatus.Id;
            _responseModel.IsActive = mrfStatus.IsActive;

            return _responseModel;
        }

        // PUT api/<MrfstatusController>/5
        [HttpPut("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Item updated successfully", Type = typeof(CandidatestatusmasterResponseModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad request")]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content (successful update)")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, Description = "Unprocessable entity")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal server error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public MrfstatusmasterResponseModel Put(int id, [FromBody] MrfstatusmasterRequestModel request)
        { 
            var existingStatus = _unitOfWork.Mrfstatusmaster.Get(u => u.Id == id);

            if (existingStatus != null)
            {
                existingStatus.Status = request.Status;
                existingStatus.IsActive = request.IsActive;
                existingStatus.UpdatedByEmployeeId = request.UpdatedByEmployeeId;
                existingStatus.UpdatedOnUtc = request.UpdatedOnUtc;

                _unitOfWork.Mrfstatusmaster.Update(existingStatus);
                _unitOfWork.Save();

                _responseModel.Id = existingStatus.Id;
                _responseModel.IsActive = existingStatus.IsActive;
            }
            else
            {
                _logger.LogError($"No result found by this Id: {id}");
                _responseModel.Id = 0;
                _responseModel.IsActive = false;
            }
            return _responseModel;
        }

        // DELETE api/<MrfstatusController>/5
        [HttpDelete("{Id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Item deleted successfully", Type = typeof(MrfstatusmasterResponseModel))]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content (successful deletion)")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal server error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public void Delete(int Id)
        {
            Mrfstatusmaster? obj = _unitOfWork.Mrfstatusmaster.Get(u => u.Id == Id);
            if (obj != null)
            {
                _unitOfWork.Mrfstatusmaster.Remove(obj);
                _unitOfWork.Save();

            }
            else {
                _logger.LogError($"No result found by this Id: {Id}");
            }
            
        }

        
    }
}
