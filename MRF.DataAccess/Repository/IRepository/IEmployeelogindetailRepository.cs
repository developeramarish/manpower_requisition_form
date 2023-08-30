using MRF.Models.Models;


namespace MRF.DataAccess.Repository.IRepository
{
    public interface IEmployeelogindetailRepository : IRepository<Employeelogindetails>
    {
        public void Update(Employeelogindetails employeelogindetail)
            ;
    }
}
