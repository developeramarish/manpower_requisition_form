using MRF.Models.Models;

namespace MRF.DataAccess.Repository.IRepository
{
    public interface IEvaluationstatusmasterRepository : IRepository<Evaluationstatusmaster>
    {
        public void Update(Evaluationstatusmaster evaluationstatusmaster);
    }
}
