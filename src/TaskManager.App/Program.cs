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
                ShowMenu();
                if (!UserInputNumber("Choose an option: ", out int userOption)) return;

                switch(userOption)
                {
                    case 1: HandleAddTask(repository); break;
                    case 2: HandleMarkAsDone(); break;
                    case 3: HandleGetAllTasks(); break;
                    case 4: HandleDeleteTask(); break;
                    case 5: keepGoing = false; break;
                    default: Console.WriteLine("Invalid option"); break;
                }

            }
            //User methods

            static void ShowMenu()
            {
                Console.WriteLine($"\n---Task Manager---"
                +"\nHere are your options:"+
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
                    Console.WriteLine("Cancelled operation");
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
            static void HandleMarkAsDone()
            {
                Console.WriteLine("Not implemented yet.");
            }
            static void HandleGetAllTasks()
            {
                Console.WriteLine("Not implemented yet.");
            }
            static void HandleDeleteTask()
            {
                Console.WriteLine("Not implemented yet.");
            }
        }
    }
}