using MRF.Models.Models;

namespace MRF.DataAccess.Repository.IRepository
{
    public interface IEmailRecipientRepository : IRepository<EmailRecipient>
    {
        public List<EmailRecipient> GetEmailRecipient(int? MrfStatusId = null, string? MrfStatus = null);
        public List<EmailRecipient> GetEmailRecipient(int? MrfStatusId = null, string? MrfStatus = null, int? MrfId=null);
        public List<EmailRecipient> GetAllHrEmail(string empRole);
    }
}
