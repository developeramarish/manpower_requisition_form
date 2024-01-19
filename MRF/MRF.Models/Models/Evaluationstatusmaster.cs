namespace MRF.Models.Models;

public class Evaluationstatusmaster
{
    public int Id { get; set; }
    public string Status { get; set; } = null!;

    public int RoleId { get; set; }

    public bool IsActive { get; set; }

    public int CreatedByEmployeeId { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public int UpdatedByEmployeeId { get; set; }

    public int UpdatedOnUtc { get; set; }
}

public class Interviewstatus
{
    public int Id { get; set; }

    public int RoleId { get; set; }

    public string Status { get; set; } = null!;
    public string CandidateorEvalution { get; set; } = null!;

}
