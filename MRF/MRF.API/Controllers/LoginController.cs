using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MRF.DataAccess.Repository.IRepository;
using MRF.Models.DTO;
using MRF.Models.Models;
using MRF.Utility;
using Swashbuckle.AspNetCore.Annotations;

namespace MRF.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private ResponseDTO _response;
        private EmployeedetailsResponseModel _responseModel;
        private readonly ILoggerService _logger;
        public LoginController(IUnitOfWork unitOfWork, ILoggerService logger)
        {
            _unitOfWork = unitOfWork;
            _response = new ResponseDTO();
            _responseModel = new EmployeedetailsResponseModel();
            _logger = logger;
        }

        // GET api/<EmployeelogindetailController>/5
        [HttpGet("{Username}")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Successful response", Type = typeof(Employeedetails))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad Request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal Server Error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        [Authorize]
        public ResponseDTO Get(string Username)
        {
            _logger.LogInfo($"Fetching Employee login detail by name: {Username}");
            Employeedetails Employeelogindetail = _unitOfWork.Employeedetails.Get(u => u.Name == Username);
            if (Employeelogindetail == null)
            {
                _logger.LogError($"Login Failed: {Username}");
            }
            _response.Result = Employeelogindetail;
            return _response;
        }
    }
}
