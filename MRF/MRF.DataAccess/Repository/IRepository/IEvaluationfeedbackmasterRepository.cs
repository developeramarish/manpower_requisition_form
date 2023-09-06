using MRF.Models.Models;

namespace MRF.DataAccess.Repository.IRepository
{
    public interface IEvaluationfeedbackmasterRepository : IRepository<Evaluationfeedbackmaster>
    {
        void Update(Evaluationfeedbackmaster evaluationfeedbackmaster);
    }
}
