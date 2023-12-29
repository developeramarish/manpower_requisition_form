using Microsoft.EntityFrameworkCore;
using MRF.DataAccess.Repository.IRepository;
using MRF.Models.Models;
using MRF.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MRF.DataAccess.Repository
{
    public class ResumeDetailsRepository : Repository<ResumeDetailsViewModel>, IResumeDetailsRepository
    {
        private readonly Data.MRFDBContext _db;
        public ResumeDetailsRepository(Data.MRFDBContext db) : base(db)
        {
            _db = db;
        }

        public List<ResumeDetailsViewModel> GetResumeStatusDetails(int mrfId)
        {

            /* take list from resume reviewer assigned to mrfId   */
            IQueryable<ResumeDetailsViewModel> Mrfresumereviewermap =
     _db.Mrfdetails
         .GroupJoin(
             _db.Mrfresumereviewermap,
             mrfDetails => mrfDetails.Id,
             resume => resume.MrfId,
             (mrfDetails, resumeGroup) => new ResumeDetailsViewModel
             {
                 MrfId = mrfDetails.Id,
                 ResumeReviewerEmployeeIds = string.Join(",", resumeGroup.Select(r => r.ResumeReviewerEmployeeId).Distinct())
             }
         );


            /* if resume reviewer assigned to mrfId will assign if reviewer assigned not with candidate  */
            IQueryable<ResumeDetailsViewModel> query =
    from mrfDetails in _db.Mrfdetails
    join pos in _db.PositionTitlemaster on mrfDetails.PositionTitleId equals pos.Id
    join candidate in _db.Candidatedetails on mrfDetails.Id equals candidate.MrfId
    join emp in _db.Employeedetails on candidate.CreatedByEmployeeId equals emp.Id
    join status in _db.Candidatestatusmaster on candidate.CandidateStatusId equals status.Id
    // Left outer join with Mrfresumereviewermap only if candidate.ReviewedByEmployeeIds is an empty string
    join resume in Mrfresumereviewermap
         on new { MrfId = mrfDetails.Id, IsEmptyReview = candidate.ReviewedByEmployeeIds == "" }
         equals new { resume.MrfId, IsEmptyReview = true } into resumeJoin
    from resume in resumeJoin.DefaultIfEmpty()
    where mrfDetails.Id == mrfId
    select new ResumeDetailsViewModel
    {
        MrfId = mrfDetails.Id,
        ReferenceNo = mrfDetails.ReferenceNo,
        CandidateStatusId = candidate.CandidateStatusId,
        Candidatestatus = status.Status,
        ResumeReviewerEmployeeIds = candidate.ReviewedByEmployeeIds == "" ? resume.ResumeReviewerEmployeeIds : candidate.ReviewedByEmployeeIds,
        CreatedByEmployeeId = emp.Id,
        CreatedName = emp.Name,
        CreatedOnUtc = candidate.CreatedOnUtc,
        ResumePath = candidate.ResumePath,
        CandidateId = candidate.Id,
        Reason = candidate.Reason,
        PositionTitle = pos.Name,
        CandidateName = candidate.Name,

    };

            return query.ToList();

        }
    }
}

