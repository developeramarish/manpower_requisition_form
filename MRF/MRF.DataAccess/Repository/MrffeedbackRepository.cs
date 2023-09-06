using MRF.DataAccess.Repository.IRepository;
using MRF.Models.Models;

namespace MRF.DataAccess.Repository
{
    public class MrffeedbackRepository : Repository<Mrffeedback>, IMrffeedbackRepository
    {
        private readonly Data.MRFDBContext _db;
        public MrffeedbackRepository(Data.MRFDBContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Mrffeedback mrffeedback )
        {
            _db.Mrffeedback.Update(mrffeedback);
        }
    }
}
