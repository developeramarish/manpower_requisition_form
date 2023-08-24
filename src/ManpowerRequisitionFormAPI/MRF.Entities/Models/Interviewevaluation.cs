using System;
using System.Collections.Generic;

namespace MRF.Entities.Models;

public partial class Interviewevaluation
{
    public int Id { get; set; }

    public int CandidateId { get; set; }

    public int EvaluationId { get; set; }

    public int InterviewerId { get; set; }

    public DateTime EvaluationDateUtc { get; set; }

    public TimeSpan? FromTimeUtc { get; set; }

    public TimeSpan? ToTimeUtc { get; set; }

    public int EvaluationFeedbackId { get; set; }

    public int EvalutionStatusId { get; set; }

    public string FeedbackAsDraft { get; set; } = null!;

    public int CreatedByEmployeeId { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public int UpdatedByEmployeeId { get; set; }

    public DateTime? UpdatedOnUtc { get; set; }
}
