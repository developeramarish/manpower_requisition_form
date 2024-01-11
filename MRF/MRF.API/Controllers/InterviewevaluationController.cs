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
    public class InterviewevaluationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private ResponseDTO _response;
        private InterviewevaluationResponseModel _responseModel;
        private readonly ILoggerService _logger;
        public InterviewevaluationController(IUnitOfWork unitOfWork, ILoggerService logger)
        {
            _unitOfWork = unitOfWork;
            _response = new ResponseDTO();
            _responseModel = new InterviewevaluationResponseModel();
            _logger = logger;
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
            var interviewevaluation = new Interviewevaluation
            {
                CandidateId = request.CandidateId,
                //EvaluationId = request.EvaluationId,
                InterviewerId = request.InterviewerId,
                EvaluationDateUtc = request.EvaluationDateUtc,
                FromTimeUtc = request.FromTimeUtc,
                ToTimeUtc = request.ToTimeUtc,
               // EvaluationFeedbackId = request.EvaluationFeedbackId,
                EvalutionStatusId = request.EvalutionStatusId,
               // FeedbackAsDraft = request.FeedbackAsDraft,
                CreatedByEmployeeId = request.CreatedByEmployeeId,
                CreatedOnUtc = request.CreatedOnUtc,
                UpdatedByEmployeeId = request.UpdatedByEmployeeId,
                UpdatedOnUtc = request.UpdatedOnUtc
            };

            _unitOfWork.Interviewevaluation.Add(interviewevaluation);
            _unitOfWork.Save();

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

            var existingRecord = _unitOfWork.Interviewevaluation.Get(u => u.Id == id);

            if (existingRecord != null)
            {
                existingRecord.CandidateId = request.CandidateId==0? existingRecord.CandidateId : request.CandidateId;
                //existingRecord.EvaluationId = request.EvaluationId;
                existingRecord.InterviewerId = request.InterviewerId == 0 ? existingRecord.InterviewerId: request.InterviewerId;
                existingRecord.EvaluationDateUtc = request.EvaluationDateUtc==DateOnly.MinValue? existingRecord.EvaluationDateUtc: request.EvaluationDateUtc;
                existingRecord.FromTimeUtc = request.FromTimeUtc == TimeOnly.MinValue ? existingRecord.FromTimeUtc: request.FromTimeUtc;
                existingRecord.ToTimeUtc = request.ToTimeUtc == TimeOnly.MinValue ?existingRecord.ToTimeUtc:request.ToTimeUtc;
               // existingRecord.EvaluationFeedbackId = request.EvaluationFeedbackId;
                existingRecord.EvalutionStatusId = request.EvalutionStatusId == 0 ? existingRecord.EvalutionStatusId : request.EvalutionStatusId;
                //existingRecord.FeedbackAsDraft = request.FeedbackAsDraft;
                existingRecord.UpdatedByEmployeeId = request.UpdatedByEmployeeId;
                existingRecord.UpdatedOnUtc = request.UpdatedOnUtc;

                _unitOfWork.Interviewevaluation.Update(existingRecord);
                _unitOfWork.Save();

                _responseModel.Id = existingRecord.Id;

            }
            else
            {
                _logger.LogError($"No result found by this Id: {id}");

                _responseModel.Id = 0;
                _responseModel.Status = null;
                _responseModel.IsActive = false;
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
