namespace Application.Models.Milestone;

public class RestoreMilestoneRequest
{
    public int GroupId { get; set; }

    public string MilestoneTitle { get; init; } = default!;

    public DateTime DueDate { get; set; }

    public DateTime StartDate { get; set; }

    public string? MilestoneDescription { get; init; }
}