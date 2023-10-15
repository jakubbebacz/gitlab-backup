namespace Application.Models.Backup;

public class CreateBackupRequest
{
    public int GroupId { get; set; }

    public string BackupName { get; set; } = default!;

    public string BackupPath { get; set; } = default!;
    
    public string? BackupDescription { get; init; }
    
    public string? Visibility { get; init; }
    
    public DateTime CreatedAt { get; set; }
}