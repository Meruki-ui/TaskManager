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

            //test for initializing table
            var repository = new TaskRepository();
            repository.CreateTableIfNotExists();

            //test for creating a new task and adding it to the repository created
            var task = new TaskItem()
            {
              Title = "Buy milk",
              Description = "Go to the store buy milk"
            };
            //repository.AddTask(task);

            //test for the display of getalltasks method
            var tasks = repository.GetAllTasks();
            Console.WriteLine("These are your tasks:\n");
            foreach(var t in tasks)
            {
                Console.WriteLine($"Id-{t.Id} - {t.Title}\nDescription: {t.Description}\nStatus: {t.Status}\n");
            }

            //test for markasdone method
            if (tasks.Count > 0)
            {
                repository.MarkAsDone(tasks[0].Id);
            }

            //calling get all tasks again to test if markasdone is working
            tasks = repository.GetAllTasks();
            Console.WriteLine("These are your tasks:\n");
            foreach(var t in tasks)
            {
                Console.WriteLine($"Id-{t.Id} - {t.Title}\nDescription: {t.Description}\nStatus: {t.Status}\n");
            }
        }
    }
}