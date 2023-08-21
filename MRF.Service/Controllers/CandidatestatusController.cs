using Microsoft.AspNetCore.Mvc;
using MRF.DataAccess.Repository.IRepository;
using MRF.Models.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MRF.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatestatusController : ControllerBase
    {
        private readonly ICandidatestatusmasterRepository _repository;
        public CandidatestatusController(ICandidatestatusmasterRepository repository)
        {
            _repository = repository;
        }
        // GET: api/<CandidatestatusController>
        [HttpGet]
        public IEnumerable<Candidatestatusmaster> Get()
        {
            List<Candidatestatusmaster> obj = _repository.GetAll().ToList();
            return obj;
        }

        // GET api/<CandidatestatusController>/5
        [HttpGet("{Id}")]
        public Candidatestatusmaster Get(int Id)
        {
            Candidatestatusmaster candidatestatusmaster = _repository.Get(u => u.Id == Id);
            return candidatestatusmaster;
        }

        // POST api/<CandidatestatusController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CandidatestatusController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CandidatestatusController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
