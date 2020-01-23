using System;

namespace PizzaBox.Client.UserInterface
{
    static public class MainUI
    {
        // Main User Interface
        // Allows user to login as customer
        // Allows user to login as employee
        // Allows user to create a customer account
        static public void startupUI()
        {
            while (true)
            {
                Console.Clear();
                string read;
                Console.WriteLine("Main Menu");
                Console.WriteLine();
                Console.WriteLine("c: \tCustomer Login");
                Console.WriteLine("e: \tEmployee Login");
                Console.WriteLine("r: \tCreate Customer Account");
                Console.WriteLine("q: \tQuit Program");
                read = Console.ReadLine();

                switch (read)
                {
                    case "c":
                        UserUI.loginUI();
                        break;
                    case "e":
                        EmployeeUI.empLoginUI();
                        break;
                    case "r":
                        RegisterUI.registerUI();
                        break;
                    case "q":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid Choice");
                        Console.WriteLine("Press any key to try again");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
