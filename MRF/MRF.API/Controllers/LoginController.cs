using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using MRF.DataAccess.Repository.IRepository;
using MRF.Models.DTO;
using MRF.Models.Models;
using MRF.Utility;
using Swashbuckle.AspNetCore.Annotations;
using System.Text;


namespace MRF.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private ResponseDTO _response;
        private readonly ILoggerService _logger;
        private readonly IUserService _userService;
        public LoginController(IUnitOfWork unitOfWork, ILoggerService logger, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _response = new ResponseDTO();
            
            _logger = logger;
            _userService = userService;
        }

        
        [HttpGet("emailaddress")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Successful response", Type = typeof(Employeedetails))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad Request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal Server Error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
        [Authorize]
        public ResponseDTO Get(string Emailaddress)
        {
            _logger.LogInfo($"Fetching Employee login detail by name: {Emailaddress}");
            
            _response = _userService.GetRoledetails(true);
            if (_response.Result == null)
            {
                _logger.LogError($"Login Failed:{Emailaddress}");
            }

            return _response;
        }
      
    }
}
