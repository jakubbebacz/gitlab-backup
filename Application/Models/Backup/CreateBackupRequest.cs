namespace Application.Models.Backup;

public class CreateBackupRequest
{
    public int GroupId { get; init; }

    public string BackupName { get; init; } = default!;

    public string BackupPath { get; init; } = default!;

    public string? BackupDescription { get; init; }

    public string? Visibility { get; init; }

    public DateTime CreatedAt { get; init; }
}