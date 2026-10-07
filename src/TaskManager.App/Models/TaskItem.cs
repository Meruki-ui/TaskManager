namespace TaskManager.App.Models;

public class TaskItem
{
    //properties
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Status { get; set; }
}