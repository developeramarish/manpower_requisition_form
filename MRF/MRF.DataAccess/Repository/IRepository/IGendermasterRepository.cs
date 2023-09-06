using MRF.Models.Models;

namespace MRF.DataAccess.Repository.IRepository
{
    public interface IGendermasterRepository : IRepository<Gendermaster>
    {
        public void Update(Gendermaster gendermaster);
    }
}
