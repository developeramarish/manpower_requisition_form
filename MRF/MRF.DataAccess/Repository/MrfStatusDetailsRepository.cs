using MRF.DataAccess.Repository.IRepository;
using MRF.Models.ViewModels;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MRF.DataAccess.Repository
{
    
    public class MrfStatusDetailsRepository : Repository<MrfDetailsViewModel>, IMrfStatusDetailsRepository
    {
        private readonly Data.MRFDBContext _db;
        public MrfStatusDetailsRepository(Data.MRFDBContext db) : base(db)
        {
            _db = db;
        }
  
        public List<MrfDetailsViewModel> GetMrfStatusDetails(int statusId)
        {
            IQueryable<MrfDetailsViewModel> query  = from mrfDetails in _db.Mrfdetails
                        join mrfStatus in _db.Mrfstatusmaster on mrfDetails.MrfStatusId equals mrfStatus.Id
                        join Emp in _db.Employeedetails on mrfDetails.CreatedByEmployeeId equals Emp.Id
                        join salary in _db.Freshmrfdetails on mrfDetails.Id equals salary.MrfId
                        join Vacancy in _db.Vacancytypemaster on mrfDetails.VacancyTypeId equals Vacancy.Id
                        where (statusId == 0 || (statusId !=0 && mrfStatus.Id == statusId))
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
                            Salary = salary.MinTargetSalary + "-" + salary.MaxTargetSalary,
                            VacancyNo = mrfDetails.VacancyNo,
                            RequisitionType = mrfDetails.RequisitionType,
                        };

            return query.ToList();
        }
    }

}

