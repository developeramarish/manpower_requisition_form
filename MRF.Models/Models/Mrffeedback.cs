namespace MRF.Models.Models;

public class Mrffeedback
{
    public int Id { get; set; }

    public int MrfId { get; set; }

    public string Feedback { get; set; } = null!;

    public int FeedbackByEmployeeId { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public int UpdatedByEmployeeId { get; set; }

    public DateTime UpdatedOnUtc { get; set; }
}
