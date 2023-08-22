using MRF.DataAccess.Repository.IRepository;
using MRF.Models.Models;

namespace MRF.DataAccess.Repository
{
    public class EvaluationstatusmasterRepository : Repository<Evaluationstatusmaster>, IEvaluationstatusmasterRepository
    {
        private readonly Data.MRFDBContext _db;
        public EvaluationstatusmasterRepository(Data.MRFDBContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Evaluationstatusmaster evaluationstatusmaster)
        {
            _db.Evaluationstatusmaster.Update(evaluationstatusmaster);
        }
    }
}
