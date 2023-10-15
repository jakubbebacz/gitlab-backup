namespace Application.Models.Label;

public abstract class LabelResponse
{
    public int GroupId { get; set; }

    public string LabelName { get; set; } = default!;
    
    public string Color { get; set; } = default!;
    
    public string? LabelDescription { get; init; }
}