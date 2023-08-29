using MRF.Models.Models;

namespace MRF.DataAccess.Repository.IRepository
{
    public interface IMrfdetailRepository : IRepository<Mrfdetails>
    {
        void Update(Mrfdetails mrfdetail);
    }
}
