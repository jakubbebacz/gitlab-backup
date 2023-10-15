namespace Application.Models.Label;

public class CreateLabelsRequest
{
    public int GroupId { get; set; }

    public string LabelName { get; set; } = default!;
    
    public string Color { get; set; } = default!;
    
    public string? LabelDescription { get; init; }
    
    public Guid BackupId { get; set; }
}