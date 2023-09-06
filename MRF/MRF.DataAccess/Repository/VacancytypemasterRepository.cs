using MRF.DataAccess.Repository.IRepository;
using MRF.Models.Models;

namespace MRF.DataAccess.Repository
{
    public class VacancytypemasterRepository : Repository<Vacancytypemaster>, IVacancytypemasterRepository
    {
        private readonly Data.MRFDBContext _db;
        public VacancytypemasterRepository(Data.MRFDBContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Vacancytypemaster vacancytypemaster)
        {
            _db.Vacancytypemaster.Update(vacancytypemaster);
        }
    }
}
