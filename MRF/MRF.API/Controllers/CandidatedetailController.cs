using Microsoft.AspNetCore.Mvc;
using MRF.DataAccess.Repository.IRepository;
using MRF.Models.DTO;
using MRF.Models.Models;
using MRF.Utility;
using Swashbuckle.AspNetCore.Annotations;
using System.Xml.Linq;

namespace MRF.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatedetailController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private ResponseDTO _response;
        private CandidatedetailResponseModel _responseModel;
        private readonly ILoggerService _logger;
        private readonly IEmailService _emailService;
        public CandidatedetailController(IUnitOfWork unitOfWork, ILoggerService logger, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _response = new ResponseDTO();
            _responseModel = new CandidatedetailResponseModel();
            _logger = logger;            
            _emailService = emailService;
        }

        // GET: api/<CandidatedetailController>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Successful response", Type = typeof(IEnumerable<Candidatedetails>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad Request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal Server Error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public ResponseDTO Get()
        {
            _logger.LogInfo("Fetching All Candidate detail");
            List<Candidatedetails> obj = _unitOfWork.Candidatedetail.GetAll().ToList();

            if (obj.Count == 0)
            {
                _logger.LogError("No record is found");
            }
            _response.Result = obj;
            _response.Count = obj.Count;
            _logger.LogInfo($"Total Candidate detail count: {_response.Count}");
            _emailService.SendEmailAsync("manish.partey@kwglobal.com", "Test Email", $"Total Candidate detail count: {obj.Count}");
            return _response;
        }

        // GET api/<CandidatedetailController>/5
        [HttpGet("{Id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Successful response", Type = typeof(Candidatedetails))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad Request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal Server Error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public ResponseDTO Get(int Id)
        {
            _logger.LogInfo($"Fetching  Candidate detail by Id: {Id}");
            Candidatedetails Candidatedetail = _unitOfWork.Candidatedetail.Get(u => u.Id == Id);
                if (Candidatedetail == null)
                {
                    
                    _logger.LogError("No result found by this Id:" + Id);
                }
                _response.Result = Candidatedetail;
            
            return _response;
        }

        // POST api/<CandidatedetailController>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, Description = "Item created successfully", Type = typeof(CandidatedetailResponseModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad Request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, Description = "Unprocessable entity")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal Server Error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public CandidatedetailResponseModel Post([FromBody] CandidatedetailRequestModel request)
        {
            
                var Candidatedetail = new Candidatedetails
                {
                    Name = request.Name,
                    MrfId=request.MrfId,
                    EmailId = request.EmailId,
                    ContactNo = request.ContactNo,
                    ResumePath = request.ResumePath,
                    ReviewedByEmployeeId = request.ReviewedByEmployeeId,
                    CandidateStatusId = request.CandidateStatusId,
                    CreatedByEmployeeId = request.CreatedByEmployeeId,
                    CreatedOnUtc = request.CreatedOnUtc,
                    UpdatedByEmployeeId = request.UpdatedByEmployeeId,
                    UpdatedOnUtc = request.UpdatedOnUtc
                };

                _unitOfWork.Candidatedetail.Add(Candidatedetail);
                _unitOfWork.Save();

                _responseModel.Id = Candidatedetail.Id;
                _responseModel.IsActive = true;


            return _responseModel;
        }

        // PUT api/<CandidatedetailController>/5
        [HttpPut("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Item updated successfully", Type = typeof(CandidatedetailResponseModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad request")]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content (successful update)")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, Description = "Unprocessable entity")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal server error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public CandidatedetailResponseModel Put(int id, [FromBody] CandidatedetailRequestModel request)
        {
            
                var existingDetails = _unitOfWork.Candidatedetail.Get(u => u.Id == id);
                if (existingDetails != null)
                {
                    existingDetails.Name = request.Name;
                    existingDetails.EmailId = request.EmailId;
                    existingDetails.ContactNo = request.ContactNo;
                    existingDetails.ResumePath = request.ResumePath;
                    existingDetails.ReviewedByEmployeeId = request.ReviewedByEmployeeId;
                    existingDetails.CandidateStatusId = request.CandidateStatusId;
                    existingDetails.UpdatedByEmployeeId = request.UpdatedByEmployeeId;
                    existingDetails.UpdatedOnUtc = request.UpdatedOnUtc;

                    _unitOfWork.Candidatedetail.Update(existingDetails);
                    _unitOfWork.Save();
                    _responseModel.Id = existingDetails.Id;
                    _responseModel.IsActive = true;
                 }
                else
                {
                    _logger.LogError($"No result found by this Id: {id}");
                    _responseModel.Id = 0;
                    _responseModel.IsActive = false;
                }
              
            return _responseModel;
        }

        // DELETE api/<CandidatedetailController>/5
        [HttpDelete("{Id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Item deleted successfully", Type = typeof(CandidatedetailResponseModel))]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content (successful deletion)")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal server error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public void Delete(int Id)
        {
           
                Candidatedetails? obj = _unitOfWork.Candidatedetail.Get(u => u.Id == Id);
            if (obj != null)
            {
                _unitOfWork.Candidatedetail.Remove(obj);
                _unitOfWork.Save();
            }
            
        }
    }
}

