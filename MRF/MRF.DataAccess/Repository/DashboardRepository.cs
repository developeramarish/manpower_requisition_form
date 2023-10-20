using Microsoft.EntityFrameworkCore;
using MRF.DataAccess.Repository.IRepository;
using MRF.Models.Models;
using MRF.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MRF.DataAccess.Repository
{
    public class DashboardRepository: Repository<MrDashboardViewModel>, IDashboardRepository
    {
        private readonly Data.MRFDBContext _db;
        private readonly IUserService _userService;
        
        public DashboardRepository(Data.MRFDBContext db,IUserService userService) : base(db)
        {
            _db = db;
            _userService= userService;
        }

        public List<MrfSummaryViewModel> GroupByMrfStatus()
        {
            int RoleId= _userService.GetRoleId();
            var query = from mrfStatus in _db.Mrfstatusmaster
                        join mrfstatusRole in _db.mrfStatusrolemap on mrfStatus.Id equals mrfstatusRole.statusId
                        join mrfDetails in _db.Mrfdetails
                        on mrfStatus.Id equals mrfDetails.MrfStatusId into mrfDetailsGroup
                        where mrfstatusRole.RoleId== RoleId
                        select new
                        {
                            MrfStatusId = mrfStatus.Id,
                            Status = mrfStatus.Status,
                            MrfDetailsCount = mrfDetailsGroup.Count(),
                        };

            var result = query.AsEnumerable()
                .Select(grouped => new MrfSummaryViewModel
                {
                    MrfStatusId = grouped.MrfStatusId,
                    Status = grouped.Status,
                    TotalCount = grouped.MrfDetailsCount,
                })
                .Distinct() // Apply distinct to avoid duplications
                .ToList();

            return result;
         
        }


        public List<MrfResumeSummaryViewModel> GetCountByMrfIdAndResumeStatus()
        {

            IQueryable<MrfResumeSummaryViewModel> query = from mrfDetails in _db.Mrfdetails
                        join Candidate in _db.Candidatedetails on mrfDetails.Id equals Candidate.MrfId 
                        join status in _db.Candidatestatusmaster on Candidate.CandidateStatusId equals status.Id
                        where status.Status.Contains("resume")
                        group new { mrfDetails, Candidate, status } by new
                        {
                            mrfDetails.Id,
                            Candidate.CandidateStatusId,
                            status.Status,
                            mrfDetails.ReferenceNo,

                        }
                    into grouped
                        select new MrfResumeSummaryViewModel
                        {
                            MrfId = grouped.Key.Id,
                            ReferenceNo = grouped.Key.ReferenceNo,
                            CandidateStatusId = grouped.Key.CandidateStatusId,
                            Candidatestatus = grouped.Key.Status.Replace("Resume","").Replace("Uploaded","New").Replace("Selected","Shortlisted").Trim(),
                            TotalCount = grouped.Count(),
                        };
            return query.ToList();


            
        }


        public List<MrfInterviewSummaryViewModel> GroupByMrfInterviewStatus()
        {

            IQueryable<MrfInterviewSummaryViewModel> query = from mrfDetails in _db.Mrfdetails
                        join Candidate in _db.Candidatedetails on mrfDetails.Id equals Candidate.MrfId
                        join status in _db.Candidatestatusmaster on Candidate.CandidateStatusId equals status.Id
                        join evaluation in _db.Evaluationmaster on Candidate.CandidateStatusId equals evaluation.Id
                        join interview in _db.Interviewevaluation on evaluation.Id equals interview.EvaluationId
                        where !status.Status.Contains("resume")
                        group new { mrfDetails, Candidate, status, interview, evaluation } by new
                        {
                            mrfDetails.Id,
                            mrfDetails.ReferenceNo,
                            Candidate.CandidateStatusId,
                            status.Status,
                            interview.EvaluationId,
                            evaluation.Type,
                        }
                    into grouped
                        select new MrfInterviewSummaryViewModel
                        {
                            MrfId = grouped.Key.Id,
                            ReferenceNo = grouped.Key.ReferenceNo,
                            EvaluationId = grouped.Key.EvaluationId,
                            Type = grouped.Key.Type,
                            Candidatestatus= grouped.Key.Status,
                            TotalCount = grouped.Count(),
                        };


            return query.ToList();

        }
    }
}
