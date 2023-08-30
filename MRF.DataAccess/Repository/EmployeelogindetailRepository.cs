using MRF.DataAccess.Repository.IRepository;
using MRF.Models.Models;

namespace MRF.DataAccess.Repository
{
    public class EmployeelogindetailRepository:Repository<Employeelogindetail>,IEmployeelogindetailRepository
    {
        private readonly Data.MRFDBContext _db;
        public EmployeelogindetailRepository(Data.MRFDBContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Employeelogindetail employeelogindetail)
        {
            _db.Employeelogindetail.Update(employeelogindetail);
        }
    }
}
