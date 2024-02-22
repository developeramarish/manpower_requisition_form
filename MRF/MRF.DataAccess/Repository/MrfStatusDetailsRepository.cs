using MRF.DataAccess.Data;
using MRF.DataAccess.Repository.IRepository;
using MRF.Models.ViewModels;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MRF.DataAccess.Repository
{
    
    public class MrfStatusDetailsRepository : Repository<MrfDetailsViewModel>, IMrfStatusDetailsRepository
    {
        private readonly Data.MRFDBContext _db;
        private readonly Data.Utility _Utility;
        public MrfStatusDetailsRepository(Data.MRFDBContext db) : base(db)
        {
            _db = db;
            _Utility = new Data.Utility();
        }
  
        public List<MrfDetailsViewModel> GetMrfStatusDetails(int statusId,int roleId,int userId)
        {
            string Role = _Utility.GetRole(roleId);
            IQueryable<MrfDetailsViewModel> query =
    from mrfDetails in _db.Mrfdetails
    join mrfStatus in _db.Mrfstatusmaster on mrfDetails.MrfStatusId equals mrfStatus.Id
    join mrfRolemap in _db.mrfStatusrolemap on mrfStatus.Id equals mrfRolemap.statusId
    join Emp in _db.Employeedetails on mrfDetails.CreatedByEmployeeId equals Emp.Id
    join Vacancy in _db.Vacancytypemaster on mrfDetails.VacancyTypeId equals Vacancy.Id
    join position in _db.PositionTitlemaster on mrfDetails.PositionTitleId equals position.Id
    // Left join with Freshmrfdetails
    join salary in _db.Freshmrfdetails
         on mrfDetails.Id equals salary.MrfId into freshMrfDetailsGroup
    from freshMrfDetail in freshMrfDetailsGroup.DefaultIfEmpty()
    where mrfRolemap.RoleId == roleId &&
          (statusId == 0 || (statusId != 0 && mrfStatus.Id == statusId)) &&
          (Role != "mrfowner" || (Role == "mrfowner" && mrfDetails.CreatedByEmployeeId == userId)) &&
         (mrfDetails.HrId == null || (Role != "hr" || (Role == "hr" && mrfDetails.HrId == userId)))
    orderby mrfDetails.UpdatedOnUtc descending
    select new MrfDetailsViewModel
    {
        MrfId = mrfDetails.Id,
        ReferenceNo = mrfDetails.ReferenceNo,
        Experience = mrfDetails.MinExperience + "-" + mrfDetails.MaxExperience,
        MrfStatusId = mrfDetails.MrfStatusId,
        MrfStatus = mrfStatus.Status,
        CreatedByEmployeeId = mrfDetails.CreatedByEmployeeId,
        Name = Emp.Name,
        CreatedOnUtc = mrfDetails.CreatedOnUtc,
        UpdatedOnUtc = mrfDetails.UpdatedOnUtc,
        // Check for null before using null-conditional operator
        Salary = freshMrfDetail != null ? freshMrfDetail.MinTargetSalary + "-" + freshMrfDetail.MaxTargetSalary : null,
        VacancyNo = mrfDetails.VacancyNo,
        RequisitionType = mrfDetails.RequisitionType,
        RoleId = mrfRolemap.RoleId,
        positionTitle = position.Name,
        hrId = mrfDetails.HrId,
    };


            return query.ToList();
        }

        
    }

}

