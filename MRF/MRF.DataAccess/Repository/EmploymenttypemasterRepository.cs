using MRF.DataAccess.Repository.IRepository;
using MRF.Models.Models;

namespace MRF.DataAccess.Repository
{
    public class EmploymenttypemasterRepository : Repository<Employmenttypemaster>, IEmploymenttypemasterRepository
    {
        private readonly Data.MRFDBContext _db;
        public EmploymenttypemasterRepository(Data.MRFDBContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Employmenttypemaster employmenttypemaster)
        {
            _db.Employmenttypemaster.Update(employmenttypemaster);
        }
    }
}
