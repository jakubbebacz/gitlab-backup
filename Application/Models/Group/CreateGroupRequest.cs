namespace Application.Models.Group;

public class CreateGroupRequest
{
    public string Name { get; init; } = default!;

    public string Path { get; init; } = default!;

    public string? Description { get; set; }

    public string? Visibility { get; set; }
}