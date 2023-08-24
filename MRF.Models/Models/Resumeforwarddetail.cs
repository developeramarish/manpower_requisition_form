namespace MRF.Models.Models;

public class Resumeforwarddetail
{
    public int Id { get; set; }

    public int CandidateId { get; set; }

    public int ForwardedFromEmployeeId { get; set; }

    public int ForwardedToEmployeeId { get; set; }

    public DateTime ForwardedOnUtc { get; set; }
}
