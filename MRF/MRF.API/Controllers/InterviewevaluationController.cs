using Microsoft.AspNetCore.Mvc;
using MRF.DataAccess.Repository.IRepository;
using MRF.Models.DTO;
using MRF.Models.Models;
using MRF.Utility;
using Org.BouncyCastle.Asn1.Ocsp;
using Swashbuckle.AspNetCore.Annotations;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;




// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MRF.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterviewevaluationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private ResponseDTO _response;
        private InterviewevaluationResponseModel _responseModel;
        private readonly ILoggerService _logger;
        private readonly IEmailService _emailService;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly IConfiguration _configuration;
        public InterviewevaluationController(IUnitOfWork unitOfWork, ILoggerService logger, IEmailService emailService, IHostEnvironment hostEnvironment, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _response = new ResponseDTO();
            _responseModel = new InterviewevaluationResponseModel();
            _logger = logger;
            _emailService = emailService;
            _hostEnvironment = hostEnvironment;
            _configuration = configuration;
        }
        // GET: api/<InterviewevaluationController>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Successful response", Type = typeof(IEnumerable<Interviewevaluation>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad Request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal Server Error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public ResponseDTO Get()
        {
            _logger.LogInfo("Fetching All Candidate Status");
            List<Interviewevaluation> InterviewevaluationList = _unitOfWork.Interviewevaluation.GetAll().ToList();
            if (InterviewevaluationList == null)
            {
                _logger.LogError("No record is found");
            }
            _response.Result = InterviewevaluationList;
            _logger.LogInfo($"Total candidate status count: {InterviewevaluationList.Count}");
            return _response;
        }

        // GET api/<InterviewevaluationController>/5
        [HttpGet("{Id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Successful response", Type = typeof(Interviewevaluation))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad Request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal Server Error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public ResponseDTO Get(int Id)
        {

            _logger.LogInfo($"Fetching All Candidate Status by Id: {Id}");
            Interviewevaluation interviewevaluation = _unitOfWork.Interviewevaluation.Get(u => u.Id == Id);
            if (interviewevaluation == null)
            {
                _logger.LogError($"No result found by this Id: {Id}");
            }
            _response.Result = interviewevaluation;
            return _response;
        }

        // POST api/<InterviewevaluationController>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, Description = "Item created successfully", Type = typeof(InterviewevaluationResponseModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad Request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, Description = "Unprocessable entity")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal Server Error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public InterviewevaluationResponseModel Post([FromBody] InterviewevaluationRequestModel request)

        {
            var interviewevaluation = new Interviewevaluation();

            if (!string.IsNullOrEmpty(request.interviewerEmployeeIds))
            {

                List<Interviewevaluation>? obj = _unitOfWork.Interviewevaluation.GetCandidateByCandidateid(request.CandidateId);
                var employeeIds = request.interviewerEmployeeIds.Split(',');
                // Remove employeeIds which exist in obj but not in request.interviewerEmployeeIds
                var employeeIdsInObj = obj.Select(item => item.InterviewerId.ToString()).ToList();
                var employeeIdsToRemove = employeeIdsInObj.Except(employeeIds).Select(int.Parse).ToList();
                Interviewevaluation obj1 = _unitOfWork.Interviewevaluation.Get(u => u.CandidateId == request.CandidateId);

                var candidateDetails = _unitOfWork.Candidatedetail.Get(u => u.Id == request.CandidateId);
                var mrfdetails = _unitOfWork.Mrfdetail.GetRequisition(candidateDetails.MrfId); //gets all mrf details

                //interviewer add email
                emailmaster addEmail = _unitOfWork.emailmaster.Get(u => u.status == "Interviewer added");
                string addContent = addEmail.Content.Replace("MRF ##", $"Resume {candidateDetails.ResumePath.Split("//")[1]}");

                bool newCandidateAdded = !obj.Any();

                foreach (var employeeId in employeeIds)
                {
                    bool employeeIdExists = obj.Any(item => item.InterviewerId == Convert.ToInt32(employeeId));
                    if (!employeeIdExists)
                    {
                        var interviewevaluation1 = new Interviewevaluation();
                        interviewevaluation1.InterviewerId = int.Parse(employeeId);
                        interviewevaluation1.CandidateId = request.CandidateId;
                        interviewevaluation1.EvalutionStatusId = request.EvalutionStatusId == 0 ? null : request.EvalutionStatusId;
                        interviewevaluation1.EvaluationDateUtc = request.EvaluationDateUtc;
                        interviewevaluation1.FromTimeUtc = request.FromTimeUtc;
                        interviewevaluation1.ToTimeUtc = request.ToTimeUtc;
                        interviewevaluation1.CreatedByEmployeeId = request.CreatedByEmployeeId;
                        interviewevaluation1.CreatedOnUtc = request.CreatedOnUtc;
                        interviewevaluation1.UpdatedByEmployeeId = request.UpdatedByEmployeeId;
                        interviewevaluation1.UpdatedOnUtc = request.UpdatedOnUtc;
                        _unitOfWork.Interviewevaluation.Add(interviewevaluation1);
                        _unitOfWork.Save();

                        var emp = _unitOfWork.Employeedetails.Get(u => u.Id == Convert.ToInt32(employeeId));
                        string content = addContent.Replace("You have", $"{emp.Name} has");

                        if (!newCandidateAdded)
                        {
                            //email to interviewer
                            _emailService.SendEmailAsync(emp.Email, addEmail.Subject, addContent);

                            //email only to the current hr which updates the status
                            if (mrfdetails.HrId > 0) _emailService.SendEmailAsync(_unitOfWork.EmailRecipient.getEmail((int)mrfdetails.HrId), addEmail.Subject, content);

                            //email to MRF Owner(hiring manager)
                            if (mrfdetails.HiringManagerId > 0) _emailService.SendEmailAsync(_unitOfWork.EmailRecipient.getEmail(mrfdetails.HiringManagerId), addEmail.Subject, content);
                        }
                        
                        if (obj1 != null)
                        {
                            AttachmentEvaluation attachment = _unitOfWork.AttachmentEvaluation.Get(u => u.InterviewEvaluationId == obj1.Id);
                            if (attachment != null)
                            {
                                attachment.FilePath = attachment.FilePath;
                                attachment.InterviewEvaluationId = interviewevaluation1.Id;

                                attachment.UpdatedByEmployeeId = attachment.UpdatedByEmployeeId;
                                attachment.UpdatedOnUtc = attachment.UpdatedOnUtc;

                                _unitOfWork.AttachmentEvaluation.Update(attachment);
                                _unitOfWork.Save();
                            }

                        }


                    }
                }

                //interviewer remove email
                emailmaster removeEmail = _unitOfWork.emailmaster.Get(u => u.status == "Interviewer removed");
                string removeContent = removeEmail.Content.Replace("#R", $"{candidateDetails.ResumePath.Split("//")[1]}");

                foreach (var employeeIdToRemove in employeeIdsToRemove)
                {
                    var itemToRemove = obj.FirstOrDefault(item => item.InterviewerId == employeeIdToRemove);
                    if (itemToRemove != null)
                    {
                        var emp = _unitOfWork.Employeedetails.Get(u => u.Id == employeeIdToRemove);
                        string content = removeContent.Replace("You have", $"{emp.Name} has");

                        try
                        {

                            _unitOfWork.Interviewevaluation.Remove(itemToRemove);
                            _unitOfWork.Save();

                            //email to interviewer
                            _emailService.SendEmailAsync(emp.Email, removeEmail.Subject, removeContent);

                            //email only to the current hr which updates the status
                            if (mrfdetails.HrId > 0) _emailService.SendEmailAsync(_unitOfWork.EmailRecipient.getEmail((int)mrfdetails.HrId), removeEmail.Subject, content);

                            //email to MRF Owner(hiring manager)
                            if (mrfdetails.HiringManagerId > 0) _emailService.SendEmailAsync(_unitOfWork.EmailRecipient.getEmail(mrfdetails.HiringManagerId), removeEmail.Subject, content);
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
               
            }

            else
            {

                interviewevaluation.InterviewerId = request.InterviewerId;
                interviewevaluation.CandidateId = request.CandidateId;
                interviewevaluation.EvaluationDateUtc = request.EvaluationDateUtc;
                interviewevaluation.FromTimeUtc = request.FromTimeUtc;
                interviewevaluation.EvalutionStatusId = request.EvalutionStatusId;
                interviewevaluation.EvaluationDateUtc = request.EvaluationDateUtc;
                interviewevaluation.FromTimeUtc = request.FromTimeUtc;
                interviewevaluation.ToTimeUtc = request.ToTimeUtc;
                interviewevaluation.EvalutionStatusId = request.EvalutionStatusId;
                interviewevaluation.CreatedByEmployeeId = request.CreatedByEmployeeId;
                interviewevaluation.CreatedOnUtc = request.CreatedOnUtc;
                interviewevaluation.UpdatedByEmployeeId = request.UpdatedByEmployeeId;
                interviewevaluation.UpdatedOnUtc = request.UpdatedOnUtc;
                _unitOfWork.Interviewevaluation.Add(interviewevaluation);
                _unitOfWork.Save();

            }
            _responseModel.Id = interviewevaluation.Id;
            return _responseModel;
        }
        // PUT api/<InterviewevaluationController>/5
        [HttpPut("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Item updated successfully", Type = typeof(InterviewevaluationResponseModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad request")]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content (successful update)")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, Description = "Unprocessable entity")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal server error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public InterviewevaluationResponseModel Put(int id, [FromBody] InterviewevaluationRequestModel request)
        {

            List<Interviewevaluation> record = _unitOfWork.Interviewevaluation.GetA(u => u.CandidateId == request.CandidateId).ToList();
            try
            {
                InterviewevaluationHistoryController controller = new InterviewevaluationHistoryController(_unitOfWork, _logger);
                controller.PostForInterview(record);
            }catch(Exception ex) { _logger.LogError($"No result updated for interviewHistory by this Id: {id}"); }
            if (record.Count > 0)
            {
                emailmaster emailRequest = _unitOfWork.emailmaster.Get(u => u.status == "Interview Status");
                string emailContent = string.Empty;
                if (emailRequest != null)
                {
                    var candidateDetails = _unitOfWork.Candidatedetail.Get(u => u.Id == request.CandidateId);
                    var mrfdetails = _unitOfWork.Mrfdetail.GetRequisition(candidateDetails.MrfId); //gets all mrf details
                    
                    var eStatus = _unitOfWork.Evaluationstatusmaster.Get(u => u.Id == request.EvalutionStatusId);
                    emailContent = emailRequest.Content.Replace("#R", $"{candidateDetails.ResumePath.Split("//")[1]}")
                                                              .Replace("#S", $"{eStatus.Status}");


                    //email only to the current hr which updates the status
                    if (mrfdetails.HrId > 0) _emailService.SendEmailAsync(_unitOfWork.EmailRecipient.getEmail((int)mrfdetails.HrId), emailRequest.Subject, emailContent);

                    //email to MRF Owner(hiring manager)
                    if (mrfdetails.HiringManagerId > 0) _emailService.SendEmailAsync(_unitOfWork.EmailRecipient.getEmail(mrfdetails.HiringManagerId), emailRequest.Subject, emailContent);

                }

                for (int i = 0; i < record.Count; i++)
                {

                    var existingRecord = record[i];


                    if (existingRecord != null)
                    {
                        //existingRecord.CandidateId = request.CandidateId == 0 ? existingRecord.CandidateId : request.CandidateId;
                        //existingRecord.InterviewerId = request.InterviewerId == 0 ? existingRecord.InterviewerId : request.InterviewerId;
                        existingRecord.EvaluationDateUtc = request.EvaluationDateUtc == DateOnly.MinValue ? existingRecord.EvaluationDateUtc : request.EvaluationDateUtc;
                        existingRecord.FromTimeUtc = request.FromTimeUtc == TimeOnly.MinValue ? existingRecord.FromTimeUtc : request.FromTimeUtc;
                        existingRecord.ToTimeUtc = request.ToTimeUtc == TimeOnly.MinValue ? existingRecord.ToTimeUtc : request.ToTimeUtc;
                        existingRecord.EvalutionStatusId = request.EvalutionStatusId == 0 ? existingRecord.EvalutionStatusId : request.EvalutionStatusId;
                        existingRecord.UpdatedByEmployeeId = request.UpdatedByEmployeeId;
                        existingRecord.UpdatedOnUtc = request.UpdatedOnUtc;


                        _unitOfWork.Interviewevaluation.Update(existingRecord);
                        _unitOfWork.Save();

                        //email to interviewer
                        if (existingRecord.InterviewerId > 0 && !string.IsNullOrWhiteSpace(emailContent)) _emailService.SendEmailAsync(_unitOfWork.EmailRecipient.getEmail(existingRecord.InterviewerId), emailRequest.Subject, emailContent);

                        _responseModel.Id = existingRecord.Id;
                        try
                        {
                            if (_responseModel.Id > 0)
                            {
                                CandidatedetailController controller = new CandidatedetailController(_unitOfWork, _logger, null, null,null);
                                int MRFId = controller.GetStatusOfAllCandidateByMRF(existingRecord.CandidateId); //only get mrfid when evalId is offer accepted but not join/offer rejected/onboarded
                                if (MRFId>0)
                                {
                                    DateTime? updatedOnUtc = request.UpdatedOnUtc;

                                    var Mrfdetail = new MrfdetailRequestModel
                                    {
                                        MrfStatusId = 10,
                                        UpdatedByEmployeeId = request.UpdatedByEmployeeId,
                                        UpdatedOnUtc = updatedOnUtc ?? DateTime.MinValue,
                                    };
                                    MrfdetailController Mcontroller = new MrfdetailController(_unitOfWork, _logger, _emailService, _hostEnvironment, _configuration);
                                    var Mrfdetails = Mcontroller.PartialUpdateMRFStatus(MRFId, Mrfdetail);

                                }
                            }
                        }
                        catch (Exception ex) { _logger.LogError($"No result updated for mrf by this Id: {id}"); }

                    }
                    else
                    {
                        _logger.LogError($"No result found by this Id: {id}");

                        _responseModel.Id = 0;
                        _responseModel.Status = null;
                        _responseModel.IsActive = false;
                    }
                }
            }
            else
            {
                Post(request);
            }

            return _responseModel;

        }

        // DELETE api/<InterviewevaluationController>/5
        [HttpDelete("{Id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Item deleted successfully", Type = typeof(InterviewevaluationResponseModel))]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content (successful deletion)")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal server error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public void Delete(int Id)
        {
            Interviewevaluation? obj = _unitOfWork.Interviewevaluation.Get(u => u.Id == Id);
            if (obj == null)
            {
                _logger.LogError($"No result found by this Id: {Id}");
            }
            _unitOfWork.Interviewevaluation.Remove(obj);
            _unitOfWork.Save();
        }
    }
}
