using MRF.Models.Models;

namespace MRF.DataAccess.Repository.IRepository
{
    public interface IInterviewevaluationHistoryRepository : IRepository<InterviewevaluationHistory>
    {
        public void Update(InterviewevaluationHistory interviewevaluation);

        


    }
}