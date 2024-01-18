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

            IQueryable<EmailRecipient> query = from MEA in _db.MrfEmailApproval
                                               join ERM in _db.Employeerolemap on MEA.EmployeeId equals ERM.EmployeeId
                                               join ED in _db.Employeedetails on MEA.EmployeeId equals ED.Id
                                               where MEA.MrfId == 1 && RoleIds.Contains(ERM.RoleId)
                                               select new EmailRecipient
                                               {
                                                   Email = ED.Email
                                               };
            return query.ToList();
        }
    }
}
