using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task_Console.Controllers;
using Task_Console.Model;
using Task_Console.Utilities;

namespace Task_Console.Views
{
    public class UserView
    {
         public static async Task<Tuple<string, string>> LoginUserView()
        {
            Console.WriteLine("Enter Your Login Details");
            Console.WriteLine("Enter your username:");
            string username =  AppView.GetUserInput();
            Console.WriteLine("Enter your password:");
            string password =  AppView.GetUserInput();
            if (Utility.ValidateStringInput(username, password))
            {
                return Tuple.Create(username, password);
            }
            else{
                AppView.ShowMessage("Invalid input. Please try again.");
                throw new ArgumentException("Invalid input.");
            }

        }

        public static async Task<Tuple<string, string>> RegisterUserView()
        {

            Console.WriteLine("Enter Your Registration Details");
            Console.WriteLine("Enter your username:");
            string username =  Console.ReadLine();
            Console.WriteLine("Enter your password:");
            string password =  Console.ReadLine();
            if (Utility.ValidateStringInput(username, password))
            {
                return Tuple.Create(username, password);
            }
            else
            {
                AppView.ShowMessage("Invalid input. Please try again.");
                throw new ArgumentException("Invalid input.");
            }
        }

        public static async Task ShowUserTasksView(List<ProjectTask> tasks)
        {
            if (tasks.Count == 0)
            {
                AppView.ShowMessage("You don't have any tasks.");
                UserController.ShowUserMenuGetOption();
            }
            else
            {
                Console.WriteLine("Here are your tasks:");
                foreach (ProjectTask task in tasks)
                {
                    Console.WriteLine(task.ToString());
                }
            }
        }

        public static async Task ShowUserTaskView(ProjectTask task)
        {
            task.printTask();
        }

        public static async Task ShowUserProject(List<Project> projects)
        {
            if (projects.Count == 0)
            {
                AppView.ShowMessage("You don't have any projects.");
                UserController.ShowUserMenuGetOption();
            }
            else
            {
                Console.WriteLine("Here are your projects:");
                foreach (Project project in projects)
                {
                    project.PrintProject();
                }
            }
        }

        public static async Task ShowUserProjectView(List<Project> projects)
        {
            Console.WriteLine("Here are your projects:");
            foreach (Project project in projects)
            {
                project.PrintProject();
            }
        }

        public static async Task<string> ShowUserMenu()
        {
            Console.WriteLine("Welcome to the Task Manager");
            Console.WriteLine("1 - Show Projects");
            Console.WriteLine("2 - Show single Project");
            Console.WriteLine("3 - Show Undone Tasks");
            Console.WriteLine("4 - Show single Task");
            Console.WriteLine("5 - Logout");
            string userInput = AppView.GetUserInput();
            if (int.TryParse(userInput, out int parsedInput) && Utility.ValidateRange(parsedInput, 1, 5))
            {
                return userInput;
            }
            else
            {
                AppView.ShowMessage("Invalid input. Please try again.");
                return await ShowUserMenu();
            }
        }
    }
}