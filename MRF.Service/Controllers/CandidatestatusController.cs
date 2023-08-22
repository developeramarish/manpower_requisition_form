using Microsoft.AspNetCore.Mvc;
using MRF.DataAccess.Repository.IRepository;
using MRF.Models.DTO;
using MRF.Models.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MRF.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatestatusController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private ResponseDTO _response;
        private CandidatestatusmasterResponseModel _responseModel;
        public CandidatestatusController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _response = new ResponseDTO();
            _responseModel= new CandidatestatusmasterResponseModel();
        }
        // GET: api/<CandidatestatusController>
        [HttpGet]
        public ResponseDTO Get()
        {
            try
            {
                List<Candidatestatusmaster> obj = _unitOfWork.Candidatestatusmaster.GetAll().ToList();
                _response.Result = obj;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        // GET api/<CandidatestatusController>/5
        [HttpGet("{Id}")]
        public ResponseDTO Get(int Id)
        {
            try
            {
                Candidatestatusmaster candidatestatusmaster = _unitOfWork.Candidatestatusmaster.Get(u => u.Id == Id);
                _response.Result = candidatestatusmaster;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }


            return _response;
        }

        // POST api/<CandidatestatusController>
        [HttpPost]
        public CandidatestatusmasterResponseModel Post([FromBody] CandidatestatusmasterRequestModel request)
        {
            try
            {
                var candidateStatus = new Candidatestatusmaster
                {
                    Status = request.Status,
                    IsActive = request.IsActive,
                    CreatedByEmployeeId = request.CreatedByEmployeeId,
                    CreatedOnUtc = request.CreatedOnUtc,
                    UpdatedByEmployeeId = request.UpdatedByEmployeeId,
                    UpdatedOnUtc = request.UpdatedOnUtc
                };

                _unitOfWork.Candidatestatusmaster.Add(candidateStatus);
                _unitOfWork.Save();

                _responseModel.Id = candidateStatus.Id;
                _responseModel.Status = candidateStatus.Status;
                _responseModel.IsActive = candidateStatus.IsActive;              
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _responseModel;
        }

        // PUT api/<CandidatestatusController>/5
        [HttpPut("{id}")]
        public CandidatestatusmasterResponseModel Put(int id, [FromBody] CandidatestatusmasterRequestModel request)
        {
            try
            {
                var existingStatus = _unitOfWork.Candidatestatusmaster.Get(u => u.Id == id);
                existingStatus.Status = request.Status;
                existingStatus.IsActive = request.IsActive;
                existingStatus.UpdatedByEmployeeId = request.UpdatedByEmployeeId;
                existingStatus.UpdatedOnUtc = request.UpdatedOnUtc;

                _unitOfWork.Candidatestatusmaster.Update(existingStatus);
                _unitOfWork.Save();

                _responseModel.Id = existingStatus.Id;
                _responseModel.Status = existingStatus.Status;
                _responseModel.IsActive = existingStatus.IsActive;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _responseModel;
        }

        // DELETE api/<CandidatestatusController>/5
        [HttpDelete("{Id}")]
        public void Delete(int Id)
        {
            try
            {
                Candidatestatusmaster? obj = _unitOfWork.Candidatestatusmaster.Get(u => u.Id == Id);
                _unitOfWork.Candidatestatusmaster.Remove(obj);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
        }
    }
}
