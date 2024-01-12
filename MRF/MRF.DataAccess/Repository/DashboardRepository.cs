using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MRF.DataAccess.Repository.IRepository;
using MRF.Models.DTO;
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
        private readonly Data.Utility _Utility;
        public DashboardRepository(Data.MRFDBContext db,IUserService userService) : base(db)
        {
            _db = db;
            _userService= userService;
            _Utility = new Data.Utility();
        }

        public List<MrfSummaryViewModel> GroupByMrfStatus(int roleId, int userId)
        {
            //int RoleId= _userService.GetRoleId();
            //if(RoleId == 0)
            //{
            //    ResponseDTO _response = _userService.GetRoledetails(false);

            //}
            //RoleId= _userService.GetRoleId();
            string Role = _Utility.GetRole(roleId);
            var mrf = from Mrfdetails in _db.Mrfdetails 
                             where (Role != "mrfowner" || (Role == "mrfowner" && Mrfdetails.CreatedByEmployeeId == userId))
                            select Mrfdetails;


            var query = from mrfStatus in _db.Mrfstatusmaster
                        join mrfstatusRole in _db.mrfStatusrolemap on mrfStatus.Id equals mrfstatusRole.statusId
                        join mrfDetails in mrf
                        on mrfStatus.Id equals mrfDetails.MrfStatusId 
                        
                        into mrfDetailsGroup
                        where mrfstatusRole.RoleId == roleId 
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
       public List<ResultViewModel> GetCountByMrfIdAndResumeStatus(int count, int roleId, int userId)
        {
            List<ResultViewModel> finalresult= new List<ResultViewModel>();
            finalresult = GetCountfromCandidateStatus(count, true, roleId, userId);
            return finalresult;
        }

        public List<ResultViewModel> GetCountfromCandidateStatus(int count,bool resume, int roleId, int userId)
        {
            List<Candidatestatusmaster> CStatus = new List<Candidatestatusmaster>();
            if (resume)
            {
                 CStatus = (from s in _db.Candidatestatusmaster
                               //where s.Status.Contains("resume")
                               select new Candidatestatusmaster
                               {
                                   Id = s.Id,
                                   Status = s.Status,
                               }).ToList();
            }
            //else
            //{
            //    CStatus = (from s in _db.Candidatestatusmaster
            //               where !s.Status.Contains("resume")
            //               select s).ToList();
            //}
            string Role = _Utility.GetRole(roleId);
            var mrfDetails = from mrfD in _db.Mrfdetails
                             join Candidate in _db.Candidatedetails on mrfD.Id equals Candidate.MrfId join position in _db.PositionTitlemaster
                             on mrfD.PositionTitleId equals position.Id
                             where ((Role == "mrfowner" && mrfD.CreatedByEmployeeId == userId)
                             || (Role == "resumereviewer" && Candidate.ReviewedByEmployeeIds!=null 
                             && Candidate.ReviewedByEmployeeIds.Contains(Convert.ToString(userId))) || (Role != "mrfowner" && Role != "resumereviewer"))
                             group new { mrfD, Candidate } by new
                             {
                                 mrfD.Id,
                                 Candidate.CandidateStatusId,
                                 mrfD.ReferenceNo,
                                 position.Name,

                             }
                     into grouped
                             select new MrfResumeSummaryViewModel
                             {
                                 MrfId = grouped.Key.Id,
                                 ReferenceNo = grouped.Key.ReferenceNo,
                                 statusID = grouped.Key.CandidateStatusId,
                                 TotalCount = grouped.Count(),
                                 PositionTitle=grouped.Key.Name,
                             };

            var result = new List<ResultViewModel>();
            
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
                        positionTitle= mrf.PositionTitle,
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
                    
                }
                if (count > 0 && result.Count == count)
                {
                    break;
                }
            }

            return result;

        }

        public List<ResultViewModel> GroupByMrfInterviewStatus(int count, int roleId, int userId)
        {
            List<ResultViewModel> result = new List<ResultViewModel>();
            //result = GetCountfromCandidateStatus(count, false);

            var CStatus = (from s in _db.Evaluationstatusmaster
                           select s).ToList();
            string Role = _Utility.GetRole(roleId);
           
            
            
                /* group by mrfid and evaluation id will get count */
                var Interviewevaluation = (from mrfD in _db.Mrfdetails
                                           join Candidate in _db.Candidatedetails on mrfD.Id equals Candidate.MrfId
                                           join interview in _db.Interviewevaluation on Candidate.Id equals interview.CandidateId
                                           join status in _db.Evaluationstatusmaster on interview.EvalutionStatusId equals status.Id
                                           join position in _db.PositionTitlemaster on mrfD.PositionTitleId equals position.Id
                                           where ((Role == "mrfowner" && mrfD.CreatedByEmployeeId == userId)
                                                          || (Role == "interviewer" && interview.InterviewerId != 0
                                               && interview.InterviewerId == userId) || (Role != "mrfowner" && Role != "interviewer"))
                                           group new { mrfD, Candidate, interview, status } by new

                                           {
                                               mrfD.Id,
                                               mrfD.ReferenceNo,
                                               position.Name,
                                               status = status.Id,

                                           }
                     into grouped
                                           select new MrfInterviewSummaryViewModel
                                           {
                                               MrfId = grouped.Key.Id,
                                               EvaluationId = grouped.Key.status,
                                               ReferenceNo = grouped.Key.ReferenceNo,
                                               TotalCount = grouped.Count(),
                                               PositionTitle = grouped.Key.Name,
                                           })
                             .OrderBy(result => result.MrfId)
                             .ToList();


            if (Role == "mrfowner" || Role == "hr")
            {
                var mrflist = (from mrfD in _db.Mrfdetails
                               join Candidate in _db.Candidatedetails on mrfD.Id equals Candidate.MrfId
                               join position in _db.PositionTitlemaster on mrfD.PositionTitleId equals position.Id
                               where ((Role == "mrfowner" && mrfD.CreatedByEmployeeId == userId) || Role != "mrfowner")
                               select new MrfInterviewSummaryViewModel
                               {
                                   MrfId = mrfD.Id,
                                   ReferenceNo = mrfD.ReferenceNo,
                                   PositionTitle = position.Name,
                               }).ToList();


                var newItems = mrflist.Where(x => !Interviewevaluation.Any(y => x.MrfId == y.MrfId));
                foreach (var item in newItems)
                {
                    Interviewevaluation.Add(item);
                }
            }
           
            

            bool valid = false;
            foreach (var mrf in Interviewevaluation)
            {
                valid = false;
                var resultViewModel = result.FirstOrDefault(r => r.mrfId == mrf.MrfId);
                if (resultViewModel == null)
                {
                    resultViewModel = new ResultViewModel
                    {
                        mrfId = mrf.MrfId,
                        referenceno = mrf.ReferenceNo,
                        positionTitle = mrf.PositionTitle,
                        resultGroups = new List<ResultGroup>()
                    };
                    valid = true;
                }

                foreach (var st in CStatus)
                {
                    var existingResultGroup = resultViewModel.resultGroups.FirstOrDefault(rg => rg.Candidatestatus == st.Status);
                    if (existingResultGroup != null)
                    {
                        if (st.Id == mrf.EvaluationId)
                        {
                            existingResultGroup.TotalstatusCount = mrf.TotalCount;
                        }
                    }
                    else
                    {
                        var totalStatusCount = st.Id == mrf.EvaluationId ? mrf.TotalCount : 0;
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

                }
                if (count > 0 && result.Count == count)
                {
                    break;
                }
            }


            //foreach (var mrf in Interviewevaluation)
            //{
            //    /*take record of same mrfdetails match with Interviewevaluation*/
            //    var resultViewModel = result.FirstOrDefault(r => r.mrfId == mrf.MrfId);

            //    if (resultViewModel != null)
            //    {    var existingResultGroup = resultViewModel.resultGroups
            //        .First(rg => rg.Candidatestatus == mrf.Candidatestatus);
            //        if (existingResultGroup != null)
            //        {
            //            existingResultGroup.TotalstatusCount = mrf.TotalCount;
            //        }
            //    }

            //}

            return result;

        }
    }
}
