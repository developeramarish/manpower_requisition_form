using MRF.DataAccess.Repository.IRepository;
using MRF.Models.Models;

namespace MRF.DataAccess.Repository
{
    public class FreshmrfdetailRepository: Repository<Freshmrfdetails>, IFreshmrfdetailRepository
    {
        private readonly Data.MRFDBContext _db;
        public FreshmrfdetailRepository(Data.MRFDBContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Freshmrfdetails freshmrfdetail)
        {
            _db.Freshmrfdetails.Update(freshmrfdetail);
        }
    }
}
