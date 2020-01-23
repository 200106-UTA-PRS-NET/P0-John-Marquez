using PizzaBox.Domain.Models;
using PizzaBox.Storing;
using PizzaBox.Storing.Repository;
using System;
using System.Threading;

namespace PizzaBox.Client.UserInterface
{
    class RegisterUI
    {
        // Register User Interface
        // Promts user to enter firstname, lastname, username, and password
        // Prevents user from creating an account with repeated username
        public static void registerUI()
        {
            while (true)
            {
                Console.Clear();
                string newId;
                string newPassword;
                string nameFirst;
                string nameLast;

                Console.WriteLine("Create an Account");
                Console.WriteLine();
                Console.Write("First Name: ");
                nameFirst = Console.ReadLine();
                Console.Write("Last Name: ");
                nameLast = Console.ReadLine();
                Console.Write("Username: ");
                newId = Console.ReadLine();
                Console.Write("Password: ");
                newPassword = Console.ReadLine();

                Entity db = AccessDb.acc();   //Singleton used to access data
                var customers = Repository.GetCustomer(db);
                foreach (var cus in customers)
                {
                    if (cus.Uname == newId) // checks if entered username is already stored in database
                    {
                        Console.WriteLine($"Invalid username. There is already an account with the username '{newId}'");
                        Console.WriteLine("Press 'b' to return to last page or any other key to try again");
                        ConsoleKeyInfo key;
                        key = Console.ReadKey();
                        Console.WriteLine();
                        switch (key.Key)
                        {
                            case ConsoleKey.B:
                                MainUI.startupUI();
                                break;
                            default:
                                registerUI();
                                Thread.Sleep(2000);
                                break;
                        }
                    }

                }
                Console.Write("Are you sure you want to create an account with this information? y/n: ");
                string confirm;
                confirm = Console.ReadLine();
                if (confirm == "y")
                {
                    Customer customer = new Customer() { Fname = nameFirst, Lname = nameLast, Uname = newId, Pword = newPassword };
                    Repository.AddCustomer(db, customer); // adds customer to customer table

                    Console.WriteLine("Success! Your account has been created");
                    Thread.Sleep(2000);
                    MainUI.startupUI();
                }
                else if (confirm == "n")
                {
                    Console.WriteLine("Press 'b' to return to last page or any other key to try again");
                    ConsoleKeyInfo key1;
                    key1 = Console.ReadKey();
                    Console.WriteLine();
                    switch (key1.Key)
                    {
                        case ConsoleKey.B:
                            MainUI.startupUI();
                            break;
                        default:
                            registerUI();
                            break;
                    }

                }
                else
                {
                    Console.WriteLine("Incorrect Input");
                    Console.WriteLine("Press 'b' to return to last page or any other key to try again");
                    ConsoleKeyInfo key1;
                    key1 = Console.ReadKey();
                    Console.WriteLine();
                    switch (key1.Key)
                    {
                        case ConsoleKey.B:
                            MainUI.startupUI();
                            break;
                        default:
                            registerUI();
                            break;
                    }
                }
            }
        }
    }
}
