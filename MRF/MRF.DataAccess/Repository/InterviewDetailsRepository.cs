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
    public class InterviewDetailsRepository:Repository<InterviewDetailsViewModel>,IInterviewDetailsRepository
    {
        private readonly Data.MRFDBContext _db;
        public InterviewDetailsRepository(Data.MRFDBContext db) : base(db)
        {
            _db = db;
        }

        public List<InterviewDetailsViewModel> GetInterviewDetails(int mrfId)
        {
            IQueryable<InterviewDetailsViewModel> query = from mrfDetails in _db.Mrfdetails
                        join Candidate in _db.Candidatedetails on mrfDetails.Id equals Candidate.MrfId
                        join Emp in _db.Employeedetails on Candidate.CreatedByEmployeeId equals Emp.Id
                        join status in _db.Candidatestatusmaster on Candidate.CandidateStatusId equals status.Id 
                        join Ivaluation in _db.Interviewevaluation on Candidate.Id equals Ivaluation.CandidateId
                        join Attachment in _db.AttachmentEvaluation on Ivaluation.EvaluationId equals Attachment.InterviewEvaluationId
                        join Emp2 in _db.Employeedetails on Ivaluation.InterviewerId equals Emp2.Id
                        join EvFeedback in _db.Evaluationfeedbackmaster on Ivaluation.EvaluationFeedbackId equals EvFeedback.Id
                        where mrfDetails.Id == mrfId && !status.Status.Contains("resume")
                        select new InterviewDetailsViewModel
                        {
                            MrfId = mrfDetails.Id,
                            ReferenceNo = mrfDetails.ReferenceNo,
                            ResumePath = Candidate.ResumePath,
                            CreatedByEmployeeId = Emp.Id,
                            CreatedName = Emp.Name,
                            CreatedOnUtc = Candidate.CreatedOnUtc,
                            CandidateId= Candidate.Id,
                            CandidateStatusId = Candidate.CandidateStatusId,
                            Candidatestatus = status.Status,
                            InterviewerEmployeeId = Ivaluation.InterviewerId,
                            InterviewerName = Emp2.Name,
                            EvaluationId= Ivaluation.Id,
                            EvaluationFeedbackId = Ivaluation.EvaluationFeedbackId,
                            EvalutionStatus = EvFeedback.Description,
                            CandidateStatusChangedOnUtc= Ivaluation.CreatedOnUtc,
                            Attachment = Attachment.FilePath,
                        };

            var latestRecords = query
            .GroupBy(r => r.CandidateId)
            .Select(g => g.OrderByDescending(r => r.EvaluationId).First());



            return latestRecords.ToList();

        }
    }
}
