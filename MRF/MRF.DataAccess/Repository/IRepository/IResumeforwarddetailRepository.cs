using MRF.Models.Models;

namespace MRF.DataAccess.Repository.IRepository
{
    public interface IResumeforwarddetailRepository : IRepository<Resumeforwarddetails>
    {
        void Update(Resumeforwarddetails resumeforwarddetail);
    }
}
