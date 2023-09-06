namespace MRF.Models.DTO
{
    public class ResumeforwarddetailRequestModel
    {
        public int CandidateId { get; set; }
        public int ForwardedFromEmployeeId { get; set; }
        public int ForwardedToEmployeeId { get; set; }
        public DateTime ForwardedOnUtc { get; set; }
    }
    public class ResumeforwarddetailResponseModel
    {
        public int Id { get; set; }
    }
}
