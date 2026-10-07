using TaskManager.App.Data;
using TaskManager.App.Models;
using System;

namespace TaskManager.App
{
    class Program
    {
        static void Main(string[] args)
        {
            //main program here
            var repository = new TaskRepository();
            repository.CreateTableIfNotExists();

            var task = new TaskItem()
            {
              Title = "Buy milk",
              Description = "Go to the store buy milk"
            };
            //repository.AddTask(task);

            var tasks = repository.GetAllTasks();
            Console.WriteLine("These are your tasks:\n");
            foreach(var t in tasks)
            {
                Console.WriteLine($"Id- {t.Id} - {t.Title}\nDescription: {t.Description}\n");
            }

        }
    }
}