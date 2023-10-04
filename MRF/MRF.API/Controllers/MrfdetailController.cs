using Microsoft.AspNetCore.Mvc;
using MRF.DataAccess.Repository;
using MRF.DataAccess.Repository.IRepository;
using MRF.Models.DTO;
using MRF.Models.Models;
using MRF.Models.ViewModels;
using MRF.Utility;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MRF.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MrfdetailController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private ResponseDTO _response;
        private MrfdetaiResponseModel _responseModel;
        private readonly ILoggerService _logger;
        private readonly IEmailService _emailService;

        public MrfdetailController(IUnitOfWork unitOfWork, ILoggerService logger, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _response = new ResponseDTO();
            _responseModel = new MrfdetaiResponseModel();
            _logger = logger;
            _emailService = emailService;
        }

        // GET: api/<MrfdetailController>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Successful response", Type = typeof(IEnumerable<Mrfdetails>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad Request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal Server Error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public ResponseDTO Get()
        {
            _logger.LogInfo("Fetching All MRF Details");
            List<Mrfdetails> mrfdetailsList = _unitOfWork.Mrfdetail.GetAll().ToList();
            if (mrfdetailsList.Count ==  0)
            {
                _logger.LogError("No record is found");
            }
            _response.Result = mrfdetailsList;
            _response.Count=mrfdetailsList.Count;
            _logger.LogInfo($"Total mrf details count: {mrfdetailsList.Count}");
            return _response;
        }

        // GET api/<MrfdetailController>/5
        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Successful response", Type = typeof(Mrfdetails))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad Request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal Server Error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public ResponseDTO Get(int id)
        {
            _logger.LogInfo($"Fetching All MRF Details by Id: {id}");
            Mrfdetails mrfdetail = _unitOfWork.Mrfdetail.Get(u => u.Id == id);
            if (mrfdetail == null)
            {
                _logger.LogError($"No result found by this Id:{id}");
            }
            _response.Result = mrfdetail;
            return _response;
        }

        // POST api/<MrfdetailController>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, Description = "Item created successfully", Type = typeof(MrfdetaiResponseModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad Request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, Description = "Unprocessable entity")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal Server Error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public MrfdetaiResponseModel Post([FromBody] MrfdetailRequestModel request)
        {
            var mrfDetail = new Mrfdetails
            {
                ReferenceNo = request.ReferenceNo,
                PositionTitle = request.PositionTitle,
                DepartmentId = request.DepartmentId,
                SubDepartmentId = request.SubDepartmentId,
                ProjectId = request.ProjectId,
                VacancyNo = request.VacancyNo,
                GenderId = request.GenderId,
                RequisitionDateUtc = request.RequisitionDateUtc,
                ReportsToEmployeeId = request.ReportsToEmployeeId,
                GradeId = request.GradeId,
                EmploymentTypeId = request.EmploymentTypeId,
                MinExperience = request.MinExperience,
                MaxExperience = request.MaxExperience,
                VacancyTypeId = request.VacancyTypeId,
                IsReplacement = request.IsReplacement,
                MrfStatusId = request.MrfStatusId,
                JdDocPath = request.JdDocPath,
                LocationId = request.LocationId,
                CreatedByEmployeeId = request.CreatedByEmployeeId,
                CreatedOnUtc = request.CreatedOnUtc,
                UpdatedByEmployeeId = request.UpdatedByEmployeeId,
                UpdatedOnUtc = request.UpdatedOnUtc
            };

            _unitOfWork.Mrfdetail.Add(mrfDetail);
            _unitOfWork.Save();

            _responseModel.Id = mrfDetail.Id;
            
            
            //_emailService.SendEmailAsync("Submit MRF");

            return _responseModel;
        }

        // PUT api/<MrfdetailController>/5
        [HttpPut("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Item updated successfully", Type = typeof(MrfdetaiResponseModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad request")]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content (successful update)")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, Description = "Unprocessable entity")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal server error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public MrfdetaiResponseModel Put(int id, [FromBody] MrfdetailRequestModel request)
        {
            var existingStatus = _unitOfWork.Mrfdetail.Get(u => u.Id == id);

            if (existingStatus != null)
            {
                existingStatus.ReferenceNo = request.ReferenceNo;
                existingStatus.PositionTitle = request.PositionTitle;
                existingStatus.DepartmentId = request.DepartmentId;
                existingStatus.SubDepartmentId = request.SubDepartmentId;
                existingStatus.ProjectId = request.ProjectId;
                existingStatus.VacancyNo = request.VacancyNo;
                existingStatus.GenderId = request.GenderId;
                existingStatus.RequisitionDateUtc = request.RequisitionDateUtc;
                existingStatus.ReportsToEmployeeId = request.ReportsToEmployeeId;
                existingStatus.GradeId = request.GradeId;
                existingStatus.EmploymentTypeId = request.EmploymentTypeId;
                existingStatus.MinExperience = request.MinExperience;
                existingStatus.MaxExperience = request.MaxExperience;
                existingStatus.VacancyTypeId = request.VacancyTypeId;
                existingStatus.IsReplacement = request.IsReplacement;
                existingStatus.MrfStatusId = request.MrfStatusId;
                existingStatus.JdDocPath = request.JdDocPath;
                existingStatus.LocationId = request.LocationId;
                existingStatus.CreatedByEmployeeId = request.CreatedByEmployeeId;
                existingStatus.CreatedOnUtc = request.CreatedOnUtc;
                existingStatus.UpdatedByEmployeeId = request.UpdatedByEmployeeId;
                existingStatus.UpdatedOnUtc = request.UpdatedOnUtc;

                _unitOfWork.Mrfdetail.Update(existingStatus);
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

        // PUT api/<MrfdetailController>/5
        [HttpPut("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Item updated successfully", Type = typeof(MrfdetaiResponseModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad request")]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content (successful update)")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, Description = "Unprocessable entity")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal server error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public MrfdetaiResponseModel PartialUpdateMRFStatus(int id, [FromBody] MrfdetailRequestModel request)
        {
            var existingStatus = _unitOfWork.Mrfdetail.Get(u => u.Id == id);

            if (existingStatus != null)
            {
                    var entityType = existingStatus.GetType();
                    foreach (var propertyInfo in typeof(MrfdetailRequestModel).GetProperties())
                    {  
                    var entityProperty = entityType.GetProperty(propertyInfo.Name);
                    if (entityProperty != null)
                        {
                            
                            var valueToUpdate = propertyInfo.GetValue(request);
                            if (valueToUpdate != null && !valueToUpdate.Equals(0))
                            {
                                entityProperty.SetValue(existingStatus, valueToUpdate);
                            }
                        }
                    }

                _unitOfWork.Mrfdetail.Update(existingStatus);
                _unitOfWork.Save();
                
                //emailmaster emailRequest = _unitOfWork.emailmaster.Get(u => u.status == "update MRF");
                //_emailService.SendEmailAsync(emailRequest.emailTo,emailRequest.Subject,emailRequest.Content);
                _responseModel.Id = existingStatus.Id;
            }
            else
            {
                _logger.LogError($"No result found by this Id: {id}");
                _responseModel.Id = 0;
            }
            return _responseModel;
        }

        // DELETE api/<MrfdetailController>/5
        [HttpDelete("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Item deleted successfully", Type = typeof(MrfdetaiResponseModel))]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content (successful deletion)")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal server error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public void Delete(int id)
        {
            Mrfdetails? obj = _unitOfWork.Mrfdetail.Get(u => u.Id == id);
            if (obj != null)
            {
                _unitOfWork.Mrfdetail.Remove(obj);
                _unitOfWork.Save();

            }
            else {
                _logger.LogError($"No result found by this Id: {id}");
            }
            
        }


        // GET api/<MrfdetailController>/5
        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Successful response", Type = typeof(MrfDetailsViewModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad Request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal Server Error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public ResponseDTO GetMrfDetails(int id)
        {
            _logger.LogInfo($"Fetching All MRF Details by Id: {id}");
            MrfDetailsViewModel mrfdetail = _unitOfWork.MrfStatusDetail.GetMrfStatusDetails(id);
            if (mrfdetail == null)
            {
                _logger.LogError($"No result found by this Id:{id}");
            }
            _response.Result = mrfdetail;
            return _response;
        }
    }
}
