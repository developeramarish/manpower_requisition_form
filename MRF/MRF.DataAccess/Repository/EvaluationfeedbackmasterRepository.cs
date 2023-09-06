using MRF.DataAccess.Repository.IRepository;
using MRF.Models.Models;

namespace MRF.DataAccess.Repository
{
    public class EvaluationfeedbackmasterRepository : Repository<Evaluationfeedbackmaster>, IEvaluationfeedbackmasterRepository
    {
        private readonly Data.MRFDBContext _db;
        public EvaluationfeedbackmasterRepository(Data.MRFDBContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Evaluationfeedbackmaster evaluationfeedbackmaster)
        {
            _db.Evaluationfeedbackmaster.Update(evaluationfeedbackmaster);
        }
    }
}
