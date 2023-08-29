using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task_Console.Utilities;
using Task_Console.Controllers;

namespace Task_Console.Views
{
    public class AppView
    {
        public static async Task InitApp()
        {
            Console.WriteLine("1. Register user");
            Console.WriteLine("2. Login user");
            Console.WriteLine("3. Admin login");
            Console.WriteLine("4. Register admin"); // Add admin registration option
            Console.WriteLine("5. Exit");
            Console.WriteLine("Enter your choice");
            string option = GetUserInput();

            if (int.TryParse(option, out int choice))
            {
                if (Utility.ValidateRange(choice, 1, 5))
                {
                    if (choice == 3)
                    {
                        await AdminController.AdminBaseRouter(choice);
                    }
                    else if (choice == 4) // Handle admin registration option
                    {
                        await AdminController.AdminBaseRouter(choice);
                    }
                    else
                    {
                        await UserController.UserBaseRouter(option);
                    }
                }
                else
                {
                    AppView.ShowMessage("Invalid choice. Please enter a number between 1 and 5.");
                    await InitApp();
                }
            }
            else
            {
                AppView.ShowMessage("Invalid input. Please enter a valid number.");
                await InitApp();
            }
        }

        
        public static void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }
        //  get user input
        public static string GetUserInput()
        {
            return  Console.ReadLine();
        }
    }
}