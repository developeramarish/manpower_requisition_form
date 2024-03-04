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
                Mrfstatusmaster mrfStatus = _db.Mrfstatusmaster.SingleOrDefault(e => e.Id == MrfStatusId);
                emailMasterQuery = emailMasterQuery.Where(e => e.status == mrfStatus.Status);
            }
            else if (!string.IsNullOrEmpty(MrfStatus))
            {
                emailMasterQuery = emailMasterQuery.Where(e => e.status == MrfStatus);
            }

            var roleId = emailMasterQuery.Select(mrfDetails => mrfDetails.roleId).FirstOrDefault();

            List<int> RoleIds = new List<int>();

            if (!string.IsNullOrEmpty(roleId))
            {
                RoleIds = roleId.Split(',').Select(int.Parse).ToList();
                RoleIds = RoleIds.Where(id => id != 4).ToList(); // Remove RoleId 4 as we have different method to send emails to hr
            }

            IQueryable<EmailRecipient> query = from ed in _db.Employeedetails
                                               join erm in _db.Employeerolemap on ed.Id equals erm.EmployeeId
                                               join mea in _db.MrfEmailApproval on ed.Id equals mea.EmployeeId
                                               where (RoleIds.Count == 0 || RoleIds.Contains(erm.RoleId)) && mea.MrfId == MrfId
                                               select new EmailRecipient
                                               {
                                                   Email = ed.Email
                                               };
            return query.ToList();
        }

        public List<EmailRecipient> GetEmployeeEmailByRoleIds(List<int> roleId)
        {

            IQueryable<EmailRecipient> query = from ed in _db.Employeedetails
                                               where _db.Employeerolemap
                                                         .Where(erm => roleId.Contains(erm.RoleId))
                                                         .Select(erm => erm.EmployeeId)
                                                         .Contains(ed.Id)
                                               select new EmailRecipient
                                               {
                                                   Email = ed.Email
                                               };
            return query.ToList();
        }

        // Get Email Recipients based on RoleId and MrfId
        public List<EmailRecipient> GetEmployeeEmailByRoleIds(List<int> roleId, int mrfId)
        {

            IQueryable<EmailRecipient> query = (from ed in _db.Employeedetails
                                               join erm in _db.Employeerolemap on ed.Id equals erm.EmployeeId
                                               join mea in _db.MrfEmailApproval on ed.Id equals mea.EmployeeId
                                               where roleId.Contains(erm.RoleId) && mea.MrfId == mrfId
                                               select new EmailRecipient
                                               {
                                                   Email = ed.Email
                                               }).Distinct();
            return query.ToList();
        }

        //need to change it for multiple roles
        public List<EmailRecipient> GetEmployeeEmail(string empRole)
        {
            IQueryable<EmailRecipient> query = from ed in _db.Employeedetails
                                               join erm in _db.Employeerolemap on ed.Id equals erm.EmployeeId
                                               where erm.RoleId == (from rm in _db.Rolemaster where rm.Name == empRole select rm.Id).FirstOrDefault()
                                               select new EmailRecipient
                                               {
                                                   Email = ed.Email
                                               };

            return query.ToList();
        }

        //Get Employee Email by Employee Id
        public string getEmail(int id)
        {
            Employeedetails Employeedetail = _db.Employeedetails.FirstOrDefault(u => u.Id == id);

            if (Employeedetail != null)
                return Employeedetail.Email;
            return string.Empty;
        }
    }
}
