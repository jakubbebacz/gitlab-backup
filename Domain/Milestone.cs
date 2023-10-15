namespace Domain;

public class Milestone
{
    public Guid MilestoneId { get; set; }

    public string MilestoneTitle { get; set; } = default!;

    public DateTime DueDate { get; set; }

    public DateTime StartDate { get; set; }

    public string? MilestoneDescription { get; init; }

    public Guid BackupId { get; set; }
    public Backup Backup { get; set; } = default!;
}