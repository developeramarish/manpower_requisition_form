using MRF.DataAccess.Repository.IRepository;
using MRF.Models.Models;

namespace MRF.DataAccess.Repository
{
    public class SubdepartmentmasterRepository : Repository<Subdepartmentmaster>, ISubdepartmentmasterRepository
    {
        private readonly Data.MRFDBContext _db;
        public SubdepartmentmasterRepository(Data.MRFDBContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Subdepartmentmaster subdepartmentmaster)
        {
            _db.Subdepartmentmaster.Update(subdepartmentmaster);
        }
    }
}
