namespace Domain;

public class Backup
{
    public Guid BackupId { get; set; }

    public int GroupId { get; set; }

    public string BackupName { get; set; } = default!;

    public string BackupPath { get; set; } = default!;

    public string? BackupDescription { get; init; }

    public string? Visibility { get; init; }

    public DateTime CreatedAt { get; set; }

    public ICollection<Label>? Labels { get; set; }

    public ICollection<Milestone>? Milestones { get; set; }
}