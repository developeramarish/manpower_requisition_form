using MRF.Models.Models;

namespace MRF.DataAccess.Repository.IRepository
{
    public interface IAttachmentEvaluationRepository:IRepository<AttachmentEvaluation>
    {
        public void Update(AttachmentEvaluation AttachmentEvaluation);
    }
}
