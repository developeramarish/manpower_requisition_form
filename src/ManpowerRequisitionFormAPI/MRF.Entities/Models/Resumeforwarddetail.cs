using System;
using System.Collections.Generic;

namespace MRF.Entities.Models;

public partial class Resumeforwarddetail
{
    public int Id { get; set; }

    public int CandidateId { get; set; }

    public int ForwardedFromEmployeeId { get; set; }

    public int ForwardedToEmployeeId { get; set; }

    public DateTime ForwardedOnUtc { get; set; }
}
