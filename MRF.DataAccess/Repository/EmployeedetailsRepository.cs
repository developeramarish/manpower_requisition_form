using MRF.DataAccess.Repository.IRepository;
using MRF.Models.Models;

namespace MRF.DataAccess.Repository
{
    public class EmployeedetailsRepository:Repository<Employeedetail>,IEmployeedetailsRepository
    {
        private readonly Data.MRFDBContext _db;
        public EmployeedetailsRepository(Data.MRFDBContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Employeedetail employeedetails)
        {
            _db.Employeedetail.Update(employeedetails);
        }
    }
}


