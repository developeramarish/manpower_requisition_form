using MRF.Models.Models;

namespace MRF.DataAccess.Repository.IRepository
{
    public interface ISubdepartmentmasterRepository : IRepository<Subdepartmentmaster>
    {
        public void Update(Subdepartmentmaster subdepartmentmaster);
    }
}
