namespace TaskManager.App.Models;

public class TaskItem
{
    //properties
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Status { get; set; }
    public int Priority { get; set; }
    public string? CreatedAt { get; set; }
    public string? DueDate { get; set; }
    public string? CompletedAt { get; set; }
}