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
                    case 1: HandleAddTask(); break;
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
                Console.WriteLine($"---Task Manager---"
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
            static bool UserInputText(string prompt, out string inputResult)
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
                    else if (!string.IsNullOrWhiteSpace(userInput))
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

            static void HandleAddTask()
            {
                Console.WriteLine("Not implemented yet.");
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