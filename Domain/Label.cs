namespace Domain;

public class Label
{
    public Guid LabelId { get; set; }

    public string LabelName { get; set; } = default!;

    public string Color { get; set; } = default!;

    public string? LabelDescription { get; init; }

    public Guid BackupId { get; set; }
    public Backup Backup { get; set; } = default!;
}