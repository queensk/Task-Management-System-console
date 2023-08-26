using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task_Console.Views
{
    public class UserView
    {
        public static async Task LoginUser()
        {
            Console.WriteLine("Enter your username:");
            string username = await AppView.GetUserInput();
            Console.WriteLine("Enter your password:");
            string password = await AppView.GetUserInput();
            
        }
    }
}