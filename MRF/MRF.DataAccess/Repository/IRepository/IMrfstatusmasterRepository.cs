using MRF.Models.Models;

namespace MRF.DataAccess.Repository.IRepository
{
    public interface IMrfstatusmasterRepository : IRepository<Mrfstatusmaster>
    {
        public void Update(Mrfstatusmaster mrfstatusmaster);
    }
}
