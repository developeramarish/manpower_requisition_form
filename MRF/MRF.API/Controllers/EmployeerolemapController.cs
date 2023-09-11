using Microsoft.AspNetCore.Mvc;
using MRF.DataAccess.Repository.IRepository;
using MRF.Models.DTO;
using MRF.Models.Models;
using MRF.Utility;
using SendGrid;
using Swashbuckle.AspNetCore.Annotations;
namespace MRF.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class EmployeerolemapController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private ResponseDTO _response;
        private EmployeerolemapResponseModel _responseModel;
        private readonly ILoggerService _logger;
        public EmployeerolemapController(IUnitOfWork unitOfWork, ILoggerService logger)
        {
            _unitOfWork = unitOfWork;
            _response = new ResponseDTO();
            _responseModel = new EmployeerolemapResponseModel();
            _logger = logger;
        }
        // GET: api/<EmployeerolemapController>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Successful response", Type = typeof(IEnumerable<Employeerolemap>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad Request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal Server Error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public ResponseDTO Get()
        {
            _logger.LogInfo("Fetching All Gemder");
            List<Employeerolemap> EmployeerolemapList = _unitOfWork.Employeerolemap.GetAll().ToList();
            if (EmployeerolemapList.Count == 0)
            {
                _logger.LogError("No record is found");
            }
            _response.Result = EmployeerolemapList;
            _response.Count = EmployeerolemapList.Count;
            _logger.LogInfo($"Total Gemder count: {EmployeerolemapList.Count}");
            return _response;
        }

        // GET api/<EmployeerolemapController>/5
        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Successful response", Type = typeof(Employeerolemap))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad Request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal Server Error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public ResponseDTO Get(int id)
        {
            _logger.LogInfo($"Fetching All Employee rolemap List by Id: {id}");
            Employeerolemap Employeerolemap = _unitOfWork.Employeerolemap.Get(u => u.Id == id);
            if (Employeerolemap == null)
            {
                _logger.LogError($"No result found by this Id: {id}");
            }
            _response.Result = Employeerolemap;
            return _response;
        }

        // POST api/<EmployeerolemapController>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, Description = "Item created successfully", Type = typeof(EmployeerolemapResponseModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad Request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, Description = "Unprocessable entity")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal Server Error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public EmployeerolemapResponseModel PostPost([FromBody] EmployeerolemapRequestModel request)
        {
            var Employeerolemap = new Employeerolemap
            {
                EmployeeId = request.EmployeeId,
                RoleId = request.RoleId,
                IsActive = request.IsActive,
                CreatedByEmployeeId = request.CreatedByEmployeeId,
                CreatedOnUtc = request.CreatedOnUtc,
                UpdatedByEmployeeId = request.UpdatedByEmployeeId,
                UpdatedOnUtc = request.UpdatedOnUtc
            };

            _unitOfWork.Employeerolemap.Add(Employeerolemap);
            _unitOfWork.Save();

            _responseModel.Id = Employeerolemap.Id;


            return _responseModel;
        }

        // PUT api/<EmployeerolemapController>/5
        [HttpPut("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Item updated successfully", Type = typeof(EmployeerolemapResponseModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad request")]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content (successful update)")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, Description = "Unprocessable entity")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal server error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public EmployeerolemapResponseModel Put(int id, [FromBody] EmployeerolemapRequestModel request)
        {
            var existingrolemap = _unitOfWork.Employeerolemap.Get(u => u.Id == id);

            if (existingrolemap != null)
            {
                existingrolemap.EmployeeId = request.EmployeeId;
                existingrolemap.RoleId = request.RoleId;
                existingrolemap.IsActive = request.IsActive;
                existingrolemap.UpdatedByEmployeeId = request.UpdatedByEmployeeId;
                existingrolemap.UpdatedOnUtc = request.UpdatedOnUtc;

                _unitOfWork.Employeerolemap.Update(existingrolemap);
                _unitOfWork.Save();

                _responseModel.Id = existingrolemap.Id;
                _responseModel.IsActive = existingrolemap.IsActive;
            }
            else
            {
                _logger.LogError($"No result found by this Id: {id}");
                _responseModel.Id = 0;
                _responseModel.IsActive = false;
            }
            return _responseModel;
        }

        // DELETE api/<EmployeerolemapController>/5
        [HttpDelete("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Item deleted successfully", Type = typeof(EmployeerolemapResponseModel))]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content (successful deletion)")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal server error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public void Delete(int id)
        {
            Employeerolemap? obj = _unitOfWork.Employeerolemap.Get(u => u.Id == id);
            if (obj != null)
            {
                _unitOfWork.Employeerolemap.Remove(obj);
                _unitOfWork.Save();
            }
            else {
                _logger.LogError($"No result found by this Id: {id}");
            }
            
            
        }
    }
}

