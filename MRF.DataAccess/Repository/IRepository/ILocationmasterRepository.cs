using MRF.Models.Models;

namespace MRF.DataAccess.Repository.IRepository
{
    public interface ILocationmasterRepository : IRepository<Locationmaster>
    {
        public void Update(Locationmaster locationmaster);
    }
}
