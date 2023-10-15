namespace Application.Models.Group;

public class CreateGroupRequest
{
    public string Name { get; set; } = default!;

    public string Path { get; set; } = default!;
    
    public string? Description { get; set; }
    
    public string? Visibility { get; set; }
}