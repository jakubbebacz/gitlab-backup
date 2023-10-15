namespace Application.Models.Label;

public class RestoreLabelRequest
{
    public int GroupId { get; set; }

    public string LabelName { get; set; } = default!;

    public string Color { get; set; } = default!;

    public string? LabelDescription { get; init; }
}