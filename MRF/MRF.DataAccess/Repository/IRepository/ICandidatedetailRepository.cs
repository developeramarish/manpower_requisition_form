using MRF.Models.DTO;
using MRF.Models.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace MRF.DataAccess.Repository.IRepository
{
    public interface ICandidatedetailRepository: IRepository<Candidatedetails>
    {
        public void Update(Candidatedetails candidatedetail);

        public List<Candidatedetails> GetForwardedTodata();

        public List<CandidatedetailRequestModel> GetReferenceNoAndPositiontitle();
        public int GetStatusOfAllCandidateByMRF(int candidateId);
    }
}
