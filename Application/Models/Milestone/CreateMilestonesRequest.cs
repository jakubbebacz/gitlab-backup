namespace Application.Models.Milestone;

public class CreateMilestonesRequest
{
    public int GroupId { get; set; }

    public string MilestoneTitle { get; set; } = default!;
    
    public DateTime DueDate { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public string? MilestoneDescription { get; init; }
    
    public Guid BackupId { get; set; }
}