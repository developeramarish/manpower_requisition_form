using MRF.DataAccess.Repository.IRepository;
using MRF.Models.Models;
using System.Net.NetworkInformation;

namespace MRF.DataAccess.Repository
{
    public class CandidatestatusmasterRepository : Repository<Candidatestatusmaster>, ICandidatestatusmasterRepository
    {
        private readonly Data.MRFDBContext _db;
        public CandidatestatusmasterRepository(Data.MRFDBContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Candidatestatusmaster candidatestatusmaster)
        {
            _db.Candidatestatusmaster.Update(candidatestatusmaster);
        }
        public List<Candidatestatusmaster> GetCandidatesByResumestatus() {

            List<Candidatestatusmaster> CStatus = (from s in _db.Candidatestatusmaster
                           where s.Status.Contains("resume")
                           select s).ToList();

            return CStatus.ToList();



        }
        public List<Candidatestatusmaster> GetCandidatesByInteriewStatus()
        {

            List<Candidatestatusmaster> CStatus = (from s in _db.Candidatestatusmaster
                                                   where !s.Status.Contains("resume")
                                                   select s).ToList();

            return CStatus.ToList();



        }
    }
}
