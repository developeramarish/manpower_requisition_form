﻿using Microsoft.AspNetCore.Mvc;
using MRF.DataAccess.Repository.IRepository;
using MRF.Models.DTO;
using MRF.Models.Models;
using MRF.Utility;
using Swashbuckle.AspNetCore.Annotations;
using System.Xml.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace MRF.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeedetailsController
    {
        private readonly IUnitOfWork _unitOfWork;
        private ResponseDTO _response;
        private EmployeedetailsResponseModel _responseModel;
        private readonly ILoggerService _logger;
        public EmployeedetailsController(IUnitOfWork unitOfWork, ILoggerService logger)
        {
            _unitOfWork = unitOfWork;
            _response = new ResponseDTO();
            _responseModel = new EmployeedetailsResponseModel();
            _logger = logger;
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


            if (obj == null)
            {
                _logger.LogError("No record is found");
            }
            _response.Result = obj;
            
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
                    Name   = request.Name,
                    Email = request.Email,
                    ContactNo = request.ContactNo,
                    IsAllowed = request.IsAllowed,
                    AllowedByEmployeeId = request.AllowedByEmployeeId,
                    CreatedByEmployeeId = request.CreatedByEmployeeId,
                    CreatedOnUtc = request.CreatedOnUtc,
                    UpdatedByEmployeeId = request.UpdatedByEmployeeId,
                    UpdatedOnUtc = request.UpdatedOnUtc
                };

                _unitOfWork.Employeedetails.Add(employeedetails);
                _unitOfWork.Save();

                _responseModel.Id = employeedetails.Id;
   
            return _responseModel;
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
           
                var existingStatus = _unitOfWork.Employeedetails.Get(u => u.Id == id);
            if (existingStatus != null)
            {
                existingStatus.Name = request.Name;
                existingStatus.Email = request.Email;
                existingStatus.ContactNo = request.ContactNo;
                existingStatus.IsAllowed = request.IsAllowed;
                existingStatus.AllowedByEmployeeId = request.AllowedByEmployeeId;
                existingStatus.CreatedByEmployeeId = request.CreatedByEmployeeId;
                existingStatus.UpdatedByEmployeeId = request.UpdatedByEmployeeId;
                existingStatus.UpdatedOnUtc = request.UpdatedOnUtc;

                _unitOfWork.Employeedetails.Update(existingStatus);
                _unitOfWork.Save();

                _responseModel.Id = existingStatus.Id;
            }
            else
            {
                _logger.LogError($"No result found by this Id: {id}");
                _responseModel.Id = 0;
                _responseModel.IsActive = false;
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
            if (obj == null)
            {
                _logger.LogError($"No result found by this Id: {id}");
            }
            _unitOfWork.Employeedetails.Remove(obj);
                _unitOfWork.Save();
            
            
        }

    }
    
}
