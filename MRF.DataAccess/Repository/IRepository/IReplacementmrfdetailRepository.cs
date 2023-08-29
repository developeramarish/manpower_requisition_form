using MRF.Models.Models;

namespace MRF.DataAccess.Repository.IRepository
{
    public interface IReplacementmrfdetailRepository : IRepository<Replacementmrfdetail>
    {
        void Update(Replacementmrfdetail replacementmrfdetail);
    }
}
