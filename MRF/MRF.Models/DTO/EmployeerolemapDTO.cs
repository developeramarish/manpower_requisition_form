namespace MRF.Models.DTO
{
    public class EmployeerolemapRequestModel
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public int RoleId { get; set; }

        public bool IsActive { get; set; }

        public int CreatedByEmployeeId { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public int UpdatedByEmployeeId { get; set; }

        public DateTime UpdatedOnUtc { get; set; }
    }

    public class EmployeerolemapResponseModel
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
    }
}
