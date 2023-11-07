using MRF.DataAccess.Repository.IRepository;
using MRF.Models.DTO;
using MRF.Models.Models;
using MRF.Models.ViewModels;
using System.Text.RegularExpressions;

namespace MRF.DataAccess.Repository
{
    public class MrfdetailRepository : Repository<Mrfdetails>, IMrfdetailRepository
    {
        private readonly Data.MRFDBContext _db;
        public MrfdetailRepository(Data.MRFDBContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Mrfdetails mrfdetail)
        {
            _db.Mrfdetails.Update(mrfdetail);
        }
        public MrfdetailRequestModel GetRequisition(int MrfId)
        {
            IQueryable<MrfdetailRequestModel> query = from mrfDetails in _db.Mrfdetails
                                                      join fr in _db.Freshmrfdetails on mrfDetails.Id equals fr.MrfId
                                                      join replacement in _db.Replacementmrfdetails on mrfDetails.Id equals replacement.MrfId into eGroup
                                                      from replacement in eGroup.DefaultIfEmpty()
                                                      where mrfDetails.Id == MrfId
                                                      select new MrfdetailRequestModel
                                                      {
                                                          ReferenceNo = mrfDetails.ReferenceNo,
                                                          PositionTitle = mrfDetails.PositionTitle,
                                                          DepartmentId = mrfDetails.DepartmentId,
                                                          SubDepartmentId = mrfDetails.SubDepartmentId,
                                                          ProjectId = mrfDetails.ProjectId,
                                                          VacancyNo = mrfDetails.VacancyNo,
                                                          GenderId = mrfDetails.GenderId,
                                                          RequisitionDateUtc = mrfDetails.RequisitionDateUtc,
                                                          ReportsToEmployeeId = mrfDetails.ReportsToEmployeeId,
                                                          GradeId = mrfDetails.GradeId,
                                                          EmploymentTypeId = mrfDetails.EmploymentTypeId,
                                                          MinExperience = mrfDetails.MinExperience,
                                                          MaxExperience = mrfDetails.MaxExperience,
                                                          VacancyTypeId = mrfDetails.VacancyTypeId,
                                                          IsReplacement = mrfDetails.IsReplacement,
                                                          MrfStatusId = mrfDetails.MrfStatusId,
                                                          JdDocPath = mrfDetails.JdDocPath,
                                                          LocationId = mrfDetails.LocationId,
                                                          Justification = fr.Justification ?? "",
                                                            SoftwaresRequired = fr.SoftwaresRequired ?? "",
                                                            HardwaresRequired = fr.HardwaresRequired ?? "",
                                                            MinTargetSalary = fr.MinTargetSalary != 0 ? fr.MinTargetSalary : 0,
                                                            MaxTargetSalary = fr.MaxTargetSalary != 0 ? fr.MaxTargetSalary : 0,
                                                            EmployeeName = replacement.EmployeeName ?? "",
                                                            EmployeeCode = replacement != null  ? replacement.EmployeeCode : 0,
                                                            EmailId = replacement != null ? replacement.EmailId : "",
                                                            LastWorkingDate = replacement != null ? replacement.LastWorkingDate:DateOnly.MinValue ,
                                                            ReplaceJustification = replacement.Justification ?? "",
                                                            AnnualCtc = replacement != null ? replacement.AnnualCtc : 0,
                                                            AnnualGross = replacement != null ?  replacement.AnnualGross : 0,
                                                        };

            return query.FirstOrDefault();
        }
    }
}

