 
using MRF.DataAccess.Repository.IRepository;
using MRF.Models.Models;

namespace MRF.DataAccess.Repository
{
    public class SourceRepository : Repository<Sourcemaster>, ISourcemasterRepository
    {
        private readonly Data.MRFDBContext _db;
        public SourceRepository(Data.MRFDBContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Sourcemaster  sourcemaster)
        {
            _db.Sourcemaster.Update(sourcemaster);
        }
    }
}