using MRF.DataAccess.Repository.IRepository;
using MRF.Models.Models;

namespace MRF.DataAccess.Repository
{
    public class LocationmasterRepository : Repository<Locationmaster>, ILocationmasterRepository
    {
        private readonly Data.MRFDBContext _db;
        public LocationmasterRepository(Data.MRFDBContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Locationmaster locationmaster)
        {
            _db.Locationmaster.Update(locationmaster);
        }
    }
}
