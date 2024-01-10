using MRF.Models.DTO;
using MRF.Models.Models;

namespace MRF.DataAccess.Repository.IRepository
{
    public interface IEmailRecipientRepository : IRepository<EmailRecipient>
    {
        public List<string> GetEmailRecipient(int? MrfStatusId = null, string? MrfStatus = null);
    }
}
