namespace MRF.Models.DTO
{
    public class CandidateInterviewFeedbackRequestModel
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public int EvaluationFeedBackId { get; set; }
        public string? EvaluationFeedBack { get; set; }
        public int InterviewRound { get; set; }
        public string Comments { get; set; }
        public int? FeedbackAsDraft { get; set; }
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
