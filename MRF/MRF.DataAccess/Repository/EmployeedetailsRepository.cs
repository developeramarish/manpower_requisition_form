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
        public List<Employeedetails> GetEmployee(int id)
        {
            IQueryable<Employeedetails> query = from empdetails in _db.Employeedetails
                                                join emprole in _db.Employeerolemap on empdetails.Id equals emprole.EmployeeId
                                                join role in _db.Rolemaster on emprole.RoleId equals role.Id
                                                where (id == 0 || (id != 0 && empdetails.Id == id))
                                                select new Employeedetails
            {
                Id = empdetails.Id,
                Name = empdetails.Name,
                Email = empdetails.Email,
                ContactNo = empdetails.ContactNo,
                RoleName = role.Name,
                RoleId = role.Id,
                EmployeeCode = empdetails.EmployeeCode,

            };
             

            return query.ToList();

        }
    }
}


