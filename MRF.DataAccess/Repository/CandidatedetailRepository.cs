using MRF.DataAccess.Repository.IRepository;
using MRF.Models.Models;
namespace MRF.DataAccess.Repository
{
    public class CandidatedetailRepository : Repository<Candidatedetail>, ICandidatedetailRepository
    {
        private readonly Data.MRFDBContext _db;
        public CandidatedetailRepository(Data.MRFDBContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Candidatedetail candidatedetail)
        {
            _db.Candidatedetail.Update(candidatedetail);
        }
    }
}
