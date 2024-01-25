using MRF.Models.Models;

namespace MRF.DataAccess.Repository.IRepository
{
    public interface IEmployeerolemapRepository : IRepository<Employeerolemap>
    {
        public void Update(Employeerolemap employeerolemap);
        public List<Employeerolemap> GetEmployeebyRole(int roleId);
        public Employeerolemap GetRoleIdFromEmpId(int empCode);
    }
}
