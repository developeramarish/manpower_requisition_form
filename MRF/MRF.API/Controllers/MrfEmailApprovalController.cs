using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MRF.DataAccess.Repository.IRepository;
using MRF.Models.DTO;
using MRF.Models.Models;
using MRF.Utility;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Ocsp;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;

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
        [HttpGet("GetListBymrfIdandEmployeeId")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Successful response", Type = typeof(List<MrfEmailApproval>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad Request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal Server Error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public MrfEmailApproval GetListBymrfIdandEmployeeId(int mrfId, int empId)
        {
            _logger.LogInfo($"Fetching All MRF Details by Id: {mrfId}");
             MrfEmailApproval  MrfEmailApproval = _unitOfWork.MrfEmailApproval.GetListBymrfIdandEmployeeId(mrfId, empId);
            if (MrfEmailApproval == null)
            {
                _logger.LogError($"No result found by this Id:{mrfId}");
            }
            _response.Result = MrfEmailApproval;
            return MrfEmailApproval;
        }

        [HttpPut("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Item updated successfully", Type = typeof(MrfEmailApprovalResponseModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad request")]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content (successful update)")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not Found")]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, Description = "Unprocessable entity")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal server error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public MrfEmailApprovalResponseModel Put(int id, [FromBody] MrfEmailApprovalRequestModel request) {
            var existingStatus = _unitOfWork.MrfEmailApproval.Get(u => u.Id == id);
            if (existingStatus != null)
            {
                existingStatus.ApprovalDate = request.ApprovalDate;
                    _unitOfWork.MrfEmailApproval.Update(existingStatus);
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

    }
}
