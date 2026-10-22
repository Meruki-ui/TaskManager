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
            bool keepGoing = true;

            while (keepGoing)
            {
                ShowMenu(repository);
                if (!UserInputNumber("Choose an option: ", out int userOption)) return;

                switch(userOption)
                {
                    case 1: HandleAddTask(repository); break;
                    case 2: HandleMarkAsDone(repository); break;
                    case 3: HandleViewTasks(repository); break;
                    case 4: HandleDeleteTask(repository); break;
                    case 5: keepGoing = false; break;
                    default: Console.WriteLine("Invalid option"); break;
                }

            }
            //User methods

            static void ShowMenu(TaskRepository repository)
            {
                int pendingCount = repository.PendingTasks();
                Console.WriteLine($"\n---Task Manager---"+
                $"\nYou have {pendingCount} pending quests"+
                "\nHere are your options:"+
                "\n1- Add a task"+
                "\n2- Complete a task"+
                "\n3- Check your tasks"+
                "\n4- Delete a task"+
                "\n5- Quit");
            }

            

            static bool UserInputNumber(string prompt, out int inputResult)
            {
                Console.Write(prompt);
                
                while (true)
                {
                    string? userInput = Console.ReadLine();
                    if (userInput == "q" || userInput == "Q")
                    {
                        inputResult = 0;
                        return false;
                    }
                    else if (int.TryParse(userInput, out inputResult))
                    {
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Type a valid number or press 'Q' to quit");
                    }
                }                
            }
            static bool UserInputText(string prompt, out string inputResult,bool allowEmpty = false)
            {
                Console.Write(prompt);
                
                while (true)
                {
                    string? userInput = Console.ReadLine();
                    if (userInput == "q" || userInput == "Q")
                    {
                        inputResult = "";
                        return false;
                    }
                    if (allowEmpty)
                    {
                        inputResult = userInput ?? "";
                        return true;
                    }
                    if (!string.IsNullOrWhiteSpace(userInput))
                    {
                        inputResult = userInput ?? "";
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Type a valid message or press 'Q' to quit.");
                    }
                }    
            }

            static void HandleAddTask(TaskRepository repository)
            {
                Console.WriteLine("\nWhat's your new task?");
                if (!UserInputText("Title: ", out string title, allowEmpty: false))
                {
                    Console.WriteLine("Cancelled operation.");
                    return;
                };
                
                if (!UserInputText("Description (Optional): ", out string description, allowEmpty: true))
                {
                    Console.WriteLine("Cancelled operation.");
                    return;
                };

                Console.WriteLine($"\nTask - {title}\nDescription - {description}");
                UserInputNumber("Add task?\n1. Yes\n2. No\nType here: ", out int confirmAddTask);
                if (confirmAddTask == 2)
                {
                    Console.WriteLine("Cancelled operation.");
                    return;
                }
                
                TaskItem newTask = new TaskItem() 
                {
                    Title = title,
                    Description = description
                };
                repository.AddTask(newTask);
                Console.WriteLine("Task added successfully!");

            }
            static void HandleMarkAsDone(TaskRepository repository)
            {

                HandleGetAllTasks(repository);

                if (!UserInputNumber("\nChoose one task to mark as done by ID: ", out int inputResult))
                {
                    Console.WriteLine("Cancelled operation.");
                    return;
                }

                var taskList = repository.GetAllTasks();

                bool found = false;

                foreach (var task in taskList)
                {
                    if (inputResult == task.Id)
                    {
                        repository.MarkAsDone(inputResult);
                        found = true;
                        break;
                    } 
                }
                
                if (found)
                {
                    Console.WriteLine("Task marked as done!");
                }
                else
                {
                   Console.WriteLine("No matching ID was found."); 
                }

                Console.WriteLine("Press any key to return to menu.");
                Console.ReadKey();
            }

            static void HandleViewTasks(TaskRepository repository)
            {
                if(!UserInputNumber("\n1. View all tasks"+
                "\n2. Filter tasks\n", out int userOption))
                {
                    Console.WriteLine("Cancelled operation.");
                    return; 
                }

                switch (userOption)
                {
                    case 1: 
                        HandleGetAllTasks(repository); 
                        break;
                    case 2: 
                        HandleGetTaskByStatus(repository); 
                        break;
                    default: 
                        Console.WriteLine("Invalid option. Press any key to return");
                        Console.ReadKey(); 
                        break;
                }
            }
            

            static void HandleGetAllTasks(TaskRepository repository)
            {
                var taskList = repository.GetAllTasks();

                foreach (var task in taskList)
                {
                    string statusText = task.Status switch
                    {
                        0 => "Pending",
                        1 => "Doing",
                        2 => "Done",
                        _ => "Unknow",
                    };
                    Console.WriteLine($"Id. {task.Id} - {task.Title}\nStatus: {statusText}\nDesc: {task.Description}\n");
                }
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
            }

            static void HandleGetTaskByStatus(TaskRepository repository)
            {
                Console.WriteLine("Filter tasks by: ");
                if (!UserInputNumber("\n1. Pending\n2. Doing\n3. Done\n", out int status))
                {
                    Console.WriteLine("Cancelled operation.");
                    return;
                }

                switch (status)
                {
                    case 1: status = 0; break;
                    case 2: status = 1; break;
                    case 3: status = 2; break;
                    default: 
                        Console.WriteLine("Invalid option. Press any key to continue");
                        Console.ReadKey(); 
                        break;
                }
                
                var taskList = repository.GetTaskByStatus(status);

                if (taskList.Count == 0)
                {
                    Console.WriteLine("No task found with that status. Press any key to return.");
                    Console.ReadKey();
                    return;
                }

                else
                {
                    foreach (var task in taskList)
                    {
                        string statusText = task.Status switch
                        {
                            0 => "Pending",
                            1 => "Doing",
                            2 => "Done",
                            _ => "Unknow",
                        };
                        Console.WriteLine($"Id. {task.Id} - {task.Title}\nStatus: {statusText}\nDesc: {task.Description ?? "None"}\n");
                    }
                    Console.WriteLine("Press any button to return.");
                    Console.ReadKey();
                }                
            }

            static void HandleDeleteTask(TaskRepository repository)
            {
                HandleGetAllTasks(repository);

                if (!UserInputNumber("Choose a task to delete by ID: ", out int inputResult))
                {
                    Console.WriteLine("Cancelled operation.");
                    return;
                }
                var taskList = repository.GetAllTasks();

                bool found = false;

                foreach (var task in taskList)
                {
                    if(inputResult == task.Id)
                    {
                        found = true;
                        Console.WriteLine($"Are you sure you want to delete '{task.Title}'?");
                        if (!UserInputNumber("1. Yes\n2. No\n", out int confirmDelete))
                        {
                            return;
                        }
                        if (confirmDelete != 1)
                        {
                            Console.WriteLine("Cancelled operation.");
                            return;
                        }

                        repository.DeleteTask(inputResult);
                        Console.WriteLine("Task deleted successfully. Press any key to return to menu.");
                        Console.ReadKey();
                        break;
                    }
                }
                if (!found)
                {
                    Console.WriteLine("No matching ID was found. Press any key to return to menu.");
                    Console.ReadKey();
                }
            }
        }
    }
}