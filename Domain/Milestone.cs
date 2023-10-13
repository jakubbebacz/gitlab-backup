namespace Domain;

public class Milestone
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public string State { get; set; }
    
    public DateTime DueDate { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public bool IsExpired { get; set; }

    public int GroupId { get; set; }
}