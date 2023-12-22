using MRF.Models.DTO;
using MRF.Models.Models;


namespace MRF.DataAccess.Repository.IRepository
{
    public interface IMrfdetailsPDFRepository : IRepository<MrfdetailsPDF>
    { 
        public MrfdetailsPDFRequestModel GetRequisition(int MrfId);
    }
}
