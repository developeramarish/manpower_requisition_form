using MRF.Models.Models;

namespace MRF.DataAccess.Repository.IRepository
{
    public interface IEmployeedetailsRepository : IRepository<Employeedetail>
    {
        public void Update(Employeedetail employeedetail);
    }
}




