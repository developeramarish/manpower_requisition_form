using MRF.DataAccess.Repository.IRepository;
using MRF.Models.Models;

namespace MRF.DataAccess.Repository
{
    public class RolemasterRepository : Repository<Rolemaster>, IRolemasterRepository
    {
        private readonly Data.MRFDBContext _db;
        public RolemasterRepository(Data.MRFDBContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Rolemaster rolemaster)
        {
            _db.Rolemaster.Update(rolemaster);
        }
    }
}
