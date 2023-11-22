using System.ComponentModel.DataAnnotations.Schema;

namespace MRF.Models.Models;

public class Candidatedetails
{
    public int Id { get; set; }

    public int MrfId { get; set; }

    public string Name { get; set; } = null!;

    public string EmailId { get; set; } = null!;

    public string ContactNo { get; set; } = null!;

    public string ResumePath { get; set; } = null!;

    public int CandidateStatusId { get; set; }
   
    public int ReviewedByEmployeeId { get; set; }

    [NotMapped]
    public String? ReviewedByEmployeeIds { get; set; }

    public int CreatedByEmployeeId { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public int UpdatedByEmployeeId { get; set; }

    public DateTime UpdatedOnUtc { get; set; }
    public string Reason { get; set; } = "";
}
