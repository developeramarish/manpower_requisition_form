using MRF.Models.Models;


namespace MRF.DataAccess.Repository.IRepository
{
    public interface ICandidatedetailRepository: IRepository<Candidatedetails>
    {
        public void Update(Candidatedetails candidatedetail);
    }
}
