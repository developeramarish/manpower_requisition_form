using MRF.Models.Models;


namespace MRF.DataAccess.Repository.IRepository
{
    public interface ICandidatedetailRepository: IRepository<Candidatedetail>
    {
        public void Update(Candidatedetail candidatedetail);
    }
}
