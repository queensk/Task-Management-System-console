using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task_Console.Controllers;
using Task_Console.Model;
using Task_Console.Utilities;

namespace Task_Console.Views
{
    public class AdminView
    {

        private static bool isInitialAdminCreated = false;
        private static string initialAdminUsername;
        private static string initialAdminPassword;
        private static Admin loginAdmin;

        public static async Task<Tuple<string, string>> RegisterAdminView()
        {
            Console.WriteLine("Enter Admin Registration Details");
            Console.WriteLine("Enter admin username:");
            string username = Console.ReadLine();
            Console.WriteLine("Enter admin password:");
            string password = Console.ReadLine();

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

        public static async Task<Tuple<string, string>> AdminLoginView()
{
    Console.WriteLine("Enter Admin Login Details");
    Console.WriteLine("Enter admin username:");
    string username = Console.ReadLine();
    Console.WriteLine("Enter admin password:");
    string password = Console.ReadLine();

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


        // public static async Task<Admin> CreateInitialAdminView()
        // {
        // // Console.WriteLine("Calling CreateInitialAdminView");

        //     if (isInitialAdminCreated)
        //     {
        //         // Console.WriteLine("Initial Admin Account Setup - Step 1");
        //         Console.WriteLine("Initial admin account has already been set up.");
        //     }
        //     else
        //     {
        //         Console.WriteLine("Initial Admin Account Setup");
        //         Console.WriteLine("Enter admin username:");
        //         initialAdminUsername = GetUserInput();
        //         Console.WriteLine("Enter admin password:");
        //         initialAdminPassword = GetUserInput();
        //         isInitialAdminCreated = true;
        //         loginAdmin = new Admin { Username = initialAdminUsername, Password = initialAdminPassword };
        //     }

        //     Console.WriteLine("Enter admin username:");
        //     string username = GetUserInput();
        //     Console.WriteLine("Enter admin password:");
        //     string password = GetUserInput();

        //     if (username == initialAdminUsername && password == initialAdminPassword)
        //     {
        //         Console.WriteLine($"Username: {username}");
        //         Console.WriteLine($"Password: {password}");
        //         Console.WriteLine($"Admin: {username} - {password}");
            
        //         return new Admin { Username = username, Password = password };
        //     }
        //     else
        //     {
        //         ShowMessage("Invalid input. Please try again.");
        //         return await CreateInitialAdminView();
        //     }
        // }

        public static async Task<string> ShowAdminMenu()
        {
            Console.WriteLine("Admin Menu");
            Console.WriteLine("1. Create Project");
            Console.WriteLine("2. Update Project");
            Console.WriteLine("3. Delete Project");
            Console.WriteLine("4. Create Task");
            Console.WriteLine("5. Update Task");
            Console.WriteLine("6. Delete Task");
            Console.WriteLine("7. Logout");
            Console.WriteLine("Enter your choice:");
            
            string userInput = GetUserInput();
            
            if (int.TryParse(userInput, out int parsedInput) && Utility.ValidateRange(parsedInput, 1, 7))
            {
                return userInput;
            }
            else
            {
                ShowMessage("Invalid input. Please enter a valid option.");
                return await ShowAdminMenu();
            }
        }

        public static Project GetProjectInput()
        {
            Console.WriteLine("Enter Project Name:");
            string projectName = GetUserInput();

            return new Project { Name = projectName };
        }

        public static void ShowProjectList(List<Project> projects)
        {
            Console.WriteLine("Available Projects:");
            foreach (var project in projects)
            {
                Console.WriteLine($"{project.Id}. {project.Name}");
            }
        }

        public static int GetProjectIdInput()
        {
            Console.WriteLine("Enter Project ID:");
            string projectIdInput = GetUserInput();

            if (int.TryParse(projectIdInput, out int projectId))
            {
                return projectId;
            }
            else
            {
                ShowMessage("Invalid input. Please enter a valid Project ID.");
                return GetProjectIdInput();
            }
        }

        public static ProjectTask GetTaskInput()
        {
            Console.WriteLine("Enter Task Description:");
            string taskDescription = GetUserInput();

            return new ProjectTask { Description = taskDescription };
        }

        public static int GetTaskIdInput()
        {
            Console.WriteLine("Enter Task ID:");
            string taskIdInput = GetUserInput();

            if (int.TryParse(taskIdInput, out int taskId))
            {
                return taskId;
            }
            else
            {
                ShowMessage("Invalid input. Please enter a valid Task ID.");
                return GetTaskIdInput();
            }
        }

        public static async Task<Project> CreateProjectView()
        {
            Console.WriteLine("Enter project name:");
            string projectName = GetUserInput();

            return new Project
            {
                Name = projectName
            };
        }

        public static async Task<Project> SelectProjectToUpdate(List<Project> projects)
        {
            Console.WriteLine("Select a project to update:");
            ShowProjectList(projects);

            string projectIdInput = GetUserInput();
            if (int.TryParse(projectIdInput, out int projectId))
            {
                Project selectedProject = projects.FirstOrDefault(p => p.Id == projectId);
                if (selectedProject != null)
                {
                    return selectedProject;
                }
                else
                {
                    ShowMessage("Invalid project ID.");
                    return await SelectProjectToUpdate(projects);
                }
            }
            else
            {
                ShowMessage("Invalid input. Please enter a valid Project ID.");
                return await SelectProjectToUpdate(projects);
            }
        }


        public static async Task<Project> UpdateProjectView(Project project)
        {
            Console.WriteLine($"Updating project: {project.Name}");
            Console.WriteLine("Enter new project name:");
            string newProjectName = GetUserInput();

            project.Name = newProjectName;
            return project;
        }

        public static async Task<Project> SelectProjectToDelete(List<Project> projects)
        {
            Console.WriteLine("Select a project to delete:");
            ShowProjectList(projects);

            string projectIdInput = GetUserInput();
            if (int.TryParse(projectIdInput, out int projectId))
            {
                Project selectedProject = projects.FirstOrDefault(p => p.Id == projectId);
                if (selectedProject != null)
                {
                    return selectedProject;
                }
                else
                {
                    ShowMessage("Invalid project ID.");
                    return await SelectProjectToDelete(projects);
                }
            }
            else
            {
                ShowMessage("Invalid input. Please enter a valid Project ID.");
                return await SelectProjectToDelete(projects);
            }
        }

        public static async Task<Tuple<int, string, bool>> CreateTaskView()
        {
            Console.WriteLine("Enter task description:");
            string description = GetUserInput();

            // Get other inputs (int projectId and bool isCompleted)
            int projectId = GetProjectIdInput();
            bool isCompleted = GetIsCompletedInput();

            return Tuple.Create(projectId, description, isCompleted);
        }

        private static bool GetIsCompletedInput()
        {
            Console.WriteLine("Is the task completed? (yes/no):");
            string input = GetUserInput();

            if (input.ToLower() == "yes")
            {
                return true;
            }
            else if (input.ToLower() == "no")
            {
                return false;
            }
            else
            {
                ShowMessage("Invalid input. Please enter 'yes' or 'no'.");
                return GetIsCompletedInput();
            }
        }


        public static async Task<int> SelectTaskToUpdate(List<ProjectTask> tasks)
        {
            Console.WriteLine("Select a task to update:");
            ShowTaskList(tasks);

            string taskIdInput = await GetUserInputAsync();
            if (int.TryParse(taskIdInput, out int taskId))
            {
                return taskId;
            }
            else
            {
                ShowMessage("Invalid task ID.");
                return await SelectTaskToUpdate(tasks);
            }
        }

        private static async Task<string> GetUserInputAsync()
        {
            return await Console.In.ReadLineAsync();
        }


        public static async Task<Tuple<string, bool>> UpdateTaskView(ProjectTask task)
        {
            Console.WriteLine($"Updating task: {task.Description}");
            Console.WriteLine("Enter new task description:");
            string newDescription = GetUserInput();

            Console.WriteLine("Enter true if the task is completed, otherwise enter false:");
            string isCompletedInput = GetUserInput();
            bool newIsCompleted;
            if (bool.TryParse(isCompletedInput, out newIsCompleted))
            {
                return Tuple.Create(newDescription, newIsCompleted);
            }
            else
            {
                ShowMessage("Invalid input. Please enter true or false.");
                return await UpdateTaskView(task);
            }
        }


        public static async Task<ProjectTask> SelectTaskToDelete(List<ProjectTask> tasks)
        {
            Console.WriteLine("Select a task to delete:");
            ShowTaskList(tasks);

            string taskIdInput = GetUserInput();
            if (int.TryParse(taskIdInput, out int taskId))
            {
                ProjectTask selectedTask = tasks.FirstOrDefault(t => t.Id == taskId);
                if (selectedTask != null)
                {
                    return selectedTask;
                }
                else
                {
                    ShowMessage("Invalid task ID.");
                    return await SelectTaskToDelete(tasks);
                }
            }
            else
            {
                ShowMessage("Invalid input. Please enter a valid Task ID.");
                return await SelectTaskToDelete(tasks);
            }
        }


        private static void ShowTaskList(List<ProjectTask> tasks)
        {
            Console.WriteLine("Available Tasks:");
            foreach (var task in tasks)
            {
                Console.WriteLine($"{task.Id}. {task.Description}");
            }
        }

        public static void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        public static string GetUserInput()
        {
            return Console.ReadLine();
        }
    }
}

