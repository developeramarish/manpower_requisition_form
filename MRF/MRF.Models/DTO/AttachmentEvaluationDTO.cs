using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRF.Models.DTO
{
    public class AttachmentEvaluationRequestModel
    {
        public int Id { get; set; }
        public int InterviewEvaluationId { get; set; }

        public string FilePath { get; set; } = null!;
        public int CreatedByEmployeeId { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public int UpdatedByEmployeeId { get; set; }

        public DateTime? UpdatedOnUtc { get; set; }
    }
    public class AttachmentEvaluationResponseModel
    {
        public int Id { get; set; }

        public string Status { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
