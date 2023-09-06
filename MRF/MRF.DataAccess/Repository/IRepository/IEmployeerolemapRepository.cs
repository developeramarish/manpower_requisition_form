using MRF.Models.Models;

namespace MRF.DataAccess.Repository.IRepository
{
    public interface IEmployeerolemapRepository : IRepository<Employeerolemap>
    {
        public void Update(Employeerolemap employeerolemap);
    }
}
