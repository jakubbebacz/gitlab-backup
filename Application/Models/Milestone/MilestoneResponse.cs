namespace Application.Models.Milestone;

public abstract class MilestoneResponse
{
    public int GroupId { get; set; }

    public string MilestoneTitle { get; set; } = default!;
    
    public DateTime DueDate { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public string? MilestoneDescription { get; init; }
}