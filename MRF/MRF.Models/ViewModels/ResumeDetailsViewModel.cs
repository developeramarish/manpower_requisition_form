using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRF.Models.ViewModels
{
    public class ResumeDetailsViewModel
    {
        public int CandidateId { get; set; }
        public int MrfId { get; set; }
        public string ReferenceNo { get; set; } = null!;
        public int ResumeReviewerEmployeeId { get; set; }
        public string? ResumeReviewerEmployeeIds { get; set; }
        public string ResumeReviewerName { get; set; } = null!;
        public int CreatedByEmployeeId { get; set; }
        public string CreatedName { get; set; } = null!;
        public DateTime CreatedOnUtc { get; set; }
        public string Candidatestatus { get; set; } = null!;
        public int CandidateStatusId { get; set; }
        public string ResumePath { get; set; } = null!;
        public string Reason { get; set; } = null!;
        public string PositionTitle { get; set; } = null!;

        public string CandidateName { get; set; }

        public int MrfStatus { get; set; }
        public String MrfStatusName { get; set; }

    }
}
