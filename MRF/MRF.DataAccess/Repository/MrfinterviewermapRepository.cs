using MRF.DataAccess.Repository.IRepository;
using MRF.Models.Models;

namespace MRF.DataAccess.Repository
{
    public class MrfinterviewermapRepository : Repository<Mrfinterviewermap>, IMrfinterviewermapRepository
    {
        private readonly Data.MRFDBContext _db;
        public MrfinterviewermapRepository(Data.MRFDBContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Mrfinterviewermap mrfinterviewermap)
        {
            _db.Mrfinterviewermap.Update(mrfinterviewermap);
        }
    }
}
