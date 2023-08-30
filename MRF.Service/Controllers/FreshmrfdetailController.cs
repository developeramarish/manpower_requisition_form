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
    public class FreshmrfdetailController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private ResponseDTO _response;
        private FreshmrfdetailResponseModel _responseModel;
        private readonly ILoggerService _logger;
        public FreshmrfdetailController(IUnitOfWork unitOfWork, ILoggerService logger)
        {
            _unitOfWork = unitOfWork;
            _response = new ResponseDTO();
            _responseModel = new FreshmrfdetailResponseModel();
            _logger = logger;
        }
        // GET: api/<FreshmrfdetailController>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Successful response", Type = typeof(IEnumerable<Freshmrfdetail>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad Request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal Server Error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public ResponseDTO Get()
        {
            _logger.LogInfo("Fetching All Fresh mr");
            List<Freshmrfdetail> FreshmrtList = _unitOfWork.Freshmrfdetail.GetAll().ToList();
            if (FreshmrtList == null)
            {
                _logger.LogError("No record is found");
            }
            _response.Result = FreshmrtList;
            _logger.LogInfo($"Total Gemder count: {FreshmrtList.Count}");
            return _response;
        }

        // GET api/<FreshmrfdetailController>/5
        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Successful response", Type = typeof(Freshmrfdetail))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad Request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal Server Error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public ResponseDTO Get(int id)
        {
            _logger.LogInfo($"Fetching All Fresh mr by Id: {id}");
            Freshmrfdetail Freshmrfdetail = _unitOfWork.Freshmrfdetail.Get(u => u.Id == id);
            if (Freshmrfdetail == null)
            {
                _logger.LogError($"No result found by this Id: {id}");
            }
            _response.Result = Freshmrfdetail;
            return _response;
        }

        // POST api/<FreshmrfdetailController>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, Description = "Item created successfully", Type = typeof(FreshmrfdetailResponseModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad Request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, Description = "Unprocessable entity")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal Server Error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public FreshmrfdetailResponseModel PostPost([FromBody] FreshmrfdetailRequestModel request)
        {
            var Freshmr = new Freshmrfdetail
            {
                MrfId = request.MrfId,
                Justification = request.Justification,
                SoftwaresRequired = request.SoftwaresRequired,
                HardwaresRequired = request.HardwaresRequired,
                MinTargetSalary = request.MinTargetSalary,
                MaxTargetSalary = request.MaxTargetSalary,
                CreatedByEmployeeId = request.CreatedByEmployeeId,
                CreatedOnUtc = request.CreatedOnUtc,
                UpdatedByEmployeeId = request.UpdatedByEmployeeId,
                UpdatedOnUtc = request.UpdatedOnUtc
            };

            _unitOfWork.Freshmrfdetail.Add(Freshmr);
            _unitOfWork.Save();

            _responseModel.Id = Freshmr.Id;
      

            return _responseModel;
        }

        // PUT api/<FreshmrfdetailController>/5
        [HttpPut("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Item updated successfully", Type = typeof(FreshmrfdetailResponseModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad request")]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content (successful update)")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, Description = "Unprocessable entity")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal server error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public FreshmrfdetailResponseModel Put(int id, [FromBody] FreshmrfdetailRequestModel request)
        {
            var existingFreshmr = _unitOfWork.Freshmrfdetail.Get(u => u.Id == id);

            if (existingFreshmr != null)
            {
                existingFreshmr.MrfId = request.MrfId;
                existingFreshmr.Justification = request.Justification;
                existingFreshmr.SoftwaresRequired=request.SoftwaresRequired;
                existingFreshmr.HardwaresRequired = request.HardwaresRequired;
                existingFreshmr.MinTargetSalary = request.MinTargetSalary;
                existingFreshmr.MaxTargetSalary=request.MaxTargetSalary;
                existingFreshmr.UpdatedByEmployeeId = request.UpdatedByEmployeeId;
                existingFreshmr.UpdatedOnUtc = request.UpdatedOnUtc;

                _unitOfWork.Freshmrfdetail.Update(existingFreshmr);
                _unitOfWork.Save();

                _responseModel.Id = existingFreshmr.Id;
               
            }
            else
            {
                _logger.LogError($"No result found by this Id: {id}");
                _responseModel.Id = 0;
              
            }
            return _responseModel;
        }

        // DELETE api/<FreshmrfdetailController>/5
        [HttpDelete("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Item deleted successfully", Type = typeof(FreshmrfdetailResponseModel))]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content (successful deletion)")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal server error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public void Delete(int id)
        {
            Freshmrfdetail? obj = _unitOfWork.Freshmrfdetail.Get(u => u.Id == id);
            if (obj == null)
            {
                _logger.LogError($"No result found by this Id: {id}");
            }
            _unitOfWork.Freshmrfdetail.Remove(obj);
            _unitOfWork.Save();
        }
    }
}

