 
using MRF.Models.Models;

namespace MRF.DataAccess.Repository.IRepository
{
    public interface ISourcemasterRepository : IRepository<Sourcemaster>
    {
        public void Update(Sourcemaster  sourcemaster);
    }
}