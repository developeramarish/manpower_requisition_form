using MRF.DataAccess.Repository.IRepository;
using MRF.Models.Models;

namespace MRF.DataAccess.Repository
{
    public class EmployeedetailsRepository:Repository<Employeedetails>,IEmployeedetailsRepository
    {
        private readonly Data.MRFDBContext _db;
        public EmployeedetailsRepository(Data.MRFDBContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Employeedetails employeedetails)
        {
            _db.Employeedetails.Update(employeedetails);
        }
    }
}


