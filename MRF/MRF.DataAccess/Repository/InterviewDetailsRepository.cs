using MRF.DataAccess.Data;
using MRF.DataAccess.Repository.IRepository;
using MRF.Models.Models;
using MRF.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MRF.DataAccess.Repository
{
    public class InterviewDetailsRepository : Repository<InterviewDetailsViewModel>, IInterviewDetailsRepository
    {
        private readonly Data.MRFDBContext _db; private readonly Data.Utility _Utility;
        public InterviewDetailsRepository(Data.MRFDBContext db) : base(db)
        {
            _db = db; _Utility = new Data.Utility();
        }

        public List<InterviewDetailsViewModel> GetInterviewDetails(int mrfId,int roleId,int userId)
        {
            string Role = _Utility.GetRole(roleId);
            /* take list from Interview reviewer assigned to mrfId   */
            IQueryable<InterviewDetailsViewModel> Mrfinterviewmap =
     _db.Mrfdetails
     .Where(mrfDetails => mrfDetails.Id == mrfId)
         .GroupJoin(
             _db.Mrfinterviewermap,
             mrfDetails => mrfDetails.Id,
             Interview => Interview.MrfId,
             (mrfDetails, InterviewGroup) => new InterviewDetailsViewModel
             {
                 MrfId = mrfDetails.Id,
                 InterviewerEmployeeIds = string.Join(",", InterviewGroup.Select(r => r.InterviewerEmployeeId).Distinct())
             }
         );

            /* take list from Interview reviewer assigned to candidate details   */
            IQueryable<InterviewDetailsViewModel> interviewer = _db.Mrfdetails
                .Where(mrfDetails => mrfDetails.Id == mrfId)
                .Join(
                    _db.Candidatedetails,
                    mrfDetails => mrfDetails.Id,
                    candidate => candidate.MrfId,
                    (mrfDetails, candidate) => new { mrfDetails, candidate }
                )
                .GroupJoin(
                    _db.Interviewevaluation,
                    combined => combined.candidate.Id,
                    interview => interview.CandidateId,
                    (combined, interviewGroup) => new InterviewDetailsViewModel
                    {
                        CandidateId = combined.candidate.Id,
                        InterviewerEmployeeIds = string.Join(",", interviewGroup.Select(r => r.InterviewerId).Distinct())
                    }
                );

            /*  Assisgnment assigned to candidate details   */
            IQueryable<InterviewDetailsViewModel> Attachments = from mrfDetails in _db.Mrfdetails
                                                                join Candidate in _db.Candidatedetails on mrfDetails.Id equals Candidate.MrfId
                                                                join Ivaluation in _db.Interviewevaluation on Candidate.Id equals Ivaluation.CandidateId
                                                                join Attachment in _db.AttachmentEvaluation on Ivaluation.Id equals Attachment.InterviewEvaluationId
                                                      
                                                                where mrfDetails.Id == mrfId
                                                                select new InterviewDetailsViewModel
                                                                {
                                                                    CandidateId = Candidate.Id,
                                                                     
                                                                    EvaluationId = Attachment.InterviewEvaluationId,
                                                                    Attachment = Attachment.FilePath,
                                                                };

            /* fetch interviewfeedback */
            IQueryable<InterviewDetailsViewModel> interviewfeedback = from mrfDetails in _db.Mrfdetails
                                                                join Candidate in _db.Candidatedetails on mrfDetails.Id equals Candidate.MrfId
                                                                join Ivaluation in _db.Interviewevaluation on Candidate.Id equals Ivaluation.CandidateId
                                                                join InterviewFeedback in _db.CandidateInterviewFeedback on Ivaluation.Id equals InterviewFeedback.CandidateId
                                                                      where mrfDetails.Id == mrfId
                                                                select new InterviewDetailsViewModel
                                                                {
                                                                    EvaluationFeedbackId= InterviewFeedback.Id,
                                                                };


            var IvaluationId = from mrfDetails in _db.Mrfdetails
                                join Candidate in _db.Candidatedetails on mrfDetails.Id equals Candidate.MrfId
                                join Ivaluation in _db.Interviewevaluation on Candidate.Id equals Ivaluation.CandidateId
                                where mrfDetails.Id == mrfId &&
                                (Role != "interviewer" || (Role == "interviewer" && Ivaluation.InterviewerId != 0
            && Ivaluation.InterviewerId == userId && Candidate.CandidateStatusId == 2))
                                select new InterviewStatus
                                {   
                                    CandidateId = Candidate.Id,
                                    EvalutionStatusId = Ivaluation.EvalutionStatusId,
                                    CandidateStatusChangedOnUtc = Ivaluation.UpdatedOnUtc,
                                    InterviewevaluationId = Ivaluation.Id,
                                };


            /*IstatusGrouped contains only the latest status for each CandidateId.*/
            var statusGrouped = from Ivaluation in IvaluationId
                                join status in _db.Evaluationstatusmaster on Ivaluation.EvalutionStatusId equals status.Id
                                into Evalstatus
                                from i in Evalstatus.DefaultIfEmpty()
                                select new InterviewStatus
                                {
                                    CandidateId = Ivaluation.CandidateId,

                                    EvalutionStatusId = Ivaluation.EvalutionStatusId,
                                    CandidateStatusChangedOnUtc = Ivaluation.CandidateStatusChangedOnUtc,
                                    InterviewevaluationId = Ivaluation.InterviewevaluationId,
                                    EvalutionStatus = i.Status==null?"": i.Status,
                                };

            var IstatusGrouped = from status in statusGrouped
                                 group status by status.CandidateId into grouped
                                 select new InterviewStatus
                                 {
                                     CandidateId = grouped.Key,
                                     EvalutionStatusId = grouped.OrderByDescending(s => s.CandidateStatusChangedOnUtc).First().EvalutionStatusId,
                                     CandidateStatusChangedOnUtc = grouped.OrderByDescending(s => s.CandidateStatusChangedOnUtc).First().CandidateStatusChangedOnUtc,
                                     EvalutionStatus = grouped.OrderByDescending(s => s.CandidateStatusChangedOnUtc).First().EvalutionStatus,
                                     InterviewevaluationId = grouped.OrderByDescending(s => s.CandidateStatusChangedOnUtc).First().InterviewevaluationId,
                                 };



            IQueryable<InterviewDetailsViewModel> firstlist = from mrfDetails in _db.Mrfdetails
                                                              join Candidate in _db.Candidatedetails on mrfDetails.Id equals Candidate.MrfId
                                                              join Emp in _db.Employeedetails on Candidate.CreatedByEmployeeId equals Emp.Id
                                                              join pos in _db.PositionTitlemaster on mrfDetails.PositionTitleId equals pos.Id
                                                        
                                                              where mrfDetails.Id == mrfId
                                                              select new InterviewDetailsViewModel
                                                              {
                                                                  MrfId = mrfDetails.Id,
                                                                  ReferenceNo = mrfDetails.ReferenceNo,
                                                                  ResumePath = Candidate.ResumePath,
                                                                  CreatedByEmployeeId = Emp.Id,
                                                                  CreatedName = Emp.Name,
                                                                  CreatedOnUtc = Candidate.CreatedOnUtc,
                                                                  CandidateId = Candidate.Id,
                                                                  PositionTitle = pos.Name,
                                                                  CandidateName = Candidate.Name,
                                                                  mrfStatusId = mrfDetails.MrfStatusId,
                                                                  CandidateStatusId = Candidate.CandidateStatusId,
                                                                  
                                                              };

            if(Role == "interviewer")
            {
                firstlist = firstlist.Where(candidate => IvaluationId.Any(iv => iv.CandidateId == candidate.CandidateId));
            }

            IQueryable<InterviewDetailsViewModel> secondmerge = from q in firstlist
                                                                join i in Mrfinterviewmap on q.MrfId equals i.MrfId into interviewResults
                                                                from i in interviewResults.DefaultIfEmpty()
                                                                select new InterviewDetailsViewModel
                                                                {
                                                                    MrfId = q.MrfId,
                                                                    ReferenceNo = q.ReferenceNo,
                                                                    ResumePath = q.ResumePath,
                                                                    CreatedByEmployeeId = q.CreatedByEmployeeId,
                                                                    CreatedName = q.CreatedName,
                                                                    CreatedOnUtc = q.CreatedOnUtc,
                                                                    CandidateId = q.CandidateId,
                                                                    PositionTitle = q.PositionTitle,
                                                                    InterviewerEmployeeIds = i.InterviewerEmployeeIds,
                                                                    CandidateName=q.CandidateName,
                                                                    mrfStatusId = q.mrfStatusId,
                                                                    CandidateStatusId = q.CandidateStatusId,
                                                                    Candidatestatus =  q.Candidatestatus,
                                                                };


            IQueryable<InterviewDetailsViewModel> thirdmerge = from q in secondmerge
                                                               join i in interviewer on q.CandidateId equals i.CandidateId into interviewResults
                                                               from i in interviewResults.DefaultIfEmpty()
                                                               select new InterviewDetailsViewModel
                                                               {
                                                                   MrfId = q.MrfId,
                                                                   ReferenceNo = q.ReferenceNo,
                                                                   ResumePath = q.ResumePath,
                                                                   CreatedByEmployeeId = q.CreatedByEmployeeId,
                                                                   CreatedName = q.CreatedName,
                                                                   CreatedOnUtc = q.CreatedOnUtc,
                                                                   CandidateId = q.CandidateId,
                                                                   PositionTitle = q.PositionTitle,
                                                                   CandidateName= q.CandidateName,
                                                                   InterviewerEmployeeIds = i.InterviewerEmployeeIds == "" ? q.InterviewerEmployeeIds : i.InterviewerEmployeeIds,
                                                                   mrfStatusId = q.mrfStatusId,
                                                                   CandidateStatusId = q.CandidateStatusId,
                                                                   Candidatestatus = q.Candidatestatus,
                                                               };


            IQueryable<InterviewDetailsViewModel> forthmerge = from q in thirdmerge
                                                               join i in Attachments on q.CandidateId equals i.CandidateId into interviewResults
                                                               from i in interviewResults.DefaultIfEmpty()
                                                               select new InterviewDetailsViewModel
                                                               {
                                                                   MrfId = q.MrfId,
                                                                   ReferenceNo = q.ReferenceNo,
                                                                   ResumePath = q.ResumePath,
                                                                   CreatedByEmployeeId = q.CreatedByEmployeeId,
                                                                   CreatedName = q.CreatedName,
                                                                   CreatedOnUtc = q.CreatedOnUtc,
                                                                   CandidateId = q.CandidateId,
                                                                   PositionTitle = q.PositionTitle,
                                                                   CandidateName=q.CandidateName,   
                                                                   InterviewerEmployeeIds = q.InterviewerEmployeeIds,
                                                                   Attachment = i.Attachment == null ? "" : i.Attachment,
                                                                   mrfStatusId = q.mrfStatusId,
                                                                   CandidateStatusId = q.CandidateStatusId,
                                                                   Candidatestatus = q.Candidatestatus,
                                                               };


            IQueryable<InterviewDetailsViewModel> finalmerge = from q in forthmerge
                                                               join i in IstatusGrouped on q.CandidateId equals i.CandidateId
                                                               into interviewResults
                                                               from i in interviewResults.DefaultIfEmpty()
                                                               select new InterviewDetailsViewModel
                                                               {
                                                                   MrfId = q.MrfId,
                                                                   ReferenceNo = q.ReferenceNo,
                                                                   ResumePath = q.ResumePath,
                                                                   CreatedByEmployeeId = q.CreatedByEmployeeId,
                                                                   CreatedName = q.CreatedName,
                                                                   CreatedOnUtc = q.CreatedOnUtc,
                                                                   CandidateId = q.CandidateId,
                                                                   PositionTitle = q.PositionTitle,
                                                                   CandidateName =  q.CandidateName,
                                                                   InterviewerEmployeeIds = q.InterviewerEmployeeIds,
                                                                   Attachment = q.Attachment,
                                                                   EvalutionStatusId = i != null ? i.EvalutionStatusId ?? 0 : 0, // Check for null outside the query
                                                                   EvalutionStatus = i != null ? i.EvalutionStatus : "",  // Check for null outside the query
                                                                   CandidateStatusChangedOnUtc = i == null ? DateTime.MinValue : i.CandidateStatusChangedOnUtc ?? DateTime.MinValue,
                                                                   InterviewevaluationId = i != null ? i.InterviewevaluationId ?? 0 : 0,
                                                                   mrfStatusId = q.mrfStatusId,
                                                                   CandidateStatusId = q.CandidateStatusId,
                                                                   Candidatestatus = q.Candidatestatus,
                                                               };

            List<InterviewDetailsViewModel> queryResults = finalmerge.ToList();
            if (queryResults.Count > 0)
            {
                List<Employeerolemap> res = GetEmployeebyRole(6);
                foreach (var q in queryResults)
                {
                    q.InterviewerName = GetEmployeeNames(res, q.InterviewerEmployeeIds);

                }
            }


            return queryResults;

        }


        private string GetEmployeeNames(List<Employeerolemap> res, string employeeIds)
        {
            if (string.IsNullOrEmpty(employeeIds))
            { return string.Empty; }
            var names = new List<string>();

            foreach (var employeeId in employeeIds.Split(','))
            {
                if (int.TryParse(employeeId, out int empId))
                {
                    var employee = res.FirstOrDefault(emp => emp.EmployeeId == empId);
                    if (employee != null)
                    {
                        names.Add(employee.name);
                    }
                }
            }

            return string.Join(", ", names);
        }

        private List<Employeerolemap> GetEmployeebyRole(int roleId)
        {
            IQueryable<Employeerolemap> query = from emprole in _db.Employeerolemap
                                                join empdetails in _db.Employeedetails on emprole.EmployeeId equals empdetails.Id
                                                where emprole.RoleId == roleId
                                                select new Employeerolemap
                                                {
                                                    EmployeeId = emprole.EmployeeId,
                                                    name = empdetails.Name,
                                                    RoleId = emprole.RoleId,
                                                    EmployeeCode = empdetails.EmployeeCode,
                                                };

            return query.ToList();

        }
    }
}
