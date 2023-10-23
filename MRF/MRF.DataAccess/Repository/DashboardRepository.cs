using Microsoft.EntityFrameworkCore;
using MRF.DataAccess.Repository.IRepository;
using MRF.Models.Models;
using MRF.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.PortableExecutable;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
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


        public List<MrfResumeSummaryViewModel> GetCountByMrfIdAndResumeStatus1()
        {

            IQueryable<MrfResumeSummaryViewModel> query = from mrfDetails in _db.Mrfdetails
                                                          join Candidate in _db.Candidatedetails on mrfDetails.Id equals Candidate.MrfId
                                                          join status in _db.Candidatestatusmaster on Candidate.CandidateStatusId equals status.Id
                                                          where status.Status.Contains("resume")
                                                          group new { mrfDetails, Candidate, status } by new
                                                          {
                                                              mrfDetails.Id,
                                                              //Candidate.CandidateStatusId,
                                                              status.Status,
                                                              mrfDetails.ReferenceNo,

                                                          }
                    into grouped
                                                          select new MrfResumeSummaryViewModel
                                                          {
                                                              MrfId = grouped.Key.Id,
                                                              ReferenceNo = grouped.Key.ReferenceNo,
                                                              //CandidateStatusId = grouped.Key.CandidateStatusId,
                                                              Candidatestatus = grouped.Key.Status.Replace("Resume", "").Replace("Uploaded", "New").Replace("Selected", "Shortlisted").Trim(),
                                                              TotalCount = grouped.Count(),
                                                          };
            return query.ToList();


        }
       public List<ResultViewModel> GetCountByMrfIdAndResumeStatus(int count)
        {
            List<ResultViewModel> finalresult= new List<ResultViewModel>();
            finalresult = GetCountfromCandidateStatus(count, true);
            return finalresult;
        }

        public List<ResultViewModel> GetCountfromCandidateStatus(int count,bool resume)
        {
            List<Candidatestatusmaster> CStatus = new List<Candidatestatusmaster>();
            if (resume)
            {
                 CStatus = (from s in _db.Candidatestatusmaster
                               where s.Status.Contains("resume")
                               select new Candidatestatusmaster
                               {
                                   Id = s.Id,
                                   Status = s.Status.Replace("Resume", "")
                                           .Replace("Uploaded", "New")
                                           .Replace("Selected", "Shortlisted")
                                           .Trim(),
                               }).ToList();
            }
            else
            {
                CStatus = (from s in _db.Candidatestatusmaster
                           where !s.Status.Contains("resume")
                           select s).ToList();
            }
            var mrfDetails = from mrfD in _db.Mrfdetails
                             join Candidate in _db.Candidatedetails on mrfD.Id equals Candidate.MrfId
                             group new { mrfD, Candidate } by new
                             {
                                 mrfD.Id,
                                 Candidate.CandidateStatusId,
                                 mrfD.ReferenceNo,

                             }
                     into grouped
                             select new MrfResumeSummaryViewModel
                             {
                                 MrfId = grouped.Key.Id,
                                 ReferenceNo = grouped.Key.ReferenceNo,
                                 statusID = grouped.Key.CandidateStatusId,
                                 TotalCount = grouped.Count(),
                             };

            var result = new List<ResultViewModel>();
            int i = 1;
            bool valid = false;
            foreach (var mrf in mrfDetails)
            {
                valid = false;
                var resultViewModel = result.FirstOrDefault(r => r.mrfId == mrf.MrfId);
                if (resultViewModel == null)
                {
                    resultViewModel = new ResultViewModel
                    {
                        mrfId = mrf.MrfId,
                        referenceno = mrf.ReferenceNo,
                        resultGroups = new List<ResultGroup>()
                    };
                    valid = true;
                }

                foreach (var st in CStatus)
                {
                    var existingResultGroup = resultViewModel.resultGroups.FirstOrDefault(rg => rg.Candidatestatus == st.Status);
                    if (existingResultGroup != null)
                    {
                        if (st.Id == mrf.statusID)
                        {
                            existingResultGroup.TotalstatusCount = mrf.TotalCount;
                        }
                    }
                    else
                    {
                        var totalStatusCount = st.Id == mrf.statusID ? mrf.TotalCount : 0;
                        resultViewModel.resultGroups.Add(new ResultGroup
                        {
                            Candidatestatus = st.Status,
                            TotalstatusCount = totalStatusCount
                        });
                    }
                }

                if (valid)
                {
                    result.Add(resultViewModel);
                    i++;
                }
                if (count > 0 && i == count)
                {
                    break;
                }
            }

            return result;

        }

        public List<ResultViewModel> GroupByMrfInterviewStatus(int count)
        {
            List<ResultViewModel> result = new List<ResultViewModel>();
            result = GetCountfromCandidateStatus(count, false);

           var CStatus = (from s in _db.Evaluationstatusmaster
                          select s).ToList();
        
           var mrfDetails = from mrfD in _db.Mrfdetails
                         join Candidate in _db.Candidatedetails on mrfD.Id equals Candidate.MrfId
                         join interview in _db.Interviewevaluation on Candidate.Id equals interview.CandidateId
                         group new { mrfD, Candidate,  interview } by new
                         {
                             mrfD.Id,
                             interview.EvaluationId,
                             mrfD.ReferenceNo,
                         }
                 into grouped
                         select new MrfInterviewSummaryViewModel
                         {
                             MrfId = grouped.Key.Id,
                             ReferenceNo = grouped.Key.ReferenceNo,
                             EvaluationId = grouped.Key.EvaluationId,
                             TotalCount = grouped.Count(),
                         };


            foreach (var mrf in mrfDetails)
            {
               
                var resultViewModel = result.FirstOrDefault(r => r.mrfId == mrf.MrfId);
                if (resultViewModel != null)
                {
                    resultViewModel = new ResultViewModel
                    {
                        mrfId = mrf.MrfId,
                        referenceno = mrf.ReferenceNo,
                        resultGroups = new List<ResultGroup>()
                    };
 
                    foreach (var st in CStatus)
                    {
                        var existingResultGroup = resultViewModel.resultGroups.FirstOrDefault(rg => rg.Candidatestatus == st.Status);
                        if (existingResultGroup != null)
                        {

                            if (existingResultGroup.Candidatestatus == st.Status)
                            {
                                existingResultGroup.TotalstatusCount = mrf.TotalCount;
                            }
                        }
                        else
                        {
                            var totalStatusCount = st.Status == mrf.Candidatestatus ? mrf.TotalCount : 0;
                            resultViewModel.resultGroups.Add(new ResultGroup
                            {
                                Candidatestatus = st.Status,
                                TotalstatusCount = totalStatusCount
                            });
                        }
                    }
                    
                }
            }

            return result;

        }
    }
}
