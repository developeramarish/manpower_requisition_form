namespace MRF.Models.DTO
{
    
    public class MrfinterviewermapRequestModel
    {
        public int MrfId { get; set; }

        public int InterviewerEmployeeId { get; set; }

        public bool IsActive { get; set; }

        public int CreatedByEmployeeId { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public int UpdatedByEmployeeId { get; set; }

        public DateTime UpdatedOnUtc { get; set; }
    }
    public class MrfinterviewermapResponseModel
    {
        public int Id { get; set; }
    }
}
