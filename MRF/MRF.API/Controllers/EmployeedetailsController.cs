using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using MRF.DataAccess.Repository.IRepository;
using MRF.Models.DTO;
using MRF.Models.Models;
using MRF.Utility;
using SendGrid;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Xml.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace MRF.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeedetailsController
    {
        private readonly IUnitOfWork _unitOfWork;
        private ResponseDTO _response;
        private EmployeedetailsResponseModel _responseModel;
        private readonly ILoggerService _logger;
        private readonly IEmailService _emailService;
        private readonly IHostEnvironment _hostEnvironment;

        public EmployeedetailsController(IUnitOfWork unitOfWork, ILoggerService logger, IEmailService emailService, IHostEnvironment hostEnvironment)

        {
            _unitOfWork = unitOfWork;
            _response = new ResponseDTO();
            _responseModel = new EmployeedetailsResponseModel();
            _logger = logger;

            _emailService = emailService;
            _hostEnvironment = hostEnvironment;


        }

        // GET: api/<EmployeedetailsController>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Successful response", Type = typeof(IEnumerable<Employeedetails>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad Request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal Server Error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public ResponseDTO Get()
        {
            _logger.LogInfo("Fetching All Employee details");
            List<Employeedetails> obj = _unitOfWork.Employeedetails.GetAll().ToList();
            _response.Result = obj;


            if (obj.Count == 0)
            {
                _logger.LogError("No record is found");
            }
            _response.Result = obj;
            _response.Count = obj.Count;
            _logger.LogInfo($"Total Employee count: {obj.Count}");
            return _response;


        }

        // GET api/<EmployeedetailsController>/5
        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Successful response", Type = typeof(Employeedetails))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad Request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal Server Error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public ResponseDTO Get(int id)
        {
            _logger.LogInfo($"Fetching Employeedetails by Id: {id}");
            Employeedetails Employeedetail = _unitOfWork.Employeedetails.Get(u => u.Id == id);
            if (Employeedetail == null)
            {

                _logger.LogError("No result found by this Id:" + id);
            }
            _response.Result = Employeedetail;


            return _response;
        }

        // POST api/<EmployeedetailsController>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, Description = "Item created successfully", Type = typeof(Employeedetails))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad Request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, Description = "Unprocessable entity")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal Server Error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public EmployeedetailsResponseModel Post([FromBody] EmployeedetailsRequestModel request)
        {
            var employeedetails = new Employeedetails
            {
                Name = request.Name,
                Email = request.Email,
                ContactNo = request.ContactNo,
                IsAllowed = request.IsAllowed,
                AllowedByEmployeeId = request.AllowedByEmployeeId,
                RoleId = request.RoleId,
                CreatedByEmployeeId = request.CreatedByEmployeeId,
                CreatedOnUtc = request.CreatedOnUtc,
                UpdatedByEmployeeId = request.UpdatedByEmployeeId,
                UpdatedOnUtc = request.UpdatedOnUtc,
                IsDeleted = request.IsDeleted,
                EmployeCode = request.EmployeCode,
            };
            _unitOfWork.Employeedetails.Add(employeedetails);
            _unitOfWork.Save();
            _responseModel.Id = employeedetails.Id;


            if (_hostEnvironment.IsEnvironment("Development") || _hostEnvironment.IsEnvironment("Production"))
            {

                emailmaster emailRequest = _unitOfWork.emailmaster.Get(u => u.status == "Create User");
                if (emailRequest != null)
                {
                    _emailService.SendEmailAsync(emailRequest.emailTo, emailRequest.Subject, emailRequest.Content);
                }
            }

            if (employeedetails.Id != 0)
            {
                CallEmployeeRoleMapController(request, employeedetails.Id);
            }
            else
            {
                _logger.LogError($"Unable to add mrf details");

            }

            return _responseModel;


        }
        private void CallEmployeeRoleMapController(EmployeedetailsRequestModel request, int id)
        {

            var freshmrRequest = new EmployeerolemapRequestModel
            {
                EmployeeId = id,
                RoleId = request.RoleId,
                CreatedByEmployeeId = request.CreatedByEmployeeId,
                CreatedOnUtc = request.CreatedOnUtc,
                UpdatedByEmployeeId = request.UpdatedByEmployeeId,
                UpdatedOnUtc = request.UpdatedOnUtc

            };
            EmployeerolemapController freshmrController = new EmployeerolemapController(_unitOfWork, _logger);
            var freshmrResponse = freshmrController.PostPost(freshmrRequest);



        }
        // PUT api/<EmployeedetailsController>/5
        [HttpPut("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Item updated successfully", Type = typeof(Employeedetails))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad request")]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content (successful update)")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, Description = "Unprocessable entity")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal server error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public EmployeedetailsResponseModel Put(int id, [FromBody] EmployeedetailsRequestModel request)
        {

            List<Employeedetails> existingStatus = _unitOfWork.Employeedetails.GetEmployee(id);
            foreach (Employeedetails existingStatusItem in existingStatus) {
                if (existingStatusItem != null)
                {
                    existingStatusItem.Name = request.Name;
                    existingStatusItem.Email = request.Email;
                    existingStatusItem.ContactNo = request.ContactNo;
                    existingStatusItem.IsAllowed = request.IsAllowed;
                    existingStatusItem.AllowedByEmployeeId = request.AllowedByEmployeeId;
                    existingStatusItem.CreatedByEmployeeId = request.CreatedByEmployeeId;
                    existingStatusItem.UpdatedByEmployeeId = request.UpdatedByEmployeeId;
                    existingStatusItem.UpdatedOnUtc = request.UpdatedOnUtc;
                    existingStatusItem.IsDeleted = request.IsDeleted;
                    existingStatusItem.EmployeCode = request.EmployeCode;
                    _unitOfWork.Employeedetails.Update(existingStatusItem);
                    _unitOfWork.Save();


                    _responseModel.Id = existingStatusItem.Id;

                    if (_hostEnvironment.IsEnvironment("Development") || _hostEnvironment.IsEnvironment("Production"))
                    {

                        emailmaster emailRequest = _unitOfWork.emailmaster.Get(u => u.status == "Update user");
                        if (emailRequest != null)
                        {
                            _emailService.SendEmailAsync(emailRequest.emailTo, emailRequest.Subject, emailRequest.Content);
                        }
                    }


                    if (_responseModel.Id != 0)
                    {
                        CallEmployeeRoleMapController(request, _responseModel.Id);
                    }
                   

                }
                else
                {
                    _logger.LogError($"No result found by this Id: {id}");
                    _responseModel.Id = 0;
                    _responseModel.IsActive = false;
                }
            }



         

            return _responseModel;
        }

        // DELETE api/<EmployeedetailsController>/5
        [HttpDelete("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Item deleted successfully", Type = typeof(Employeedetails))]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content (successful deletion)")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal server error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public void Delete(int id)
        {

            Employeedetails? obj = _unitOfWork.Employeedetails.Get(u => u.Id == id);
            if (obj != null)
            {
                _unitOfWork.Employeedetails.Remove(obj);
                obj.IsDeleted = true;

                _unitOfWork.Save();

                if (_hostEnvironment.IsEnvironment("Development") || _hostEnvironment.IsEnvironment("Production"))
                {

                    emailmaster emailRequest = _unitOfWork.emailmaster.Get(u => u.status == "Delete User");
                    if (emailRequest != null)
                    {
                        _emailService.SendEmailAsync(emailRequest.emailTo, emailRequest.Subject, emailRequest.Content);
                    }
                }


            }
            else
            {
                _logger.LogError($"No result found by this Id: {id}");
            }
        }

        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Successful response", Type = typeof(IEnumerable<Employeedetails>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad Request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal Server Error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public ResponseDTO GetEmployee(int id)
        {
            _logger.LogInfo("Fetching All Employee details");
            List<Employeedetails> obj = _unitOfWork.Employeedetails.GetEmployee(id);
            var r = from l in obj
                    where l.IsDeleted == false
                    select l;
            _response.Result = r;
            return _response;

        }

    }
}
