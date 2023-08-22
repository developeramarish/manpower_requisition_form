using MRF.Models.Models;

namespace MRF.DataAccess.Repository.IRepository
{
    public interface IEvaluationmasterRepository : IRepository<Evaluationmaster>
    {
        public void Update(Evaluationmaster evaluationmaster);
    }
}
