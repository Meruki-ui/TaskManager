namespace TaskManager.App.Data;

using TaskManager.App.Models;
using Microsoft.Data.Sqlite;

public class TaskRepository
{
    private readonly string _connectionString;

    public TaskRepository()
    {
        _connectionString = "Data Source = dataBase/tasks.db";
    }
    
    public void CreateTableIfNotExists()
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();

            var createCommand = new SqliteCommand();
            createCommand.Connection = connection;
            createCommand.CommandText = @"CREATE TABLE IF NOT EXISTS Tasks (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Title TEXT NOT NULL,
            Description TEXT,
            Status INTEGER NOT NULL DEFAULT 0 CHECK (Status IN (0, 1, 2))
            );";
            createCommand.ExecuteNonQuery();
        }
        
    }

    public void AddTask(TaskItem task)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();
            var addCommand = new SqliteCommand();
            addCommand.Connection = connection;
            addCommand.CommandText = "INSERT INTO Tasks (Title, Description) VALUES ($title, $description)";
            
            addCommand.Parameters.AddWithValue("$title", task.Title);
            addCommand.Parameters.AddWithValue("$description", task.Description);
            addCommand.ExecuteNonQuery();
        }
    }
}
 
