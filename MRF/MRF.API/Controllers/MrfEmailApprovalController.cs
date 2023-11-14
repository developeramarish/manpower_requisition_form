using Microsoft.AspNetCore.Mvc;
using MRF.DataAccess.Repository.IRepository;
using MRF.Models.DTO;
using MRF.Models.Models;
using MRF.Utility;
using Swashbuckle.AspNetCore.Annotations;

namespace MRF.API.Controllers
{
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
        [SwaggerResponse(StatusCodes.Status201Created, Description = "Item created successfully", Type = typeof(MrfdetaiResponseModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Bad Request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "Forbidden")]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, Description = "Unprocessable entity")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Internal Server Error")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Description = "Service Unavailable")]
        public MrfEmailApprovalResponseModel Post([FromBody] MrfEmailApprovalRequestModel request)
        {
            var MrfEmailApproval = new MrfEmailApproval
            {
                MrfId=request.MrfId,
                EmployeeId=request.EmployeeId,
                ApprovalDate=request.ApprovalDate,
                
            };

            _unitOfWork.MrfEmailApproval.Add(MrfEmailApproval);
            _unitOfWork.Save();
            _responseModel.Id = MrfEmailApproval.Id;
            return _responseModel;
        }
    }
}
