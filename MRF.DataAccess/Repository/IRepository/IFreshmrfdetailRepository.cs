using MRF.Models.Models;

namespace MRF.DataAccess.Repository.IRepository
{
    public interface IFreshmrfdetailRepository: IRepository<Freshmrfdetail>
    {
        public void Update(Freshmrfdetail freshmrfdetail);
    }
}
