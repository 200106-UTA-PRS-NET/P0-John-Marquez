using System;
using System.Threading;

namespace PizzaBox.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();

                string userName;
                string password;
                Console.WriteLine("Welcome to Marquez's Pizzaria");
                Console.Write("Username: ");
                userName = Console.ReadLine();
                Console.Write("Password: ");
                password = Console.ReadLine();

                if (userName == "john" && password == "password")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid Username or Password");
                    Thread.Sleep(2000);

                }

                

            }

            while(true)
            {
                Console.Clear();
                int location;
                Console.WriteLine("Hello");
                Console.WriteLine("1. Revature");
                Console.WriteLine("2. Liv+");
                Console.WriteLine("3. UTA");
                Console.Write("Please Select a Location: ");
                location = Convert.ToInt32(Console.ReadLine());

                if (location < 3 && location > 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid Choice");
                    Thread.Sleep(2000);
                }

            }

            while(true)
            {
                Console.Clear();
                int select;
                Console.WriteLine("Hello");
                Console.WriteLine("1. Place an Order");
                Console.WriteLine("2. View Order History");
                Console.WriteLine("3. Sign Out");
                Console.Write("Please select an option: ");
                select = Convert.ToInt32(Console.ReadLine());

                if (select < 3 && select > 0)
                {
                    //break;
                    if(select == 1)
                    {
                        int pizza;
                        Console.WriteLine("Hello");
                        Console.WriteLine("1. Meat Lovers");
                        Console.WriteLine("2. Cheese");
                        Console.WriteLine("3. Pepperoni");
                        Console.Write("Please Select a Pizza: ");
                        pizza = Convert.ToInt32(Console.ReadLine());

                        if (pizza < 3 && pizza > 0)
                        {
                            Console.Clear();
                            Console.WriteLine("Select Crust and Size");
                            Console.WriteLine("");
                          
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Choice");
                    Thread.Sleep(2000);
                }

            }


        }
    }
}
