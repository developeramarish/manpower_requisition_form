using MRF.DataAccess.Repository.IRepository;
using MRF.Models.Models;

namespace MRF.DataAccess.Repository
{
    public class GrademasterRepository : Repository<Grademaster>, IGrademasterRepository
    {
        private readonly Data.MRFDBContext _db;
        public GrademasterRepository(Data.MRFDBContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Grademaster grademaster)
        {
            _db.Grademaster.Update(grademaster);
        }
    }
}
