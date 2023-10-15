namespace Application.Models.Milestone;

public class MilestoneResponse
{
    public string Title { get; set; } = default!;

    public DateTime DueDate { get; set; }

    public DateTime StartDate { get; set; }

    public string? Description { get; init; }
}