using MRF.DataAccess.Repository.IRepository;
using MRF.Models.Models;

namespace MRF.DataAccess.Repository
{
    public class ReplacementmrfdetailRepository : Repository<Replacementmrfdetails>, IReplacementmrfdetailRepository
    {
        private readonly Data.MRFDBContext _db;
        public ReplacementmrfdetailRepository(Data.MRFDBContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Replacementmrfdetails replacementmrfdetail)
        {
            _db.Replacementmrfdetails.Update(replacementmrfdetail);
        }
    }
}
