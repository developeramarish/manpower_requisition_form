using MRF.Models.Models;

namespace MRF.DataAccess.Repository.IRepository
{
    public interface IEmploymenttypemasterRepository : IRepository<Employmenttypemaster>
    {
        void Update(Employmenttypemaster employmenttypemaster);
    }
}
