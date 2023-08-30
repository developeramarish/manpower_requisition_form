using MRF.DataAccess.Repository.IRepository;
using MRF.Models.Models;

namespace MRF.DataAccess.Repository
{
    public class FreshmrfdetailRepository: Repository<Freshmrfdetail>, IFreshmrfdetailRepository
    {
        private readonly Data.MRFDBContext _db;
        public FreshmrfdetailRepository(Data.MRFDBContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Freshmrfdetail freshmrfdetail)
        {
            _db.Freshmrfdetail.Update(freshmrfdetail);
        }
    }
}
