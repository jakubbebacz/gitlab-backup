namespace Application.Models.Label;

public class LabelResponse
{
    public string Name { get; set; } = default!;

    public string Color { get; set; } = default!;

    public string? Description { get; init; }
}