using MRF.DataAccess.Repository.IRepository;
using MRF.Models.Models;

namespace MRF.DataAccess.Repository
{
    public class EmailRecipientRepository : Repository<EmailRecipient>, IEmailRecipientRepository
    {
        private readonly Data.MRFDBContext _db;

        public EmailRecipientRepository(Data.MRFDBContext db) : base(db)
        {
            _db = db;
        }
        public List<EmailRecipient> GetEmailRecipient(int? MrfStatusId, string? MrfStatus)
        {
            var emailMasterQuery = _db.emailmaster.AsQueryable();

            if (MrfStatusId.HasValue)
            {
                emailMasterQuery = emailMasterQuery.Where(mrfDetails => mrfDetails.statusId == MrfStatusId);
            }
            else if (!string.IsNullOrEmpty(MrfStatus))
            {
                emailMasterQuery = emailMasterQuery.Where(mrfDetails => mrfDetails.status == MrfStatus);
            }

            var roleId = emailMasterQuery.Select(mrfDetails => mrfDetails.roleId).FirstOrDefault();

            List<int> RoleIds = roleId.Split(',').Select(int.Parse).ToList();

            IQueryable<EmailRecipient> query = from ed in _db.Employeedetails
                                               join erm in _db.Employeerolemap on ed.Id equals erm.EmployeeId
                                               join mea in _db.MrfEmailApproval on ed.Id equals mea.EmployeeId
                                               where RoleIds.Contains(erm.RoleId) 
                                               select new EmailRecipient
                                               {
                                                   Email = ed.Email
                                               };
            return query.ToList();
        }

        public List<EmailRecipient> GetEmailRecipient(int? MrfStatusId, string? MrfStatus, int? MrfId)
        {
            var emailMasterQuery = _db.emailmaster.AsQueryable();

            if (MrfStatusId.HasValue)
            {
                emailMasterQuery = emailMasterQuery.Where(mrfDetails => mrfDetails.statusId == MrfStatusId);
            }
            else if (!string.IsNullOrEmpty(MrfStatus))
            {
                emailMasterQuery = emailMasterQuery.Where(mrfDetails => mrfDetails.status == MrfStatus);
            }

            var roleId = emailMasterQuery.Select(mrfDetails => mrfDetails.roleId).FirstOrDefault();

            List<int> RoleIds = roleId.Split(',').Select(int.Parse).ToList();

            IQueryable<EmailRecipient> query = from ed in _db.Employeedetails
                                               join erm in _db.Employeerolemap on ed.Id equals erm.EmployeeId
                                               join mea in _db.MrfEmailApproval on ed.Id equals mea.EmployeeId
                                               where RoleIds.Contains(erm.RoleId) && mea.MrfId == MrfId
                                               select new EmailRecipient
                                               {
                                                   Email = ed.Email
                                               };
            return query.ToList();
        }
    }
}
