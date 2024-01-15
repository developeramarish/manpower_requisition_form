using System.ComponentModel.DataAnnotations.Schema;

namespace MRF.Models.Models;

public class Candidatedetails
{
    public int Id { get; set; }

    public int MrfId { get; set; }

    public string Name { get; set; } = null!;

    public string EmailId { get; set; } = null!;

    public int ContactNo { get; set; } 

    public string ResumePath { get; set; } = null!;

    public int CandidateStatusId { get; set; }
    [NotMapped]
    public int ReviewedByEmployeeId { get; set; }
    
    public String? ReviewedByEmployeeIds { get; set; }

    public int CreatedByEmployeeId { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public int UpdatedByEmployeeId { get; set; }

    public DateTime UpdatedOnUtc { get; set; }
    public string Reason { get; set; } = "";

    public int?  SourceId { get; set; }
}
