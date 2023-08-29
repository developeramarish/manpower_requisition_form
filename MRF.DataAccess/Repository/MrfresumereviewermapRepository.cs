using MRF.DataAccess.Repository.IRepository;
using MRF.Models.Models;

namespace MRF.DataAccess.Repository
{
    public class MrfresumereviewermapRepository : Repository<Mrfresumereviewermap>, IMrfresumereviewermapRepository
    {
        private readonly Data.MRFDBContext _db;
        public MrfresumereviewermapRepository(Data.MRFDBContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Mrfresumereviewermap mrfresumereviewermap)
        {
            _db.Mrfresumereviewermap.Update(mrfresumereviewermap);
        }
    }
}
