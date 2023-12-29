using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MRF.DataAccess.Repository.IRepository;
using MRF.Models.DTO;
using MRF.Models.Models;
using MRF.Utility;
using Swashbuckle.AspNetCore.Annotations;

namespace MRF.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MrfEmailApprovalController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private ResponseDTO _response;
        private MrfEmailApprovalResponseModel _responseModel;

        private readonly ILoggerService _logger;

        public MrfEmailApprovalController(IUnitOfWork unitOfWork, ILoggerService logger)
        {
            _unitOfWork = unitOfWork;
            _response = new ResponseDTO();
            _responseModel = new MrfEmailApprovalResponseModel();
            _logger = logger;

        }

        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Item deleted successfully", Type = typeof(MrfEmailApprovalResponseModel))]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content (successful deletion)")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal server error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public MrfEmailApprovalResponseModel Post([FromBody] MrfEmailApprovalRequestModel request)
        {
            var MrfEmailApproval = new MrfEmailApproval
            {
                MrfId = request.MrfId,
                EmployeeId = request.EmployeeId,
                ApprovalDate = request.ApprovalDate,

            };

            _unitOfWork.MrfEmailApproval.Add(MrfEmailApproval);
            _unitOfWork.Save();
            _responseModel.Id = MrfEmailApproval.Id;
            return _responseModel;
        }

        // DELETE api/<MrfinterviewermapController>/5
        [HttpDelete("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Item deleted successfully", Type = typeof(MrfEmailApprovalResponseModel))]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content (successful deletion)")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal server error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public void Delete(int id)
        {
            List<MrfEmailApproval> MrfEmailApproval = _unitOfWork.MrfEmailApproval.GetList(id);
            if (MrfEmailApproval.Count > 0)
            {
                for (int i = 0; i < MrfEmailApproval.Count; i++)
                {
                    _unitOfWork.MrfEmailApproval.Remove(MrfEmailApproval[i]);
                    _unitOfWork.Save();
                }
            }
            else
            {
                _logger.LogError($"No result found by this Id: {id}");
            }

        }

    }
}
