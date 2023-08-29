using MRF.Models.Models;

namespace MRF.DataAccess.Repository.IRepository
{
    public interface IResumeforwarddetailRepository : IRepository<Resumeforwarddetail>
    {
        void Update(Resumeforwarddetail resumeforwarddetail);
    }
}
