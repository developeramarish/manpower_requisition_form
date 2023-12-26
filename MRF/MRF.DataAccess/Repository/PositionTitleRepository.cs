
using MRF.DataAccess.Repository.IRepository;
using MRF.Models.Models;

namespace MRF.DataAccess.Repository
{
    public class PositionTitleRepository : Repository<PositionTitlemaster>, IPositionTitlemasterRepository
    {
        private readonly Data.MRFDBContext _db;
        public PositionTitleRepository(Data.MRFDBContext db) : base(db)
        {
            _db = db;
        }
        public void Update(PositionTitlemaster positiontitle)
        {
            _db.PositionTitlemaster.Update(positiontitle);
        }
    }
}
