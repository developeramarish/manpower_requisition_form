using MRF.Models.Models;

namespace MRF.DataAccess.Repository.IRepository
{
    public interface IMrffeedbackRepository : IRepository<Mrffeedback>
    {
        void Update(Mrffeedback mrffeedback);
    }
}
