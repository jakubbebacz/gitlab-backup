namespace Application.Models.Backup;

public class BackupResponse
{
    public Guid BackupId { get; set; }

    public int GroupId { get; set; }

    public string BackupName { get; set; } = default!;

    public string BackupPath { get; set; } = default!;


    public string BackupDescription { get; set; } = default!;


    public string Visibility { get; set; } = default!;
}