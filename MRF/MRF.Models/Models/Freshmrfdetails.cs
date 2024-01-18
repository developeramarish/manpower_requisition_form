namespace MRF.Models.Models;

public class Freshmrfdetails
{
    public int Id { get; set; }

    public int MrfId { get; set; }

    public string Justification { get; set; } = null!;

    public string JobDescription { get; set; } = null!;

    public string Skills { get; set; } = null!;

    public float MinTargetSalary { get; set; }

    public float MaxTargetSalary { get; set; }

    public int CreatedByEmployeeId { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public int UpdatedByEmployeeId { get; set; }

    public DateTime UpdatedOnUtc { get; set; }
}
