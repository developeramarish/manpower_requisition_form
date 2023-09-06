using MRF.Models.Models;

namespace MRF.DataAccess.Repository.IRepository
{
    public interface IMrfresumereviewermapRepository : IRepository<Mrfresumereviewermap>
    {
        void Update(Mrfresumereviewermap mrfresumereviewermap);
    }
}
