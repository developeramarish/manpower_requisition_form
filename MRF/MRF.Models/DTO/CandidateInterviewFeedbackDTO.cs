namespace MRF.Models.DTO
{
    public class CandidateInterviewFeedbackRequestModel
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public string SoftSkills { get; set; }
        public string HardSkills { get; set; }
        public string RequiredTraining { get; set; }
        public string Comments { get; set; }
        public int CreatedByEmployeeId { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public int UpdatedByEmployeeId { get; set; }
        public DateTime? UpdatedOnUtc { get; set; }
    }
    public class CandidateInterviewFeedbackResponseModel
    {
        public int Id { get; set; }
    }
}
