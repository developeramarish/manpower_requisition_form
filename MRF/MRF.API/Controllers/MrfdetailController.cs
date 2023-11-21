using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using MRF.DataAccess.Repository;
using MRF.DataAccess.Repository.IRepository;
using MRF.Models.DTO;
using MRF.Models.Models;
using MRF.Models.ViewModels;
using MRF.Utility;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System.Diagnostics;
using System.Net;

using static System.Runtime.InteropServices.JavaScript.JSType;

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
        private FreshmrfdetailResponseModel _responseModelf;
        private readonly ILoggerService _logger;
        private readonly IEmailService _emailService;
        private readonly IHostEnvironment _hostEnvironment;
        public MrfdetailController(IUnitOfWork unitOfWork, ILoggerService logger, IEmailService emailService, IHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _response = new ResponseDTO();
            _responseModel = new MrfdetaiResponseModel();
            _responseModelf = new FreshmrfdetailResponseModel();
            _logger = logger;
            _emailService = emailService;
             _hostEnvironment = hostEnvironment;
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
            if (mrfdetailsList.Count == 0)
            {
                _logger.LogError("No record is found");
            }
            _response.Result = mrfdetailsList;
            _response.Count = mrfdetailsList.Count;
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
            _logger.LogInfo($"Post All MRF Details");
            {
                string ReferenceNo = string.Empty;
                if (request.ReferenceNo != "")
                {
                    Put(request.mrfID, request);
                }
                else
                {
                    var result = GenerateMrfReferenceNumber(request);
                    ReferenceNo = result.Reference;
                    MrfLastNumber Number = result.Number;

                    var mrfDetail = Mrfdetail(request, ReferenceNo);

                    _unitOfWork.Mrfdetail.Add(mrfDetail);
                    _unitOfWork.Save();

                    _responseModel.Id = mrfDetail.Id;
                    if (mrfDetail.Id != 0)
                    {
                        CallFreshmrfdetailController(request, mrfDetail.Id);
                        if (Number.LastNumber > 0)
                        {
                            _unitOfWork.MrfLastNo.Update(Number);
                            _unitOfWork.Save();
                        }
                    }
                    else
                    {
                        _logger.LogError($"Unable to add mrf details");

                    }
                }

                //_emailService.SendEmailAsync("Submit MRF");
                
                return _responseModel;
            }
        }

        private Mrfdetails Mrfdetail(MrfdetailRequestModel request,string ReferenceNo)
        {
            var mrfDetail = new Mrfdetails
            {
                ReferenceNo = ReferenceNo,
                PositionTitle = request.PositionTitle,
                RequisitionType = request.RequisitionType,
                DepartmentId = request.DepartmentId,
                SubDepartmentId = request.SubDepartmentId,
                ProjectId = request.ProjectId,
                VacancyNo = request.VacancyNo,
                GenderId = request.GenderId,
                RequisitionDateUtc = request.RequisitionDateUtc,
                ReportsToEmployeeId = request.ReportsToEmployeeId,
                MinGradeId = request.MinGradeId,
                MaxGradeId = request.MaxGradeId,
                EmploymentTypeId = request.EmploymentTypeId,
                MinExperience = request.MinExperience,
                MaxExperience = request.MaxExperience,
                VacancyTypeId = request.VacancyTypeId,
                IsReplacement = request.IsReplacement,
                MrfStatusId = request.MrfStatusId,
                JdDocPath = request.JdDocPath,
                LocationId = request.LocationId,
                QualificationId = request.QualificationId,
                CreatedByEmployeeId = request.CreatedByEmployeeId,
                CreatedOnUtc = request.CreatedOnUtc,
                UpdatedByEmployeeId = request.UpdatedByEmployeeId,
                UpdatedOnUtc = request.UpdatedOnUtc
            };

            return mrfDetail;
        }


        private void CallFreshmrfdetailController(MrfdetailRequestModel request,int mrfId)
        {

            var freshmrRequest = new FreshmrfdetailRequestModel
            {
                MrfId = mrfId,
                Justification = request.Justification,
                JobDescription = request.JobDescription,
                Skills = request.Skills,
                MinTargetSalary = request.MinTargetSalary,
                MaxTargetSalary = request.MaxTargetSalary,
                CreatedByEmployeeId = request.CreatedByEmployeeId,
                CreatedOnUtc = request.CreatedOnUtc,
                UpdatedByEmployeeId = request.UpdatedByEmployeeId,
                UpdatedOnUtc = request.UpdatedOnUtc
            };
            FreshmrfdetailController freshmrController = new FreshmrfdetailController(_unitOfWork, _logger);
            var freshmrResponse = freshmrController.Post(freshmrRequest);


            if (freshmrResponse.Id != 0)
            {
                CallEmailApprovalController(request, mrfId);
                CallReplacementController(request,mrfId);
                CallreviewerController(request, mrfId);
               
            }
            else
            {
                _logger.LogError($"Unable to add mrf details");

            }
        }

        private void CallEmailApprovalController(MrfdetailRequestModel request, int mrfId)
        {
            if (request.HiringManagerId != 0)
            {
                var MrfdetailRequestModelRequest = new MrfEmailApprovalRequestModel
                {
                    MrfId = mrfId,
                    EmployeeId = request.HiringManagerId,
                    ApprovalDate = request.HMApprovalDate
                };
                postMrfEmail(MrfdetailRequestModelRequest);

            }
            if (request.FunctionHeadId != 0)
            {
                var MrfdetailRequestModelRequest = new MrfEmailApprovalRequestModel
                {
                    MrfId = mrfId,
                    EmployeeId = request.FunctionHeadId,
                    ApprovalDate = request.FHApprovalDate
                };
                postMrfEmail(MrfdetailRequestModelRequest);
            }
            if (request.SiteHRSPOCId != 0)
            {
                var MrfdetailRequestModelRequest = new MrfEmailApprovalRequestModel
                {
                    MrfId = mrfId,
                    EmployeeId = request.SiteHRSPOCId,
                    ApprovalDate = request.SPApprovalDate
                };
                postMrfEmail(MrfdetailRequestModelRequest);
            }
            if (request.FinanceHeadId != 0)
            {
                var MrfdetailRequestModelRequest = new MrfEmailApprovalRequestModel
                {
                    MrfId = mrfId,
                    EmployeeId = request.FinanceHeadId,
                    ApprovalDate = request.FIApprovalDate
                };
                postMrfEmail(MrfdetailRequestModelRequest);
            }
            if (request.PresidentnCOOId != 0)
            {
                var MrfdetailRequestModelRequest = new MrfEmailApprovalRequestModel
                {
                    MrfId = mrfId,
                    EmployeeId = request.PresidentnCOOId,
                    ApprovalDate = request.PCApprovalDate
                };
                postMrfEmail(MrfdetailRequestModelRequest);
            }
        }

    private void postMrfEmail(MrfEmailApprovalRequestModel MrfdetailRequestModelRequest)
    {
        MrfEmailApprovalController MrfEmailApprovalController = new MrfEmailApprovalController(_unitOfWork, _logger);
        var MrfEmailApprovalResponse = MrfEmailApprovalController.Post(MrfdetailRequestModelRequest);
    }
        

        private void CallReplacementController(MrfdetailRequestModel request, int mrfId)
        {
            if (request.IsReplacement)
            {
                var ReplacementmrfdetailRequest = new ReplacementmrfdetailRequestModel
                {
                    MrfId = mrfId,
                    EmployeeName = request.EmployeeName,
                    EmailId = request.EmailId,
                    EmployeeCode = request.EmployeeCode,
                    LastWorkingDate = request.LastWorkingDate,
                    AnnualCtc = request.AnnualCtc,
                    AnnualGross = request.AnnualGross,
                    CreatedByEmployeeId = request.CreatedByEmployeeId,
                    CreatedOnUtc = request.CreatedOnUtc,
                    UpdatedByEmployeeId = request.UpdatedByEmployeeId,
                    UpdatedOnUtc = request.UpdatedOnUtc,
                    Justification = request.ReplaceJustification,
                };
                ReplacementmrfdetailController freshmrController = new ReplacementmrfdetailController(_unitOfWork, _logger);
                var ReplacementmrfdetailResponse = freshmrController.Post(ReplacementmrfdetailRequest);

               
            }
            
        }


        private void CallreviewerController(MrfdetailRequestModel request, int mrfId)
        {
            if (!string.IsNullOrEmpty(request.ResumeReviewerEmployeeIds))
            {
                // Split the comma-separated string into an array of IDs
                var employeeIds = request.ResumeReviewerEmployeeIds.Split(',');

                // Create a new MrfresumereviewermapRequestModel for each employee ID
                foreach (var employeeId in employeeIds)
                {
                    var mrfresumereviewermap = new MrfresumereviewermapRequestModel
                    {
                        MrfId = mrfId,
                        ResumeReviewerEmployeeId = int.Parse(employeeId), // Convert the ID to the appropriate type
                        IsActive = true,
                        CreatedByEmployeeId = request.CreatedByEmployeeId,
                        CreatedOnUtc = request.CreatedOnUtc,
                        UpdatedByEmployeeId = request.UpdatedByEmployeeId,
                        UpdatedOnUtc = request.UpdatedOnUtc
                    };

                    MrfresumereviewermapController resumereviewermap = new MrfresumereviewermapController(_unitOfWork, _logger, _emailService, _hostEnvironment);
                    var resumereviewermapResponse = resumereviewermap.Post(mrfresumereviewermap);

                }
            }

            if (!string.IsNullOrEmpty(request.InterviewerEmployeeIds))
            {
                var employeeIds = request.InterviewerEmployeeIds.Split(',');
                foreach (var employeeId in employeeIds)
                {
                    var mrfinterviewermap = new MrfinterviewermapRequestModel
                    {
                        MrfId = mrfId,
                        InterviewerEmployeeId = int.Parse(employeeId),
                        IsActive = true,
                        CreatedByEmployeeId = request.CreatedByEmployeeId,
                        CreatedOnUtc = request.CreatedOnUtc,
                        UpdatedByEmployeeId = request.UpdatedByEmployeeId,
                        UpdatedOnUtc = request.UpdatedOnUtc
                    };

                    MrfinterviewermapController interviewermap = new MrfinterviewermapController(_unitOfWork, _logger, _emailService, _hostEnvironment);
                    var interviewermapResponse = interviewermap.Post(mrfinterviewermap);
                }
            }
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
                existingStatus.MinGradeId = request.MinGradeId;
                existingStatus.MaxGradeId = request.MaxGradeId;
                existingStatus.EmploymentTypeId = request.EmploymentTypeId;
                existingStatus.MinExperience = request.MinExperience;
                existingStatus.MaxExperience = request.MaxExperience;
                existingStatus.VacancyTypeId = request.VacancyTypeId;
                existingStatus.IsReplacement = request.IsReplacement;
                existingStatus.MrfStatusId = request.MrfStatusId;
                existingStatus.JdDocPath = request.JdDocPath;
                existingStatus.LocationId = request.LocationId;
                existingStatus.QualificationId = request.QualificationId;
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
                        if (_emailService.IsValidUpdateValue(valueToUpdate))
                            
                            {
                                entityProperty.SetValue(existingStatus, valueToUpdate);
                            }
                        }
                    }

                _unitOfWork.Mrfdetail.Update(existingStatus);
                _unitOfWork.Save();
                //if (_hostEnvironment.IsEnvironment("Development") || _hostEnvironment.IsEnvironment("Production"))
                //{
                //    emailmaster emailRequest = _unitOfWork.emailmaster.Get(u => u.status == "update MRF");
                //    if (emailRequest != null)
                //    {
                //        _emailService.SendEmailAsync(emailRequest.emailTo, emailRequest.Subject, emailRequest.Content);
                //    }
                //}
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
            List<MrfDetailsViewModel> mrfdetail = _unitOfWork.MrfStatusDetail.GetMrfStatusDetails(id);
            if (mrfdetail == null)
            {
                _logger.LogError($"No result found by this Id:{id}");
            }
            _response.Result = mrfdetail;
            return _response;
        }
        // GET api/<MrfdetailController>/
        [HttpGet("{MrfId}")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Successful response", Type = typeof(MrfDetailsViewModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad Request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal Server Error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public MrfdetailRequestModel GetRequisition(int MrfId)
        {
            _logger.LogInfo($"Fetching All MRF Details by Id: {MrfId}");
            MrfdetailRequestModel mrfdetail = _unitOfWork.Mrfdetail.GetRequisition(MrfId);
            if (mrfdetail == null)
            {
                _logger.LogError($"No result found by this Id:{MrfId}");

                MrfdetailRequestModel blankData=new MrfdetailRequestModel();
                return blankData;
            }
            else
            {
                return mrfdetail;
            }
            
            
        }
        /*
         Reference Number: [Format: No. of positions (in 2 digits) / Location Name (in 3 alphabets)/ 
                            Type (RP/ CRP/ FR/ CFR) / MMM/ YY/ MRF No. (in 3 digits)]
                            Example: 02/ MUM/ CFR/ JAN/ 15/ 003  */
        private (string Reference, MrfLastNumber Number) GenerateMrfReferenceNumber(MrfdetailRequestModel request)
        {
            string Reference=string.Empty;
            _logger.LogInfo("Fetching All MRF Details");
            MrfLastNumber Number = _unitOfWork.MrfLastNo.Get(u => u.Id ==1);
            
            if (Number==null)
            {
                _logger.LogError("No record is found");
            }

            Locationmaster locationmaster = _unitOfWork.Locationmaster.Get(u => u.Id == request.LocationId);
            string month= request.CreatedOnUtc.ToString("MMM").ToUpper(); 
            string Year= request.CreatedOnUtc.ToString("yy");
            string RequisitionType = request.RequisitionType;

            Reference = request.VacancyNo.ToString("D2") + "/ " + locationmaster.ShortCode  + "/ " +RequisitionType+"/ " + month + "/ " + Year + "/ " 
                + (Number.LastNumber++).ToString("D3");
            
            return (Reference, Number);
        }
         

        // GET: api/<ProjectController>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Successful response", Type = typeof(IEnumerable<SwaggerResponseDTO>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad Request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal Server Error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public ResponseDTO GetMRFDropdownlist()
        {
            _logger.LogInfo("Fetching create MRF Dropdown list");
            SwaggerResponseDTO sw = new SwaggerResponseDTO();

            sw.Projects = _unitOfWork.Projectmaster.GetAll().ToList();
            sw.Departments = _unitOfWork.Departmentmaster.GetAll().ToList();
            sw.Grades = _unitOfWork.Grademaster.GetAll().ToList();
            sw.Vaccancies = _unitOfWork.Vacancytypemaster.GetAll().ToList();
            sw.EmploymentTypes = _unitOfWork.Employmenttypemaster.GetAll().ToList();
            sw.location = _unitOfWork.Locationmaster.GetAll().ToList();
            sw.Qualification = _unitOfWork.Qualificationmaster.GetAll().ToList();
            sw.ReportingTo = _unitOfWork.Employeedetails.GetAll().ToList();
            sw.Resumereviewer = _unitOfWork.Employeerolemap.GetEmployeebyRole(5);
            sw.InterviewReviewer = _unitOfWork.Employeerolemap.GetEmployeebyRole(6);
            sw.HiringManager= _unitOfWork.Employeerolemap.GetEmployeebyRole(7);
            sw.FunctionHead = _unitOfWork.Employeerolemap.GetEmployeebyRole(8);
            sw.SiteHRSPOC = _unitOfWork.Employeerolemap.GetEmployeebyRole(9);
            sw.FinanceHead= _unitOfWork.Employeerolemap.GetEmployeebyRole(10);
            sw.PresidentnCOO = _unitOfWork.Employeerolemap.GetEmployeebyRole(11);
            if (sw.Projects.Count == 0 || sw.Departments.Count == 0 || sw.Grades.Count == 0 || sw.Vaccancies.Count == 0 || sw.EmploymentTypes.Count == 0 || sw.location.Count == 0 || sw.Qualification.Count == 0 || sw.ReportingTo.Count == 0)
            {
                _logger.LogError("No record is found");
            }
            var combinedData = new
            {
                sw.Projects,
                sw.Departments,
                sw.Grades,
                sw.Vaccancies,
                sw.EmploymentTypes,
                sw.location,
                sw.Qualification,
                sw.ReportingTo,
                sw.Resumereviewer,
                sw.InterviewReviewer,
                sw.HiringManager ,
                sw.FunctionHead ,
                sw.SiteHRSPOC,
                sw.FinanceHead ,
                sw.PresidentnCOO

            };

            int Count = sw.Projects.Count + sw.Departments.Count + sw.Grades.Count + sw.Vaccancies.Count + sw.EmploymentTypes.Count + sw.location.Count + sw.Qualification.Count + sw.ReportingTo.Count;
            _response.Result = combinedData;
            _response.Count = Count;
            _logger.LogInfo($"Total MRF Dropdown list  count: {Count}");
            return _response;
        }

        public class SwaggerResponseDTO
        {
            public List<Projectmaster> Projects { get; set; } = new List<Projectmaster>();
            public List<Departmentmaster> Departments { get; set; } = new List<Departmentmaster>();
            public List<Grademaster> Grades { get; set; } = new List<Grademaster>();
            public List<Vacancytypemaster> Vaccancies { get; set; } = new List<Vacancytypemaster>();
            public List<Employmenttypemaster> EmploymentTypes { get; set; } = new List<Employmenttypemaster>();
            public List<Locationmaster> location { get; set; } = new List<Locationmaster>();
            public List<Qualificationmaster> Qualification { get; set; } = new List<Qualificationmaster>();
            public List<Employeedetails> ReportingTo { get; set; } = new List<Employeedetails>();
            public List<Employeerolemap> Resumereviewer { get; set; } = new List<Employeerolemap>();//5
            public List<Employeerolemap> InterviewReviewer { get; set; } = new List<Employeerolemap>();//6
            public List<Employeerolemap> HiringManager { get; set; } = new List<Employeerolemap>();//7
            public List<Employeerolemap> FunctionHead { get; set; } = new List<Employeerolemap>();//8
            public List<Employeerolemap> SiteHRSPOC { get; set; } = new List<Employeerolemap>();//9
            public List<Employeerolemap> FinanceHead { get; set; } = new List<Employeerolemap>();//10

            public List<Employeerolemap> PresidentnCOO { get; set; } = new List<Employeerolemap>();//11
          
        }



    }
}
