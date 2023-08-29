using MRF.DataAccess.Repository.IRepository;
using MRF.Models.Models;

namespace MRF.DataAccess.Repository
{
    public class ResumeforwarddetailRepository : Repository<Resumeforwarddetail>, IResumeforwarddetailRepository
    {
        private readonly Data.MRFDBContext _db;
        public ResumeforwarddetailRepository(Data.MRFDBContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Resumeforwarddetail resumeforwarddetail)
        {
            _db.Resumeforwarddetail.Update(resumeforwarddetail);
        }
    }
}
