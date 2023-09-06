using MRF.Models.Models;

namespace MRF.DataAccess.Repository.IRepository
{
    public interface IRolemasterRepository : IRepository<Rolemaster>
    {
        public void Update(Rolemaster rolemaster);
    }
}
