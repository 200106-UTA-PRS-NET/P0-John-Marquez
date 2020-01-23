using PizzaBox.Domain;
using PizzaBox.Domain.Models;
using PizzaBox.Storing;
using PizzaBox.Storing.Repository;
using System;
/*
using System.Collections.Generic;
using System.Text;
*/

namespace PizzaBox.Client.UserInterface
{
    class EmployeeUI
    {
        //Employee Login User Interface
        //prompts employee user to login
        //checks if username and password entered is present in the database
        public static void empLoginUI()
        {
            while (true)
            {
                Console.Clear();

                string Id;
                string passWord;
                Console.WriteLine("Store Login");
                Console.Write("Username: ");
                Id = Console.ReadLine();
                Console.Write("Password: ");
                passWord = Console.ReadLine();

                Entity db = AccessDb.acc();   //Singleton used to access data
                var employee = Repository.GetEmployees(db); 

                foreach (var emp in employee) // run though employees table and will check if user is in program
                {
                    if (emp.Uname == Id && emp.Pword == passWord) // stores user information for future reference
                    {
                        Pemployee.Id = emp.Id;
                        Pemployee.password = emp.Pword;
                        Pemployee.firstname = emp.Fname;
                        Pemployee.lastname = emp.Lname;
                        storelocationUI(); // moves to the location UI
                    }

                }


                Console.WriteLine("Invalid Username or Password");
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
                        empLoginUI();
                        break;
                }

            }
        }

        //Store Location User Interface
        //Allows employee to view order histoy of every location 
        //Allows employee to view total order history of all locations
        public static void storelocationUI()
        {

            while (true)
            {
                Console.Clear();
                string location;
                int locat;
                int counter = 0;
                Console.WriteLine("Hello Employee {0}", Pemployee.firstname);
                Console.WriteLine();

                Entity db = AccessDb.acc();   //Singleton used to access data

                var locations = Repository.GetLocations(db);
                foreach (var loc in locations)
                {
                    Console.WriteLine($"{loc.Id}.  {loc.Locat}");
                    counter++; // counter used to see how many locations are in the system
                }
                Console.WriteLine();
                Console.WriteLine("a.  All Locations");
                Console.WriteLine("s.  Sign Out");
                Console.WriteLine();
                Console.Write("Please Select a Location to view order history: ");
                location = Console.ReadLine();
                Console.WriteLine();

                int value;
                if (int.TryParse(location, out value)) // location id and address is stored for record purposes
                {
                    locat = Convert.ToInt32(location);
                    if (locat <= counter && locat > 0)
                    {
                        foreach (var loc in locations)
                        {
                            if (loc.Id == locat) // stores location for future reference
                            {
                                Location.Id = loc.Id;
                                Location.Locat = loc.Locat;
                            }

                        }
                        singlestoreUI(); // displays location store order history
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid Input");
                        Console.WriteLine("Press 'b' to return to Main Menu or any other key to try again");
                        ConsoleKeyInfo key;
                        key = Console.ReadKey();
                        Console.WriteLine();
                        switch (key.Key)
                        {
                            case ConsoleKey.B:
                                MainUI.startupUI();
                                break;
                            default:

                                break;
                        }
                    }
                }
                else if (location == "a") // displays total order history
                {
                    totstoreUI();
                }
                else if (location == "s")
                {
                    MainUI.startupUI();
                }
                else
                {
                    Console.WriteLine("Invalid Input");
                    Console.WriteLine("Press 'b' to return to Main Menu or any other key to try again");
                    ConsoleKeyInfo key;
                    key = Console.ReadKey();
                    Console.WriteLine();
                    switch (key.Key)
                    {
                        case ConsoleKey.B:
                            MainUI.startupUI();
                            break;
                        default:
                            break;
                    }
                }

            }
        }

        //Single Store Order History User Interface
        //Displays userId, total cost, data/time, amount of pizzas, type of pizza, size, and crust for each order
        public static void singlestoreUI()
        {
            Console.WriteLine($"Store ID: {Location.Id}");
            Console.WriteLine($"Store Address: {Location.Locat}");
            Console.WriteLine();
            Console.WriteLine("UserId   Total        Date/Time        Amount of Pizzas   Type of Pizza   Size    Crust");
            Console.WriteLine("-----------------------------------------------------------------------------------------");
            Entity db = AccessDb.acc();   //Singleton used to access data
            var records = Repository.GetRecordsReverse(db);
            decimal monthTotal = 0;
            foreach (var rec in records)
            {
                if (rec.LocatId == Location.Id) // runs through records table and displays only order information from that location
                {
                    decimal? value = rec.Total;
                    decimal total = value ?? 0;
                    total = Math.Round(total, 2);

                    Console.WriteLine(String.Format("{0,4} {1,9:C}  {2,22}  {3,7}             {4,-12}  {5,-7}  {6,-10}"
                        , rec.UserId, total, rec.DateT, rec.AmountP, rec.PizzaType, rec.Size, rec.Crust));

                    monthTotal = total + monthTotal;
                }

            }
            Console.WriteLine();
            Console.WriteLine($"Total Month Sales: ${monthTotal}");
            Console.WriteLine();
            Console.WriteLine("Press any key to return to store locations");
            Console.ReadKey();
            storelocationUI(); // returns to locations options

        }

        //Total Stores Order History User Interface
        //Displays storeId, userId, total cost, data/time, amount of pizzas, type of pizza, size, and crust for each order
        public static void totstoreUI()
        {
            Console.WriteLine();
            Console.WriteLine("StoreId UserId   Total        Date/Time        Amount of Pizzas   Type of Pizza   Size    Crust");
            Console.WriteLine("------------------------------------------------------------------------------------------------");
            Entity db = AccessDb.acc();   //Singleton used to access data
            var records = Repository.GetRecordsReverse(db);

            foreach (var rec in records) // runs throgh records and prints total order history
            {
                decimal? value = rec.Total;
                decimal total = value ?? 0;
                total = Math.Round(total, 2);

                Console.WriteLine(String.Format("{0,4} {1,6}  {2,9:C}  {3,22}  {4,7}             {5,-12}  {6,-7}  {7,-10}"
                    , rec.LocatId, rec.UserId, total, rec.DateT, rec.AmountP, rec.PizzaType, rec.Size, rec.Crust));
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to return to store locations");
            Console.ReadKey();
            storelocationUI();
        }
    }
}
