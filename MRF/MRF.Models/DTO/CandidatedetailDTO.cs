namespace MRF.Models.DTO
{
    public class CandidatedetailRequestModel
    {
        public int Id { get; set; }

        public int MrfId { get; set; }

        public string Name { get; set; } = null!;

        public string EmailId { get; set; } = null!;

        public int ContactNo { get; set; } 

        public string ResumePath { get; set; } = null!;

        public int CandidateStatusId { get; set; }

        public int ReviewedByEmployeeId { get; set; }

        public string? ReviewedByEmployeeIds { get; set; }

        public int CreatedByEmployeeId { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public int UpdatedByEmployeeId { get; set; }

        public DateTime UpdatedOnUtc { get; set; }
        public string Reason { get; set; } = "";

        public int? SourceId { get; set; }

        public string? referenceNo { get; set; }

        public string? Positiontitle { get; set; }
    }

    public class CandidatedetailResponseModel
    {
        public int Id { get; set; }

        public string Status { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
