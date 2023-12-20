using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRF.Models.Models
{
    public class CandidateInterviewFeedback
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public int EvaluationFeedBackId { get; set; }
        public int InterviewRound { get; set; }        
        public string Comments { get; set; }
        public int CreatedByEmployeeId { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public int UpdatedByEmployeeId { get; set; }
        public DateTime? UpdatedOnUtc { get; set; }
    }
}
