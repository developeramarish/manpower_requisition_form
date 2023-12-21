using MRF.DataAccess.Repository.IRepository;
using MRF.Models.Models;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Net.NetworkInformation;
using System.Reflection.Metadata.Ecma335;

namespace MRF.DataAccess.Repository
{
    public class EvaluationstatusmasterRepository : Repository<Evaluationstatusmaster>, IEvaluationstatusmasterRepository
    {
        private readonly Data.MRFDBContext _db;
        public EvaluationstatusmasterRepository(Data.MRFDBContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Evaluationstatusmaster evaluationstatusmaster)
        {
            _db.Evaluationstatusmaster.Update(evaluationstatusmaster);
        }


        public List<Interviewstatus> GetStatus()
        {
            List<Evaluationstatusmaster> evaluationList = (from s in _db.Evaluationstatusmaster
                                                           select s).ToList();
            List<Interviewstatus> interviewList = evaluationList
        .Select(evaluationStatus => new Interviewstatus
        {
            Id = evaluationStatus.Id,
            Status = evaluationStatus.Status,
            CandidateorEvalution = "E",
        })
        .ToList();

        //    List<Candidatestatusmaster> CStatus = new List<Candidatestatusmaster>();

        //    CStatus = (from s in _db.Candidatestatusmaster
        //               where !s.Status.Contains("resume")
        //               select s).ToList();

        //    List<Interviewstatus> interviewList2 = CStatus
        //.Select(CStatus => new Interviewstatus
        //{
        //    Id = CStatus.Id,
        //    Status = CStatus.Status,
        //    CandidateorEvalution = "C",
        //})
        //.ToList();
         //   interviewList.AddRange(interviewList2);

            return interviewList;

        }
      
        
       
    }
}
