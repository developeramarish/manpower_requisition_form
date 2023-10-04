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
        public int InterviewerEmployeeId { get; set; }
        public string InterviewerName { get; set; } = null!;
        public int CreatedByEmployeeId { get; set; }
        public string CreatedName { get; set; } = null!;
        public DateTime CreatedOnUtc { get; set; }
        public int CandidateId { get; set; }
        public int CandidateStatusId { get; set; }
        public string Candidatestatus { get; set; } = null!;
      
        public DateTime CandidateStatusChangedOnUtc { get; set; }
        public string ResumePath { get; set; } = null!;
        public int EvaluationId { get; set; }
        public int EvaluationFeedbackId { get; set; }

        
        public string EvalutionStatus { get; set; } = null!;

        public string Attachment { get; set; } = null!;

    }
}
