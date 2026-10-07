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


}
 
