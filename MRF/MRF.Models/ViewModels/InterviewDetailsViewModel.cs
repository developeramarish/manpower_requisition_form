using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRF.Models.ViewModels
{
    public class InterviewDetailsViewModel
    {


        public int MrfId { get; set; }
        public string ReferenceNo { get; set; } = null!;
        public int? PositionTitleId { get; set; }
        public string PositionTitle { get; set; } = null!;

        public int InterviewerEmployeeId { get; set; }
        public string? InterviewerEmployeeIds { get; set; }
        public string? InterviewerName { get; set; } 
        public int CreatedByEmployeeId { get; set; }
        public string CreatedName { get; set; } = null!;
        public DateTime CreatedOnUtc { get; set; }
        public int CandidateId { get; set; }
        public int? CandidateStatusId { get; set; }
        public string? Candidatestatus { get; set; }

        public DateTime? CandidateStatusChangedOnUtc { get; set; }
        public string ResumePath { get; set; } = null!;
        public int EvaluationId { get; set; }
        public int EvaluationFeedbackId { get; set; }

        public int? RoleId { get; set; }
        public string EvalutionStatus { get; set; } = null!;

        public string? Attachment { get; set; }
        public int? InterviewevaluationId { get; set; }
        public int? EvalutionStatusId { get; set; }

        public string  CandidateName { get; set; } = null!;

        public int?  mrfStatusId { get; set; }

    }


    public class InterviewStatus
    {

        public int? CandidateId { get; set; }

        public int?  RoleId { get; set; }
        public int? EvalutionStatusId { get; set; }
        public string? EvalutionStatus { get; set; }
        public int? InterviewevaluationId { get; set; }
        public DateTime? CandidateStatusChangedOnUtc { get; set; }
    }
}
