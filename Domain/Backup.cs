namespace Domain;

public class Backup
{
    public int Id { get; set; }
    
    public int GroupId { get; set; }
    
    public string Name { get; set; }
    
    public string Path { get; set; }
    
    public string Description { get; set; }
    
    public string Visibility { get; set; }
    
    public DateTime CreatedAt { get; set; }
}