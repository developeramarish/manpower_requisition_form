using MRF.DataAccess.Repository.IRepository;
using MRF.Models.Models;

namespace MRF.DataAccess.Repository
{
    public class GendermasterRepository : Repository<Gendermaster>, IGendermasterRepository
    {
        private readonly Data.MRFDBContext _db;
        public GendermasterRepository(Data.MRFDBContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Gendermaster gendermaster)
        {
            _db.Gendermaster.Update(gendermaster);
        }
    }
}
