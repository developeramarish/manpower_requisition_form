﻿namespace MRF.Models.Models;

public class Interviewevaluation
{
    public int Id { get; set; }

    public int CandidateId { get; set; }

    public int EvaluationId { get; set; }

    public int InterviewerId { get; set; }

    public DateOnly EvaluationDateUtc { get; set; }

    public TimeOnly? FromTimeUtc { get; set; }

    public TimeOnly? ToTimeUtc { get; set; }

    public int EvaluationFeedbackId { get; set; }

    public int EvalutionStatusId { get; set; }

    public string FeedbackAsDraft { get; set; } = null!;

    public int CreatedByEmployeeId { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public int UpdatedByEmployeeId { get; set; }

    public DateTime? UpdatedOnUtc { get; set; }
}
