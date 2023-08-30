using MRF.Models.Models;


namespace MRF.DataAccess.Repository.IRepository
{
    public interface IEmployeelogindetailRepository : IRepository<Employeelogindetail>
    {
        public void Update(Employeelogindetail employeelogindetail)
            ;
    }
}
