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
            var query = from mrfDetails in _db.Mrfdetails
                        join resume in _db.Mrfresumereviewermap on mrfDetails.Id equals resume.MrfId
                        join Emp in _db.Employeedetails on resume.CreatedByEmployeeId equals Emp.Id
                        join Emp2 in _db.Employeedetails on resume.ResumeReviewerEmployeeId equals Emp2.Id
                        join Candidate in _db.Candidatedetails on resume.MrfId equals Candidate.MrfId
                        join status in _db.Candidatestatusmaster on Candidate.CandidateStatusId equals status.Id
                        where mrfDetails.Id==mrfId && status.Status.Contains("resume")
                        select new ResumeDetailsViewModel
                        {
                            MrfId = resume.MrfId,
                            ReferenceNo = mrfDetails.ReferenceNo,
                            CandidateStatusId = Candidate.CandidateStatusId,
                            Candidatestatus = status.Status,
                            ResumeReviewerEmployeeId = Emp2.Id,
                            ResumeReviewerName = Emp2.Name,
                            CreatedByEmployeeId = Emp.Id,
                            CreatedName = Emp.Name,
                            CreatedOnUtc = resume.CreatedOnUtc,
                            ResumePath = Candidate.ResumePath
                        };
            return query.ToList();
           
        }
    }
}

