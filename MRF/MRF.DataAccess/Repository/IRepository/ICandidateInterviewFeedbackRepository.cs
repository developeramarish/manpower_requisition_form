using MRF.Models.Models;

namespace MRF.DataAccess.Repository.IRepository
{
    public interface ICandidateInterviewFeedbackRepository : IRepository<CandidateInterviewFeedback>
    {
        public void Update(CandidateInterviewFeedback candidateInterviewFeedback);
        public List<CandidateInterviewFeedback> GetByCandidate(int CandidateId);
    }
}
