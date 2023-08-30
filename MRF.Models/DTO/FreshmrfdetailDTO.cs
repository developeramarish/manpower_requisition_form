namespace MRF.Models.DTO
{
    public class FreshmrfdetailRequestModel
    {
        public int Id { get; set; }

        public int MrfId { get; set; }

        public string Justification { get; set; } = null!;

        public string SoftwaresRequired { get; set; } = null!;

        public string HardwaresRequired { get; set; } = null!;

        public int MinTargetSalary { get; set; }

        public int MaxTargetSalary { get; set; }

        public int CreatedByEmployeeId { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public int UpdatedByEmployeeId { get; set; }

        public DateTime UpdatedOnUtc { get; set; }
    }

    public class FreshmrfdetailResponseModel
    {
       public int Id { get; set; }
    }
}
