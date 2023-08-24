namespace MRF.Models.Models;

public class Evaluationstatusmaster
{
    public int Id { get; set; }

    public string Status { get; set; } = null!;

    public bool IsActive { get; set; }

    public int CreatedByEmployeeId { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public int UpdatedByEmployeeId { get; set; }

    public int UpdatedOnUtc { get; set; }
}
