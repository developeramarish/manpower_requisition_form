using MRF.Models.Models;

namespace MRF.DataAccess.Repository.IRepository
{
    public interface IEmployeedetailsRepository : IRepository<Employeedetails>
    {
        public void Update(Employeedetails employeedetail);
    }
}




