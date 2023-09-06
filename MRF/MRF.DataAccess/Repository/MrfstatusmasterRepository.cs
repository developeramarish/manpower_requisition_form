using MRF.DataAccess.Repository.IRepository;
using MRF.Models.Models;

namespace MRF.DataAccess.Repository
{
    public class MrfstatusmasterRepository : Repository<Mrfstatusmaster>, IMrfstatusmasterRepository
    {
        private readonly Data.MRFDBContext _db;
        public MrfstatusmasterRepository(Data.MRFDBContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Mrfstatusmaster mrfstatusmaster)
        {
            _db.Mrfstatusmaster.Update(mrfstatusmaster);
        }
    }
}
