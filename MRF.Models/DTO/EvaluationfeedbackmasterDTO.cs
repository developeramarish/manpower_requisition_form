namespace MRF.Models.DTO
{
    public class EvaluationfeedbackmasterRequestModel
    {
        public string Description { get; set; } = null!;

        public bool IsActive { get; set; }

        public int CreatedByEmployeeId { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public int UpdatedByEmployeeId { get; set; }

        public DateTime UpdatedOnUtc { get; set; }
    }
    public class EvaluationfeedbackmasterResponseModel
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
    }
}
