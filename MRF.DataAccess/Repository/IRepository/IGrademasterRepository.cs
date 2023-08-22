using MRF.Models.Models;

namespace MRF.DataAccess.Repository.IRepository
{
    public interface IGrademasterRepository : IRepository<Grademaster>
    {
        public void Update(Grademaster grademaster);
    }
}
