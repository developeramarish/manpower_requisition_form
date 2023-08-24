namespace MRF.Models.DTO
{

    public class RolemasterRequestModel
    {
        public string Name { get; set; } = null!;
        public bool IsActive { get; set; }
        public int CreatedByEmployeeId { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public int UpdatedByEmployeeId { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
    }
    public class RolemasterResponseModel
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
    }
}
