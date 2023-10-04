using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRF.Models.ViewModels
{
    public class MrDashboardViewModel
    {
        public List<MrfSummaryViewModel> Mrfstatus { get; set; }
        public List<MrfResumeSummaryViewModel> MrfRstatus { get; set; }
        public List<MrfInterviewSummaryViewModel> MrIfstatus { get; set; }
    }


        public class MrfDashboardViewModel
    {
        public int MrfId { get; set; }
        public int StatusCount { get; set; }
        public string ReferenceNo { get; set; } = null!;
        public int EmployeeId { get; set; }
        public int RoleId { get; set; }

    }

    public class MrfSummaryViewModel: MrfDashboardViewModel
    {
       public string Status { get; set; } = null!;
       public int MrfStatusId { get; set; }
    }

    public class MrfResumeSummaryViewModel : MrfDashboardViewModel
    {
        // join in Candidatestatusmaster and Candidatedetails
        public int CandidateId { get; set; } 
        public string Candidatestatus { get; set; } = null!;
        public int CandidateStatusId { get; set; }
    }

    public class MrfInterviewSummaryViewModel : MrfDashboardViewModel
    {
        // join in Candidatedetails and Interviewevaluation and Evaluationmaster
        public int CandidateId { get; set; }
        public int EvaluationId { get; set; }
        public string Type { get; set; } = null!;
        public string Candidatestatus { get; set; } = null!;
    }

}