using MRF.DataAccess.Repository.IRepository;
using MRF.Models.DTO;
using MRF.Models.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MRF.DataAccess.Repository
{
    public class InterviewevaluationHistoryRepository : Repository<InterviewevaluationHistory>, IInterviewevaluationHistoryRepository
    {

        private readonly Data.MRFDBContext _db;
        public InterviewevaluationHistoryRepository(Data.MRFDBContext db) : base(db)
        {
            _db = db;
        }

        public void Update(InterviewevaluationHistory interviewevaluation)
        {
            _db.InterviewevaluationHistory.Update(interviewevaluation);
        }

       
    }
}








