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
            addCommand.CommandText = "INSERT INTO Tasks (Title, Description, Priority, CreatedAt, DueDate) VALUES ($title, $description, $priority, CURRENT_TIMESTAMP, $dueDate)";
            
            addCommand.Parameters.AddWithValue("$title", task.Title);
            addCommand.Parameters.AddWithValue("$description", task.Description);
            addCommand.Parameters.AddWithValue("$priority", task.Priority);
            addCommand.Parameters.AddWithValue("$dueDate", task.DueDate ?? (object)DBNull.Value);

            addCommand.ExecuteNonQuery();
        }
    }

    public List<TaskItem> GetAllTasks()
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();
            var getAllCommand = new SqliteCommand();
            getAllCommand.Connection = connection;
            getAllCommand.CommandText = "SELECT * FROM Tasks ORDER BY Id DESC";
            using var reader = getAllCommand.ExecuteReader(); 

            var tasks = new List<TaskItem>();

            while (reader.Read())
            {
                TaskItem taskItem = new TaskItem()
                {
                  Id = reader.GetInt32(0),
                  Title = reader.GetString(1),
                  Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                  Status = reader.GetInt32(3),
                  Priority = reader.GetInt32(4),
                  CreatedAt = reader.IsDBNull(5) ? null : reader.GetString(5),
                  DueDate = reader.IsDBNull(6) ? null : reader.GetString(6),
                  CompletedAt = reader.IsDBNull(7) ? null : reader.GetString(7)
                };

                tasks.Add(taskItem);
            }
            return tasks;
        }
    }

    public List<TaskItem> GetTaskByStatus(int status)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();
            var getByStatusCommand = new SqliteCommand();
            getByStatusCommand.Connection = connection;
            getByStatusCommand.CommandText = "SELECT * FROM Tasks WHERE Status = $status";
            getByStatusCommand.Parameters.AddWithValue("$status", status);
            using var reader = getByStatusCommand.ExecuteReader(); 

            var taskByStatus = new List<TaskItem>();

            while (reader.Read())
            {
                TaskItem taskItem = new TaskItem()
                {
                  Id = reader.GetInt32(0),
                  Title = reader.GetString(1),
                  Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                  Status = reader.GetInt32(3),
                  Priority = reader.GetInt32(4),
                  CreatedAt = reader.IsDBNull(5) ? null : reader.GetString(5),
                  DueDate = reader.IsDBNull(6) ? null : reader.GetString(6),
                  CompletedAt = reader.IsDBNull(7) ? null : reader.GetString(7)
                };

                taskByStatus.Add(taskItem);
            }
            return taskByStatus;
        }
    }

    public void UpdateTaskStatus(int id, int newStatus)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();
            var updateTaskCommand = new SqliteCommand();
            updateTaskCommand.Connection = connection;
            updateTaskCommand.CommandText = @"UPDATE Tasks
            SET Status = $newStatus, 
                CompletedAt = CASE
                    WHEN $newStatus = 2 THEN CURRENT_TIMESTAMP
                    ELSE NULL
                END
            WHERE Id = $id";

            updateTaskCommand.Parameters.AddWithValue("$id", id);
            updateTaskCommand.Parameters.AddWithValue("$newStatus", newStatus);
            updateTaskCommand.ExecuteNonQuery();
        }
    }

    public void DeleteTask(int id)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();
            var deleteTaskCommand = new SqliteCommand();
            deleteTaskCommand.Connection = connection;
            deleteTaskCommand.CommandText = "DELETE FROM Tasks WHERE Id = $id";

            deleteTaskCommand.Parameters.AddWithValue("$id", id);
            deleteTaskCommand.ExecuteNonQuery();
        }
    }

    public void ClearCompletedTask(int status)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();
            var clearCompletedCommand = new SqliteCommand();
            clearCompletedCommand.Connection = connection;
            clearCompletedCommand.CommandText = "DELETE FROM Tasks WHERE Status = $status";

            clearCompletedCommand.Parameters.AddWithValue("$status", status);
            clearCompletedCommand.ExecuteNonQuery();
        }
    }

    public int PendingTasks()
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();
            var pendingTaskCommand = new SqliteCommand();
            pendingTaskCommand.Connection = connection;
            pendingTaskCommand.CommandText = "SELECT COUNT(*) FROM Tasks WHERE Status = 0";

            int pendingTasksCount = Convert.ToInt32(pendingTaskCommand.ExecuteScalar());

            
            return pendingTasksCount;
        }
    }
}
 
