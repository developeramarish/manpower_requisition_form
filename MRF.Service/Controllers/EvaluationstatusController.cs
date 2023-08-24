using Microsoft.AspNetCore.Mvc;
using MRF.DataAccess.Repository.IRepository;
using MRF.Models.DTO;
using MRF.Models.Models;
using Swashbuckle.AspNetCore.Annotations;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MRF.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvaluationstatusController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private ResponseDTO _response;
        private EvaluationstatusmasterResponseModel _responseModel;
        private readonly ILogger<EvaluationstatusController> _logger;
        public EvaluationstatusController(IUnitOfWork unitOfWork, ILogger<EvaluationstatusController> logger)
        {
            _unitOfWork = unitOfWork;
            _response = new ResponseDTO();
            _responseModel = new EvaluationstatusmasterResponseModel();
            _logger = logger;
        }
        // GET: api/<EvaluationstatusController>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Successful response", Type = typeof(IEnumerable<Evaluationstatusmaster>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad Request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal Server Error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public ResponseDTO Get()
        {
            try
            {
                List<Evaluationstatusmaster> obj = _unitOfWork.Evaluationstatusmaster.GetAll().ToList();
                _response.Result = obj;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _logger.LogError(ex.Message);
            }
            return _response;
        }

        // GET api/<EvaluationstatusController>/5
        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Successful response", Type = typeof(Evaluationstatusmaster))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad Request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal Server Error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public ResponseDTO Get(int id)
        {
            try
            {
                Evaluationstatusmaster evaluationstatusmaster = _unitOfWork.Evaluationstatusmaster.Get(u => u.Id == id);
                if (evaluationstatusmaster == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "No result found by this Id: " + id;
                    _logger.LogError("No result found by this Id:" + id);
                }
                _response.Result = evaluationstatusmaster;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _logger.LogError(ex.Message);
            }
            return _response;
        }

        // POST api/<EvaluationstatusController>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, Description = "Item created successfully", Type = typeof(EvaluationstatusmasterResponseModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad Request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, Description = "Unprocessable entity")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal Server Error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public EvaluationstatusmasterResponseModel Post([FromBody] EvaluationstatusmasterRequestModel request)
        {
            try
            {
                var evaluationstatus = new Evaluationstatusmaster
                {
                    Status = request.Status,
                    IsActive = request.IsActive,
                    CreatedByEmployeeId = request.CreatedByEmployeeId,
                    CreatedOnUtc = request.CreatedOnUtc,
                    UpdatedByEmployeeId = request.UpdatedByEmployeeId,
                    UpdatedOnUtc = request.UpdatedOnUtc
                };

                _unitOfWork.Evaluationstatusmaster.Add(evaluationstatus);
                _unitOfWork.Save();

                _responseModel.Id = evaluationstatus.Id;
                _responseModel.Status = evaluationstatus.Status;
                _responseModel.IsActive = evaluationstatus.IsActive;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _logger.LogError(ex.Message);
            }

            return _responseModel;
        }

        // PUT api/<EvaluationstatusController>/5
        [HttpPut("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Item updated successfully", Type = typeof(EvaluationstatusmasterResponseModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad request")]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content (successful update)")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, Description = "Unprocessable entity")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal server error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public EvaluationstatusmasterResponseModel Put(int id, [FromBody] EvaluationstatusmasterRequestModel request)
        {
            try
            {
                var existingStatus = _unitOfWork.Evaluationstatusmaster.Get(u => u.Id == id);
                existingStatus.Status = request.Status;
                existingStatus.IsActive = request.IsActive;
                existingStatus.UpdatedByEmployeeId = request.UpdatedByEmployeeId;
                existingStatus.UpdatedOnUtc = request.UpdatedOnUtc;

                _unitOfWork.Evaluationstatusmaster.Update(existingStatus);
                _unitOfWork.Save();

                _responseModel.Id = existingStatus.Id;
                _responseModel.Status = existingStatus.Status;
                _responseModel.IsActive = existingStatus.IsActive;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _logger.LogError(ex.Message);
            }
            return _responseModel;
        }

        // DELETE api/<EvaluationstatusController>/5
        [HttpDelete("{Id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Item deleted successfully", Type = typeof(EvaluationstatusmasterResponseModel))]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content (successful deletion)")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal server error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public void Delete(int id)
        {
            try
            {
                Evaluationstatusmaster? obj = _unitOfWork.Evaluationstatusmaster.Get(u => u.Id == id);
                _unitOfWork.Evaluationstatusmaster.Remove(obj);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _logger.LogError(ex.Message);
            }
        }
    }
}
