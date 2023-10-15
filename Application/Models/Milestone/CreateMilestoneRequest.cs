namespace Application.Models.Milestone;

public class CreateMilestoneRequest
{
    public string MilestoneTitle { get; init; } = default!;

    public DateTime DueDate { get; init; }

    public DateTime StartDate { get; init; }

    public string? MilestoneDescription { get; init; }

    public Guid BackupId { get; init; }
}