using MRF.DataAccess.Repository.IRepository;
using MRF.Models.Models;


namespace MRF.DataAccess.Repository
{
    public class EmployeerolemapRepository: Repository<Employeerolemap>, IEmployeerolemapRepository
    {
        private readonly Data.MRFDBContext _db;
        public EmployeerolemapRepository(Data.MRFDBContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Employeerolemap employeerolemap)
        {
            _db.Employeerolemap.Update(employeerolemap);
        }
        public List<Employeerolemap> GetEmployeebyRole(int roleId)
        {
            string roleIdString = roleId.ToString(); // Convert the roleId to string

            IQueryable<Employeerolemap> query = from emprole in _db.Employeerolemap
                                                join empdetails in _db.Employeedetails on emprole.EmployeeId equals empdetails.Id
                                                where emprole.multipleRoleIds.Contains(roleIdString)
                                                select new Employeerolemap
                                                {
                                                    EmployeeId = emprole.EmployeeId,
                                                    name = empdetails.Name,
                                                    RoleId = emprole.RoleId,
                                                    EmployeeCode = empdetails.EmployeeCode,
                                                };

            return query.ToList();

        }

        public Employeerolemap GetRoleIdFromEmpId(int empCode)
        {
            Employeerolemap employeerolemap = (from emprole in _db.Employeerolemap
                                               join empdetails in _db.Employeedetails on emprole.EmployeeId equals empdetails.Id
                                               where empdetails.EmployeeCode == empCode
                                               select new Employeerolemap
                                               {
                                                   EmployeeId = emprole.EmployeeId,
                                                   name = empdetails.Name,
                                                   RoleId = emprole.RoleId,
                                                   EmployeeCode = empdetails.EmployeeCode,
                                               }).FirstOrDefault();

            return employeerolemap;
        }

    }
}
