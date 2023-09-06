using MRF.Models.Models;

namespace MRF.DataAccess.Repository.IRepository
{
    public interface IQualificationmasterRepository : IRepository<Qualificationmaster>
    {
        public void Update(Qualificationmaster qualificationmaster);
    }
}
