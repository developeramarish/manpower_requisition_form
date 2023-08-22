using MRF.DataAccess.Repository.IRepository;
using MRF.Models.Models;

namespace MRF.DataAccess.Repository
{
    public class EvaluationmasterRepository : Repository<Evaluationmaster>, IEvaluationmasterRepository
    {
        private readonly Data.MRFDBContext _db;
        public EvaluationmasterRepository(Data.MRFDBContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Evaluationmaster evaluationmaster)
        {
            _db.Evaluationmaster.Update(evaluationmaster);
        }
    }
}
