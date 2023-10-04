using MRF.DataAccess.Repository.IRepository;
using MRF.Models.Models;
using MRF.Models.ViewModels;

namespace MRF.DataAccess.Repository
{
    public class AttachmentEvaluationRepository: Repository<AttachmentEvaluation>, IAttachmentEvaluationRepository
    {
        private readonly Data.MRFDBContext _db;
        public AttachmentEvaluationRepository(Data.MRFDBContext db) : base(db)
        {
            _db = db;
        }
        public void Update(AttachmentEvaluation AttachmentEvaluation)
        {
            _db.AttachmentEvaluation.Update(AttachmentEvaluation);
        }
    }
}
