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
        public int TotalCount { get; set; }
        public string ReferenceNo { get; set; } = null!;
        public int EmployeeId { get; set; }
        public int RoleId { get; set; }
        
        public string  PositionTitle { get; set; } = null!;

    }

    public class MrfSummaryViewModel: MrfDashboardViewModel
    {
       public string Status { get; set; } = null!;
       public int MrfStatusId { get; set; }
    }

    public class MrfResumeSummaryViewModel : MrfDashboardViewModel
    {
        public string Candidatestatus { get; set; } = null!;
        public int statusID { get; set; }
        public int TotalCount { get; set; }

        public DateTime UpdatedOnUtc { get; set; }

    }

    public class ResultViewModel
{
        public int mrfId { get; set; }
        public string referenceno { get; set; }

        public string positionTitle { get; set; }
        public List<ResultGroup> resultGroups { get; set; }
}

    public class ResultGroup
    {
        public string Candidatestatus { get; set; } = null!;
        public int TotalstatusCount { get; set; }
    }

    public class MrfInterviewSummaryViewModel : MrfDashboardViewModel
    {
        // join in Candidatedetails and Interviewevaluation and Evaluationmaster
        public int CandidateId { get; set; }
        public int EvaluationId { get; set; }
        public string Type { get; set; } = null!;
        public string Candidatestatus { get; set; } = null!;
        public int ITotalCount { get; set; }
        
    }

}