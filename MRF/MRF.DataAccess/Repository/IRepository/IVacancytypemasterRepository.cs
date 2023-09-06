using MRF.Models.Models;

namespace MRF.DataAccess.Repository.IRepository
{
    public interface IVacancytypemasterRepository : IRepository<Vacancytypemaster>
    {
        public void Update(Vacancytypemaster vacancytypemaster);
    }
}
