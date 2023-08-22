namespace MRF.Models.DTO
{
    public class CandidatestatusmasterRequestModel
    {
        public string Status { get; set; } = null!;
        public bool IsActive { get; set; }
        public int CreatedByEmployeeId { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public int UpdatedByEmployeeId { get; set; }
        public DateTime UpdatedOnUtc { get; set; }

    }
    public class CandidatestatusmasterResponseModel
    {
        public int Id { get; set; }
        public string Status { get; set; } = null!;
        public bool IsActive { get; set; }      
    }
}
