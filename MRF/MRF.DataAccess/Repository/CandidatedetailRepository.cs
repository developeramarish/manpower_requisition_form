using MRF.DataAccess.Repository.IRepository;
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
    }
}
