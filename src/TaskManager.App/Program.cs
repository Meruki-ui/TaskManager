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
                return;
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
        }
    }
}