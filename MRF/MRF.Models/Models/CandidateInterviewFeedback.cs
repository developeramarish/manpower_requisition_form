using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        [NotMapped]
        public string? EvaluationFeedBack { get; set; }
        public int InterviewRound { get; set; }        
        public string Comments { get; set; }
        public int CreatedByEmployeeId { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public int UpdatedByEmployeeId { get; set; }
        public DateTime? UpdatedOnUtc { get; set; }
    }
}
