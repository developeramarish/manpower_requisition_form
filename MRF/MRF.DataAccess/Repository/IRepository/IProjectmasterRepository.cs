using MRF.Models.Models;

namespace MRF.DataAccess.Repository.IRepository
{
    public interface IProjectmasterRepository : IRepository<Projectmaster>
    {
        public void Update(Projectmaster projectmaster);
    }
}
