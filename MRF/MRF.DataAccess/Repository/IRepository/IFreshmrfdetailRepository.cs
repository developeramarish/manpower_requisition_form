using MRF.Models.Models;

namespace MRF.DataAccess.Repository.IRepository
{
    public interface IFreshmrfdetailRepository: IRepository<Freshmrfdetails>
    {
        public void Update(Freshmrfdetails freshmrfdetail);
    }
}
