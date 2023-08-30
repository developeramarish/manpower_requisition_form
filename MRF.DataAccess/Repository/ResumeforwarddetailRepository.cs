using MRF.DataAccess.Repository.IRepository;
using MRF.Models.Models;

namespace MRF.DataAccess.Repository
{
    public class ResumeforwarddetailRepository : Repository<Resumeforwarddetails>, IResumeforwarddetailRepository
    {
        private readonly Data.MRFDBContext _db;
        public ResumeforwarddetailRepository(Data.MRFDBContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Resumeforwarddetails resumeforwarddetail)
        {
            _db.Resumeforwarddetails.Update(resumeforwarddetail);
        }
    }
}
