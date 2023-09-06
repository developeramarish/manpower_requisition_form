using MRF.Models.Models;

namespace MRF.DataAccess.Repository.IRepository
{
    public interface IReplacementmrfdetailRepository : IRepository<Replacementmrfdetails>
    {
        void Update(Replacementmrfdetails replacementmrfdetail);
    }
}
