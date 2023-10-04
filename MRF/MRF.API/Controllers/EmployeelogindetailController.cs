using Microsoft.AspNetCore.Mvc;
using MRF.DataAccess.Repository.IRepository;
using MRF.Models.DTO;
using MRF.Models.Models;
using MRF.Utility;
using SendGrid;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MRF.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class EmployeelogindetailController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private ResponseDTO _response;
        private EmployeelogindetailResponseModel _responseModel;
        private readonly ILoggerService _logger;
        public EmployeelogindetailController(IUnitOfWork unitOfWork, ILoggerService logger)
        {
            _unitOfWork = unitOfWork;
            _response = new ResponseDTO();
            _responseModel = new EmployeelogindetailResponseModel();
            _logger = logger;
        }
        // GET: api/<EmployeelogindetailController>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Successful response", Type = typeof(IEnumerable<Employeelogindetails>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad Request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal Server Error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public ResponseDTO Get()
        {
            _logger.LogInfo("Fetching All Gemder");
            List<Employeelogindetails> EmployeelogindetailList = _unitOfWork.Employeelogindetail.GetAll().ToList();
            if (EmployeelogindetailList.Count == 0)
            {
                _logger.LogError("No record is found");
            }
            _response.Result = EmployeelogindetailList;
            _response.Count = EmployeelogindetailList.Count;
            _logger.LogInfo($"Total Gemder count: {EmployeelogindetailList.Count}");
            return _response;
        }

        // GET api/<EmployeelogindetailController>/5
        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Successful response", Type = typeof(Employeelogindetails))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad Request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal Server Error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public ResponseDTO Get(int id)
        {
            _logger.LogInfo($"Fetching All Employee login detail by Id: {id}");
            Employeelogindetails Employeelogindetail = _unitOfWork.Employeelogindetail.Get(u => u.Id == id);
            if (Employeelogindetail == null)
            {
                _logger.LogError($"No result found by this Id:{id}");
            }
            _response.Result = Employeelogindetail;
            return _response;
        }

        // POST api/<EmployeelogindetailController>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, Description = "Item created successfully", Type = typeof(EmployeelogindetailResponseModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad Request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, Description = "Unprocessable entity")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal Server Error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public EmployeelogindetailResponseModel PostPost([FromBody] EmployeelogindetailRequestModel request)
        {
            var Employeelogindetail = new Employeelogindetails
            {
                Id = request.Id,
                EmployeeId = request.EmployeeId,

            };

            _unitOfWork.Employeelogindetail.Add(Employeelogindetail);
            _unitOfWork.Save();

            _responseModel.Id = Employeelogindetail.Id;


            return _responseModel;
        }

        // PUT api/<EmployeelogindetailController>/5
        [HttpPut("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Item updated successfully", Type = typeof(EmployeelogindetailResponseModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad request")]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content (successful update)")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, Description = "Unprocessable entity")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal server error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public EmployeelogindetailResponseModel Put(int id, [FromBody] EmployeelogindetailRequestModel request)
        {
            return _responseModel;
        }

        // DELETE api/<EmployeelogindetailController>/5
        [HttpDelete("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Item deleted successfully", Type = typeof(EmployeelogindetailResponseModel))]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content (successful deletion)")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal server error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public void Delete(int id)
        {
            Employeelogindetails? obj = _unitOfWork.Employeelogindetail.Get(u => u.Id == id);
            if (obj != null)
            {
                _unitOfWork.Employeelogindetail.Remove(obj);
                _unitOfWork.Save();
            }
            else
            {
                _logger.LogError($"No result found by this Id: {id}");
            }


        }
    }
}

