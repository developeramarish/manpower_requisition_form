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
    }
}
