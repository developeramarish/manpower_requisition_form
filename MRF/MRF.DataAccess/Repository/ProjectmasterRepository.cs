using MRF.DataAccess.Repository.IRepository;
using MRF.Models.Models;

namespace MRF.DataAccess.Repository
{
    public class ProjectmasterRepository : Repository<Projectmaster>, IProjectmasterRepository
    {
        private readonly Data.MRFDBContext _db;
        public ProjectmasterRepository(Data.MRFDBContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Projectmaster projectmaster)
        {
            _db.Projectmaster.Update(projectmaster);
        }
    }
}
