namespace Application.Models.Group;

public class GroupResponse
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;

    public string Path { get; set; } = default!;


    public string Description { get; set; } = default!;


    public string Visibility { get; set; } = default!;
}