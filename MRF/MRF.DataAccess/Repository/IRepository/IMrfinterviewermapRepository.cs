using MRF.Models.Models;

namespace MRF.DataAccess.Repository.IRepository
{
    public interface IMrfinterviewermapRepository : IRepository<Mrfinterviewermap>
    {
        void Update(Mrfinterviewermap mrfinterviewermap);
    }
}
