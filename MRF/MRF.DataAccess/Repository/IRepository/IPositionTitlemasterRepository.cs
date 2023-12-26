
using MRF.Models.Models;

namespace MRF.DataAccess.Repository.IRepository
{
    public interface IPositionTitlemasterRepository : IRepository<PositionTitlemaster>
    {
        public void Update(PositionTitlemaster positionmaster);
    }
}
