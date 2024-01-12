using MRF.DataAccess.Repository.IRepository;
using MRF.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRF.DataAccess.Repository
{
    public class CandidateInterviewFeedbackRepository : Repository<CandidateInterviewFeedback>, ICandidateInterviewFeedbackRepository
    {
        private readonly Data.MRFDBContext _db;
        public CandidateInterviewFeedbackRepository(Data.MRFDBContext db) : base(db)
        {
            _db = db;
        }

        public void Update(CandidateInterviewFeedback candidateInterviewFeedback)
        {
            _db.CandidateInterviewFeedback.Update(candidateInterviewFeedback);
        }

        public List<CandidateInterviewFeedback> GetByCandidate(int CandidateId)
        {
            List<CandidateInterviewFeedback> list=(from m in _db.CandidateInterviewFeedback
                                                  join e in _db.Evaluationfeedbackmaster on m.EvaluationFeedBackId equals e.Id
                                                  where m.CandidateId == CandidateId
                                                   select new CandidateInterviewFeedback 
                                                  { Id = m.Id, 
                                                    CandidateId = m.CandidateId,
                                                    EvaluationFeedBack=e.Description,
                                                    InterviewRound=m.InterviewRound,
                                                    Comments=m.Comments,
                                                  }).ToList();

            return list;
        }

    }
}
