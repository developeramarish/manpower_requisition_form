using MRF.Models.Models;

namespace MRF.DataAccess.Repository.IRepository
{
    public interface ICandidatestatusmasterRepository : IRepository<Candidatestatusmaster>
    {
        void Update(Candidatestatusmaster candidatestatusmaster);

        public List<Candidatestatusmaster> GetCandidatesByResumestatus();

        public List<Candidatestatusmaster> GetCandidatesByInteriewStatus();
    }
}
