using MRF.DataAccess.Repository.IRepository;
using MRF.Models.Models;

namespace MRF.DataAccess.Repository
{
    public class QualificationmasterRepository : Repository<Qualificationmaster>, IQualificationmasterRepository
    {
        private readonly Data.MRFDBContext _db;
        public QualificationmasterRepository(Data.MRFDBContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Qualificationmaster qualificationmaster)
        {
            _db.Qualificationmaster.Update(qualificationmaster);
        }
    }
}
