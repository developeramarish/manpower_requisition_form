using Microsoft.EntityFrameworkCore;
using MRF.DataAccess.Repository.IRepository;
using MRF.Models.DTO;
using MRF.Models.Models;
using MRF.Models.ViewModels;
using System.Diagnostics;
using System.Net.Http.Headers;
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
            List<MrfdetailRequestModel> query = (from mrfDetails in _db.Mrfdetails
                                                 join fr in _db.Freshmrfdetails on mrfDetails.Id equals fr.MrfId
                                                 join replacement in _db.Replacementmrfdetails on mrfDetails.Id equals replacement.MrfId into eGroup
                                                 from replacement in eGroup.DefaultIfEmpty()
                                                 where mrfDetails.Id == MrfId
                                                 select new MrfdetailRequestModel
                                                 {
                                                     mrfID = mrfDetails.Id,
                                                     ReferenceNo = mrfDetails.ReferenceNo,
                                                     RequisitionType = mrfDetails.RequisitionType,
                                                     PositionTitle = mrfDetails.PositionTitle,
                                                     DepartmentId = mrfDetails.DepartmentId,
                                                     SubDepartmentId = mrfDetails.SubDepartmentId,
                                                     ProjectId = mrfDetails.ProjectId,
                                                     VacancyNo = mrfDetails.VacancyNo,
                                                     GenderId = mrfDetails.GenderId,
                                                     RequisitionDateUtc = mrfDetails.RequisitionDateUtc,
                                                     ReportsToEmployeeId = mrfDetails.ReportsToEmployeeId,
                                                     MinGradeId = mrfDetails.MinGradeId,
                                                     MaxGradeId = mrfDetails.MaxGradeId,
                                                     EmploymentTypeId = mrfDetails.EmploymentTypeId,
                                                     MinExperience = mrfDetails.MinExperience,
                                                     MaxExperience = mrfDetails.MaxExperience,
                                                     VacancyTypeId = mrfDetails.VacancyTypeId,
                                                     IsReplacement = mrfDetails.IsReplacement,
                                                     MrfStatusId = mrfDetails.MrfStatusId,
                                                     JdDocPath = mrfDetails.JdDocPath,
                                                     LocationId = mrfDetails.LocationId,
                                                     QualificationId=mrfDetails.QualificationId,
                                                     Justification = fr.Justification ?? "",
                                                     JobDescription = fr.JobDescription ?? "",
                                                     Skills = fr.Skills ?? "",
                                                     MinTargetSalary = fr.MinTargetSalary != 0 ? fr.MinTargetSalary : 0,
                                                     MaxTargetSalary = fr.MaxTargetSalary != 0 ? fr.MaxTargetSalary : 0,
                                                     EmployeeName = replacement.EmployeeName ?? "",
                                                     EmployeeCode = replacement != null ? replacement.EmployeeCode : 0,
                                                     EmailId = replacement != null ? replacement.EmailId : "",
                                                     LastWorkingDate = replacement != null ? replacement.LastWorkingDate : DateOnly.MinValue,
                                                     ReplaceJustification = replacement.Justification ?? "",
                                                     AnnualCtc = replacement != null ? replacement.AnnualCtc : 0,
                                                     AnnualGross = replacement != null ? replacement.AnnualGross : 0,
                                                 }).ToList();


            List<MrfdetailRequestModel> interviewR = (_db.Mrfdetails
    .Join(_db.Mrfinterviewermap, mrfDetail => mrfDetail.Id, interviewer => interviewer.MrfId, (mrfDetail, interviewer) => new MrfdetailRequestModel
    {
        mrfID = mrfDetail.Id,
        InterviewerEmployeeId = interviewer.InterviewerEmployeeId
    })
    .Where(mrfDetail => mrfDetail.mrfID == MrfId)
    .GroupBy(mrfDetail => mrfDetail.mrfID)
    .Select(grouping => new MrfdetailRequestModel
    {
        mrfID = grouping.Key,
        InterviewerEmployeeIds = string.Join(",", grouping.Select(mrfDetail => mrfDetail.InterviewerEmployeeId))
    })
    ).ToList();


            List<MrfdetailRequestModel> resumeR = (_db.Mrfdetails
  .Join(_db.Mrfresumereviewermap, mrfDetail => mrfDetail.Id, resumer => resumer.MrfId, (mrfDetail, resumer) => new MrfdetailRequestModel
  {
      mrfID = mrfDetail.Id,
      ResumeReviewerEmployeeId = resumer.ResumeReviewerEmployeeId
  })
  .Where(mrfDetail => mrfDetail.mrfID == MrfId)
  .GroupBy(mrfDetail => mrfDetail.mrfID)
  .Select(grouping => new MrfdetailRequestModel
  {
      mrfID = grouping.Key,
      ResumeReviewerEmployeeIds = string.Join(",", grouping.Select(mrfDetail => mrfDetail.ResumeReviewerEmployeeId))
  })
  ).ToList();


            List<MrfdetailRequestModel> emploeeyemailId = (from mrfDetails in _db.Mrfdetails
                                                           join mail in _db.MrfEmailApproval on mrfDetails.Id equals mail.MrfId
                                                           join employee in _db.Employeedetails on mail.EmployeeId equals employee.Id
                                                           join role in _db.Employeerolemap on employee.Id equals role.EmployeeId
                                                           where mrfDetails.Id == MrfId
                                                           select new MrfdetailRequestModel
                                                           {
                                                               mrfID = mrfDetails.Id,
                                                               HiringManagerEmpId = mail.EmployeeId,
                                                               EmployeeCode= employee.Id,// update later with employeecode
                                                               HiringManagerId = employee.Id,
                                                               roleId = role.RoleId,
                                                               HMApprovalDate = mail.ApprovalDate,
                                                           }
                                                            ).ToList();

            


            foreach (var r in query)
            {
                if (interviewR.Count() > 0)
                    r.InterviewerEmployeeIds =  interviewR.First().InterviewerEmployeeIds ;

                if (resumeR.Count() > 0)
                    r.ResumeReviewerEmployeeIds =  resumeR.First().ResumeReviewerEmployeeIds ;


                if(emploeeyemailId.Count>0)
                {
                    
                        var e = emploeeyemailId.FirstOrDefault(e => e.roleId == 7);
                        if (e != null)
                         { r.HiringManagerEmpId = e.HiringManagerEmpId; r.HiringManagerId = e.HiringManagerId; r.HMApprovalDate = e.HMApprovalDate; }
                    
                    e = emploeeyemailId.FirstOrDefault(e => e.roleId == 8);
                    if (e != null)
                    { r.FunctionHeadEmpId = e.HiringManagerEmpId; r.FunctionHeadId = e.HiringManagerId; r.FHApprovalDate = e.HMApprovalDate; }
                    
                    e = emploeeyemailId.FirstOrDefault(e => e.roleId == 9);
                    if (e != null)
                    { r.SiteHRSPOCEmpId = e.HiringManagerEmpId; r.SiteHRSPOCId = e.HiringManagerId; r.SPApprovalDate = e.HMApprovalDate; }
                    
                    e = emploeeyemailId.FirstOrDefault(e => e.roleId == 10);
                    if (e != null)
                    { r.FinanceHeadEmpId = e.HiringManagerEmpId; r.FinanceHeadId = e.HiringManagerId; r.FIApprovalDate = e.HMApprovalDate; }
                   
                    e = emploeeyemailId.FirstOrDefault(e => e.roleId == 11);
                    if (e != null)
                    { r.PresidentnCOOEmpId = e.HiringManagerEmpId; r.PresidentnCOOId = e.HiringManagerId; r.PCApprovalDate = e.HMApprovalDate; }

                    
                }
            }

            return query.FirstOrDefault();
        }
    }

}

