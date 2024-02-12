using MRF.DataAccess.Repository.IRepository;
using MRF.Models.DTO;
using MRF.Models.Models;
namespace MRF.DataAccess.Repository
{
    public class CandidatedetailRepository : Repository<Candidatedetails>, ICandidatedetailRepository
    {
        private readonly Data.MRFDBContext _db;
        public CandidatedetailRepository(Data.MRFDBContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Candidatedetails candidatedetail)
        {
            _db.Candidatedetails.Update(candidatedetail);
        }

        public List<Candidatedetails> GetForwardedTodata()
        {
            IQueryable<Candidatedetails> query = from candidatedetails in _db.Candidatedetails
                                                 select candidatedetails;
            List<CandidatedetailRequestModel> resumeR = _db.Candidatedetails
  .Join(_db.Resumeforwarddetails, candidatedetail => candidatedetail.Id, resumer => resumer.CandidateId, (candidatedetail, resumer) => new CandidatedetailRequestModel
  {
      Id = candidatedetail.Id,
      ReviewedByEmployeeId = resumer.ForwardedToEmployeeId
  })
  .GroupBy(mrfDetail => mrfDetail.Id)
  .Select(grouping => new CandidatedetailRequestModel
  {
      Id = grouping.Key,
      ReviewedByEmployeeIds = string.Join(",", grouping.Select(candidatedetail => candidatedetail.ReviewedByEmployeeId))
  })
  .ToList();
            foreach (var r in query)
            {
                foreach (CandidatedetailRequestModel r1 in resumeR)
                {
                    if (r.Id == r1.Id)
                        r.ReviewedByEmployeeIds = r1.ReviewedByEmployeeIds;

                }
            }



            return query.ToList();



        }

        public List<CandidatedetailRequestModel> GetReferenceNoAndPositiontitle()
        {
            IQueryable<CandidatedetailRequestModel> query = from candidatedetails in _db.Candidatedetails
                                                            join mrfdetails in _db.Mrfdetails on
                                                 candidatedetails.MrfId equals mrfdetails.Id
                                                            join position in _db.PositionTitlemaster on mrfdetails.PositionTitleId
                                                equals position.Id
                                                            orderby candidatedetails.UpdatedOnUtc descending
                                                            select new CandidatedetailRequestModel
                                                            {
                                                                Id = candidatedetails.Id,
                                                                Name = candidatedetails.Name,
                                                                EmailId = candidatedetails.EmailId,
                                                                ContactNo = candidatedetails.ContactNo,
                                                                Reason = candidatedetails.Reason,
                                                                ResumePath = candidatedetails.ResumePath,
                                                                CandidateStatusId = candidatedetails.CandidateStatusId,
                                                                CreatedOnUtc = candidatedetails.CreatedOnUtc,
                                                                UpdatedOnUtc = candidatedetails.UpdatedOnUtc,
                                                                SourceId = candidatedetails.SourceId,
                                                                referenceNo = mrfdetails.ReferenceNo,
                                                                Positiontitle = position.Name,

                                                            };



            return query.ToList();



        }

        public int GetStatusOfAllCandidateByMRF(int CandidateId)
        {
            
            int MrfId = (from candidatedetails in _db.Candidatedetails
                                      where candidatedetails.Id == CandidateId
                                      select candidatedetails.MrfId).FirstOrDefault();


            int getCountCandidates = (from candidatedetails in _db.Candidatedetails
                                      where candidatedetails.MrfId == MrfId
                                      select candidatedetails).Distinct().Count();

            int getAllEvaluation = (from candidatedetails in _db.Candidatedetails
                                                            join Interviewevaluation in _db.Interviewevaluation on
                                                            candidatedetails.Id equals Interviewevaluation.Id
                                                            where candidatedetails.MrfId == MrfId
                                                           && (Interviewevaluation.EvalutionStatusId == 29 ||
                                                               Interviewevaluation.EvalutionStatusId == 20 ||
                                                               Interviewevaluation.EvalutionStatusId == 19)
                                                             select candidatedetails).Distinct().Count();
                                                    


            if(getAllEvaluation != getCountCandidates) { MrfId = 0; }
            
            return MrfId;

        }
    }
}
