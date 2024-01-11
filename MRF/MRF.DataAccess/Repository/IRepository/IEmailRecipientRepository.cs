using MRF.Models.Models;

namespace MRF.DataAccess.Repository.IRepository
{
    public interface IEmailRecipientRepository : IRepository<EmailRecipient>
    {
        public List<EmailRecipient> GetEmailRecipient(int? MrfStatusId = null, string? MrfStatus = null);
    }
}
