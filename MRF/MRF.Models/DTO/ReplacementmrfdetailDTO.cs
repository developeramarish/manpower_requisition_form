namespace MRF.Models.DTO
{
    public class ReplacementmrfdetailRequestModel
    {
        public int MrfId { get; set; }
        public string EmployeeName { get; set; } = null!;
        public string EmailId { get; set; } = null!;
        public int EmployeeCode { get; set; }
        public DateOnly LastWorkingDate { get; set; }
        public string Justification { get; set; } = null!;
        public int AnnualCtc { get; set; }
        public int AnnualGross { get; set; }
        public int GradeId { get; set; }
        public int CreatedByEmployeeId { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public int UpdatedByEmployeeId { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
    }
    public class ReplacementmrfdetailResponseModel
    {
        public int Id { get; set; }
    }
}
