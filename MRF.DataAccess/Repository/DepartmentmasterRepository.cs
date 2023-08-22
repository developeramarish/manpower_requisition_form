using MRF.DataAccess.Repository.IRepository;
using MRF.Models.Models;

namespace MRF.DataAccess.Repository
{
    public class DepartmentmasterRepository : Repository<Departmentmaster>, IDepartmentmasterRepository
    {
        private readonly Data.MRFDBContext _db;
        public DepartmentmasterRepository(Data.MRFDBContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Departmentmaster departmentmaster)
        {
            _db.Departmentmaster.Update(departmentmaster);
        }
    }
}
