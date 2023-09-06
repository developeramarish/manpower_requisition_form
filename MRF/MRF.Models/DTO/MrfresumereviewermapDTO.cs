namespace MRF.Models.DTO
{ 
    public class MrfresumereviewermapRequestModel
    {
        public int MrfId { get; set; }
        public int ResumeReviewerEmployeeId { get; set; }
        public bool IsActive { get; set; }
        public int CreatedByEmployeeId { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public int UpdatedByEmployeeId { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
    }
    public class MrfresumereviewermapResponseModel
    {
        public int Id { get; set; }      
    }
}
