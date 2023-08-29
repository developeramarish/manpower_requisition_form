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
    public class SubdepartmentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private ResponseDTO _response;
        private SubdepartmentmasterResponseModel _responseModel;
        private readonly ILoggerService _logger;
        public SubdepartmentController(IUnitOfWork unitOfWork, ILoggerService logger)
        {
            _unitOfWork = unitOfWork;
            _response = new ResponseDTO();
            _responseModel = new SubdepartmentmasterResponseModel();
            _logger = logger;
        }
        // GET: api/<SubdepartmentController>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Successful response", Type = typeof(IEnumerable<Subdepartmentmaster>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad Request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal Server Error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public ResponseDTO Get()
        {
            _logger.LogInfo("Fetching All Sub Department");
            List<Subdepartmentmaster> subdepartmentList = _unitOfWork.Subdepartmentmaster.GetAll().ToList();
            if (subdepartmentList == null)
            {
                _logger.LogError("No record is found");
            }
            _response.Result = subdepartmentList;
            _logger.LogInfo($"Total sub department  count: {subdepartmentList.Count}");
            return _response;
        }

        // GET api/<SubdepartmentController>/5
        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Successful response", Type = typeof(Candidatestatusmaster))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad Request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal Server Error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public ResponseDTO Get(int id)
        {
            _logger.LogInfo($"Fetching All Sub Department by Id: {id}");
            Subdepartmentmaster subdepartmentmaster = _unitOfWork.Subdepartmentmaster.Get(u => u.Id == id);
            if (subdepartmentmaster == null)
            {
                _logger.LogError($"No result found by this Id: {id}");
            }
            _response.Result = subdepartmentmaster;
            return _response;
        }

        // POST api/<SubdepartmentController>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, Description = "Item created successfully", Type = typeof(SubdepartmentmasterResponseModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad Request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, Description = "Unprocessable entity")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal Server Error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public SubdepartmentmasterResponseModel Post([FromBody] SubdepartmentmasterRequestModel request)
        {
            var subDapartment = new Subdepartmentmaster
            {
                DepartmentId = request.DepartmentId,
                Name = request.Name,
                IsActive = request.IsActive,
                CreatedByEmployeeId = request.CreatedByEmployeeId,
                CreatedOnUtc = request.CreatedOnUtc,
                UpdatedByEmployeeId = request.UpdatedByEmployeeId,
                UpdatedOnUtc = request.UpdatedOnUtc
            };

            _unitOfWork.Subdepartmentmaster.Add(subDapartment);
            _unitOfWork.Save();

            _responseModel.Id = subDapartment.Id;
            _responseModel.IsActive = subDapartment.IsActive;

            return _responseModel;
        }

        // PUT api/<SubdepartmentController>/5
        [HttpPut("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Item updated successfully", Type = typeof(SubdepartmentmasterResponseModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad request")]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content (successful update)")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, Description = "Unprocessable entity")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal server error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public SubdepartmentmasterResponseModel Put(int id, [FromBody] SubdepartmentmasterRequestModel request)
        {
            var existingStatus = _unitOfWork.Subdepartmentmaster.Get(u => u.Id == id);

            if (existingStatus != null)
            {
                existingStatus.Name = request.Name;
                existingStatus.DepartmentId = request.DepartmentId;
                existingStatus.IsActive = request.IsActive;
                existingStatus.UpdatedByEmployeeId = request.UpdatedByEmployeeId;
                existingStatus.UpdatedOnUtc = request.UpdatedOnUtc;

                _unitOfWork.Subdepartmentmaster.Update(existingStatus);
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

        // DELETE api/<SubdepartmentController>/5
        [HttpDelete("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Item deleted successfully", Type = typeof(SubdepartmentmasterResponseModel))]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content (successful deletion)")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal server error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public void Delete(int id)
        {
            Subdepartmentmaster? obj = _unitOfWork.Subdepartmentmaster.Get(u => u.Id == id);
            if (obj == null)
            {
                _logger.LogError($"No result found by this Id: {id}");
            }
            _unitOfWork.Subdepartmentmaster.Remove(obj);
            _unitOfWork.Save();
        }
    }
}
