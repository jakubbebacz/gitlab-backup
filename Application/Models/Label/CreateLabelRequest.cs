namespace Application.Models.Label;

public class CreateLabelRequest
{
    public string LabelName { get; init; } = default!;

    public string Color { get; init; } = default!;

    public string? LabelDescription { get; init; }

    public Guid BackupId { get; init; }
}