using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Matching;
using MRF.DataAccess.Repository;
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
        public CandidatestatusController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: api/<CandidatestatusController>
        [HttpGet]
        public IEnumerable<Candidatestatusmaster> Get()
        {
            List<Candidatestatusmaster> obj = _unitOfWork.Candidatestatusmaster.GetAll().ToList();
            return obj;
        }

        // GET api/<CandidatestatusController>/5
        [HttpGet("{Id}")]
        public Candidatestatusmaster Get(int Id)
        {
            Candidatestatusmaster candidatestatusmaster = _unitOfWork.Candidatestatusmaster.Get(u => u.Id == Id);
            return candidatestatusmaster;
        }

        // POST api/<CandidatestatusController>
        [HttpPost]
        public void Post([FromBody] CandidatestatusmasterRequestModel request)
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

            var response = new CandidatestatusmasterResponseModel
            {
                Id = candidateStatus.Id,
                Status = candidateStatus.Status,
                IsActive = candidateStatus.IsActive
            };

        }

        // PUT api/<CandidatestatusController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] CandidatestatusmasterRequestModel request)
        {
            var existingStatus = _unitOfWork.Candidatestatusmaster.Get(u => u.Id == id);
            existingStatus.Status = request.Status;
            existingStatus.IsActive = request.IsActive;
            existingStatus.UpdatedByEmployeeId = request.UpdatedByEmployeeId;
            existingStatus.UpdatedOnUtc = request.UpdatedOnUtc;

            _unitOfWork.Candidatestatusmaster.Update(existingStatus);
            _unitOfWork.Save();

            var response = new CandidatestatusmasterResponseModel
            {
                Id = existingStatus.Id,
                Status = existingStatus.Status,
                IsActive = existingStatus.IsActive
            };
        }

        // DELETE api/<CandidatestatusController>/5
        [HttpDelete("{Id}")]
        public void Delete(int Id)
        {
            Candidatestatusmaster? obj = _unitOfWork.Candidatestatusmaster.Get(u => u.Id == Id);          
            _unitOfWork.Candidatestatusmaster.Remove(obj);
            _unitOfWork.Save();
        }
    }
}
