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
                                                          mrfID=mrfDetails.Id,
                                                          ReferenceNo = mrfDetails.ReferenceNo,
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
                                                      };



            IQueryable<MrfdetailRequestModel> interviewR = from mrfDetails in _db.Mrfdetails
                                                           join interviewer in _db.Mrfinterviewermap on mrfDetails.Id equals interviewer.MrfId 
                                                           //into eGroup
                                                           //from interviewer in eGroup.DefaultIfEmpty()
                                                           where mrfDetails.Id == MrfId
                                                           select new MrfdetailRequestModel
                                                           {
                                                               mrfID = mrfDetails.Id,
                                                               InterviewerEmployeeId=interviewer.InterviewerEmployeeId,
                                                           };
            IQueryable<MrfdetailRequestModel> resumeR = from mrfDetails in _db.Mrfdetails
                                                           join resumer in _db.Mrfresumereviewermap on mrfDetails.Id equals resumer.MrfId 
                                                           //into eGroup
                                                           //from resumer in eGroup.DefaultIfEmpty()
                                                           where mrfDetails.Id == MrfId
                                                           select new MrfdetailRequestModel
                                                           {
                                                               mrfID = mrfDetails.Id,
                                                               ResumeReviewerEmployeeId = resumer.ResumeReviewerEmployeeId,
                                                           };


            IQueryable<MrfdetailRequestModel> combined = from r in resumeR
                                                         join i in interviewR on r.mrfID equals i.mrfID
                                                         select new MrfdetailRequestModel
                                                         {
                                                             mrfID = r.mrfID,
                                                             ResumeReviewerEmployeeId = i.ResumeReviewerEmployeeId,
                                                             InterviewerEmployeeId = r.InterviewerEmployeeId,
                                                         };



            IQueryable<MrfdetailRequestModel> finalquery = from q in query
                                                      
                                                      join c in combined on q.mrfID equals c.mrfID into eGroup
                                                      from c in eGroup.DefaultIfEmpty()
                                                      where q.mrfID == c.mrfID
                                                      select new MrfdetailRequestModel
                                                      {
                                                          mrfID = q.mrfID,
                                                          ReferenceNo = q.ReferenceNo,
                                                          PositionTitle = q.PositionTitle,
                                                          DepartmentId = q.DepartmentId,
                                                          SubDepartmentId = q.SubDepartmentId,
                                                          ProjectId = q.ProjectId,
                                                          VacancyNo = q.VacancyNo,
                                                          GenderId = q.GenderId,
                                                          RequisitionDateUtc = q.RequisitionDateUtc,
                                                          ReportsToEmployeeId = q.ReportsToEmployeeId,
                                                          MinGradeId = q.MinGradeId,
                                                          MaxGradeId = q.MaxGradeId,
                                                          EmploymentTypeId = q.EmploymentTypeId,
                                                          MinExperience = q.MinExperience,
                                                          MaxExperience = q.MaxExperience,
                                                          VacancyTypeId = q.VacancyTypeId,
                                                          IsReplacement = q.IsReplacement,
                                                          MrfStatusId = q.MrfStatusId,
                                                          JdDocPath = q.JdDocPath,
                                                          LocationId = q.LocationId,
                                                          Justification = q.Justification,
                                                          JobDescription = q.JobDescription,
                                                          Skills = q.Skills ,
                                                          MinTargetSalary = q.MinTargetSalary ,
                                                          MaxTargetSalary = q.MaxTargetSalary ,
                                                          EmployeeName = q.EmployeeName ,
                                                          EmployeeCode =  q.EmployeeCode ,
                                                          EmailId = q.EmailId,
                                                          LastWorkingDate = q.LastWorkingDate ,
                                                          ReplaceJustification = q.ReplaceJustification,
                                                          AnnualCtc = q.AnnualCtc ,
                                                          AnnualGross = q.AnnualGross ,
                                                          ResumeReviewerEmployeeId= c != null ? c.ResumeReviewerEmployeeId:0,
                                                          InterviewerEmployeeId= c != null ? c.InterviewerEmployeeId:0,

                                                      };





            return finalquery.FirstOrDefault();
        }
    }
       
}

