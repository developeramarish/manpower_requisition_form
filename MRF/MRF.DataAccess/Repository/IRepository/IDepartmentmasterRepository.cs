using MRF.Models.Models;

namespace MRF.DataAccess.Repository.IRepository
{
    public interface IDepartmentmasterRepository : IRepository<Departmentmaster>
    {
        void Update(Departmentmaster departmentmaster);
    }
}
