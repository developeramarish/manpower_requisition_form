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
    public class EvaluationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private ResponseDTO _response;
        private EvaluationmasterResponseModel _responseModel;
        private readonly ILoggerService _logger;
        public EvaluationController(IUnitOfWork unitOfWork, ILoggerService logger)
        {
            _unitOfWork = unitOfWork;
            _response = new ResponseDTO();
            _responseModel = new EvaluationmasterResponseModel();
            _logger = logger;
        }
        // GET: api/<EvaluationController>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Successful response", Type = typeof(IEnumerable<Evaluationmaster>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad Request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal Server Error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public ResponseDTO Get()
        {
            _logger.LogInfo("Fetching All Evaluation");
            List<Evaluationmaster> evaluationList = _unitOfWork.Evaluationmaster.GetAll().ToList();
            if (evaluationList.Count == 0)
            {
                _logger.LogError("No record is found");
            }
            _response.Result = evaluationList;
            _response.Count= evaluationList.Count;
            _logger.LogInfo($"Total evaluation  count: {evaluationList.Count}");
            return _response;
        }

        // GET api/<EvaluationController>/5
        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Successful response", Type = typeof(Evaluationmaster))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad Request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal Server Error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public ResponseDTO Get(int id)
        {
            _logger.LogInfo($"Fetching All Evaluation by Id: {id}");
            Evaluationmaster evaluationmaster = _unitOfWork.Evaluationmaster.Get(u => u.Id == id);
            if (evaluationmaster == null)
            {
                _logger.LogError($"No result found by this Id:{id}" );
            }
            _response.Result = evaluationmaster;
            return _response;
        }

        // POST api/<EvaluationController>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, Description = "Item created successfully", Type = typeof(EvaluationmasterResponseModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad Request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, Description = "Unprocessable entity")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal Server Error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public EvaluationmasterResponseModel Post([FromBody] EvaluationmasterRequestModel request)
        {
            var evaluation = new Evaluationmaster
            {
                Type = request.Type,                
                IsActive = request.IsActive,
                CreatedByEmployeeId = request.CreatedByEmployeeId,
                CreatedOnUtc = request.CreatedOnUtc,
                UpdatedByEmployeeId = request.UpdatedByEmployeeId,
                UpdatedOnUtc = request.UpdatedOnUtc
            };
            _unitOfWork.Evaluationmaster.Add(evaluation);
            _unitOfWork.Save();

            _responseModel.Id = evaluation.Id;
            _responseModel.IsActive = evaluation.IsActive;

            return _responseModel;
        }

        // PUT api/<EvaluationController>/5
        [HttpPut("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Item updated successfully", Type = typeof(EvaluationmasterResponseModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad request")]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content (successful update)")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, Description = "Unprocessable entity")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal server error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public EvaluationmasterResponseModel Put(int id, [FromBody] EvaluationmasterRequestModel request)
        {
            var existingStatus = _unitOfWork.Evaluationmaster.Get(u => u.Id == id);

            if (existingStatus != null)
            {
                existingStatus.Type = request.Type;
                existingStatus.IsActive = request.IsActive;
                existingStatus.UpdatedByEmployeeId = request.UpdatedByEmployeeId;
                existingStatus.UpdatedOnUtc = request.UpdatedOnUtc;

                _unitOfWork.Evaluationmaster.Update(existingStatus);
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

        // DELETE api/<EvaluationController>/5
        [HttpDelete("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Item deleted successfully", Type = typeof(EvaluationmasterResponseModel))]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content (successful deletion)")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal server error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public void Delete(int id)
        {
            Evaluationmaster? obj = _unitOfWork.Evaluationmaster.Get(u => u.Id == id);
            if (obj != null)
            {
                _unitOfWork.Evaluationmaster.Remove(obj);
                _unitOfWork.Save();

            }
            else {
                _logger.LogError($"No result found by this Id: {id}");
            }
           
        }
    }
}
