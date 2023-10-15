namespace Application.Models.Group;

public class GroupResponse
{
    public int GroupId { get; set; }

    public string GroupName { get; set; } = default!;

    public string GroupPath { get; set; } = default!;


    public string GroupDescription { get; set; } = default!;


    public string Visibility { get; set; } = default!;
}