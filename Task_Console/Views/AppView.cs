using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task_Console.Views
{
    public class AppView
    {
        public static async Task InitApp()
        {
            Console.WriteLine("1. Register user");
            Console.WriteLine("2. Login user");
            Console.WriteLine("3. Exit");
            Console.WriteLine("Enter your choice");
            await GetUserInput();
        }

        public static async Task RoutUserInit(string Option )
        {
            switch (Option)
            {
                case "1":
                    await RegisterUser();
                    break;
                case "2":
                    await LoginUser();
                    break;
                case "3":
                    await ExitApp();
                    break;
                default:
                    await ShowMessage("Invalid option");
                    await InitApp();
                    break;
            }
        }

        public static async Task ShowMessage(string message)
        {
            Console.WriteLine(message);
        }


        //  get user input
        public static async Task<string> GetUserInput()
        {
            return Console.ReadLine();
        }
    }
}