namespace MRF.Models.DTO
{
    public class MrffeedbackRequestModel
    {
        public int MrfId { get; set; }
        public string Feedback { get; set; } = null!;
        public int FeedbackByEmployeeId { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public int UpdatedByEmployeeId { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
    }
    public class MrffeedbackResponseModel
    {
        public int Id { get; set; }
      
    }
}
