namespace MRF.Models.DTO
{
    public class LocationmasterRequestModel
    {
        public string Location { get; set; } = null!;
        public string ShortCode { get; set; } = null!;
        public bool IsActive { get; set; }
        public int CreatedByEmployeeId { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public int UpdatedByEmployeeId { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
    }
    public class LocationmasterResponseModel
    {
        public int Id { get; set; }        
        public bool IsActive { get; set; }
    }
}
