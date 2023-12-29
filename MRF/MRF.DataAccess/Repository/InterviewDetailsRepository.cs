using MRF.DataAccess.Repository.IRepository;
using MRF.Models.Models;
using MRF.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MRF.DataAccess.Repository
{
    public class InterviewDetailsRepository : Repository<InterviewDetailsViewModel>, IInterviewDetailsRepository
    {
        private readonly Data.MRFDBContext _db;
        public InterviewDetailsRepository(Data.MRFDBContext db) : base(db)
        {
            _db = db;
        }

        public List<InterviewDetailsViewModel> GetInterviewDetails(int mrfId)
        {
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

            /*IstatusGrouped contains only the latest status for each CandidateId.*/
            var statusGrouped = from mrfDetails in _db.Mrfdetails
                                join Candidate in _db.Candidatedetails on mrfDetails.Id equals Candidate.MrfId
                                join Ivaluation in _db.Interviewevaluation on Candidate.Id equals Ivaluation.CandidateId
                                join status in _db.Evaluationstatusmaster on Ivaluation.EvalutionStatusId equals status.Id
                                where mrfDetails.Id == mrfId
                                select new InterviewStatus
                                {
                                    CandidateId = Candidate.Id,
                                    
                                    EvalutionStatusId = Ivaluation.EvalutionStatusId,
                                    CandidateStatusChangedOnUtc = Ivaluation.UpdatedOnUtc,
                                    EvalutionStatus = status.Status,
                                    InterviewevaluationId = Ivaluation.Id,
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
                                                              };

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

                                                               };


            return finalmerge.ToList();

        }
    }
}
