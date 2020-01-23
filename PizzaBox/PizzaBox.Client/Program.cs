/*
using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PizzaBox.Domain;
using PizzaBox.Domain.Models;
using PizzaBox.Storing;
using System.IO;
using Microsoft.Extensions.Configuration.Json;
using System.Linq;
using PizzaBox.Storing.Repository;
*/
using PizzaBox.Client.UserInterface;

namespace PizzaBox.Client
{

    class Program
    {

        static void Main(string[] args)
        {

            MainUI.startupUI();

        }








        /*startup
        public static void startupUI()
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
                        loginUI();
                        break;
                    case "e":
                        empLoginUI();
                        break;
                    case "r":
                        registerUI();
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
        */

        /*login
        public static void loginUI()
        {
            while (true)
            {
                Console.Clear();

                string userName;
                string passWord;
                Console.WriteLine("Welcome to Marquez's Pizzaria");
                Console.Write("Username: ");
                userName = Console.ReadLine();
                Console.Write("Password: ");
                passWord = Console.ReadLine();

                Entity db = AccessDb.acc();   //Singleton used to access data

                var customers = Repository.GetCustomer(db);
                foreach (var cus in customers)
                {
                    if (cus.Uname == userName && cus.Pword == passWord)
                    {
                        PCustomer.Id = cus.Id;
                        PCustomer.password = cus.Pword;
                        PCustomer.firstname = cus.Fname;
                        PCustomer.lastname = cus.Lname;

                        locationUI(); // move to location interface
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
                        startupUI();
                        break;
                    default:
                        loginUI();
                        break;
                }

            }
        }
        */

        /* location    
        public static void locationUI()
        {
            while (true)
            {
                Console.Clear();
                string location;
                int locat;
                int counter = 0;
                Console.WriteLine("Hello {0}", PCustomer.firstname);
                Console.WriteLine();

                Entity db = AccessDb.acc();   //Singleton used to access data

                var locations = Repository.GetLocations(db);
                foreach (var loc in locations)
                {
                    Console.WriteLine($"{loc.Id}.  {loc.Locat}");
                    counter++; // counter used to see how many locations are in the system
                }
                Console.WriteLine();
                Console.Write("Please Select a Location: ");
                location = Console.ReadLine();
                int value;
                if (int.TryParse(location, out value)) // location id and address is stored for record purposes
                {
                    locat = Convert.ToInt32(location);
                    if (locat <= counter && locat > 0)
                    {
                        foreach (var loc in locations)
                        {
                            if (loc.Id == locat)
                            {
                                Location.Id = loc.Id;
                                Location.Locat = loc.Locat;
                            }

                        }
                        optionsUI();
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
                                startupUI();
                                break;
                            default:

                                break;
                        }
                    }
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
                            startupUI();
                            break;
                        default:
                            break;
                    }
                }

            }
        }
        */

        /* options
        public static void optionsUI()
        {
            while (true)
            {
                Console.Clear();
                string select;

                Console.WriteLine("1. Place an Order");
                Console.WriteLine("2. View Order History");
                Console.WriteLine("3. Select Another Location");
                Console.WriteLine("4. Sign Out");
                Console.Write("Please select an option: ");

                select = (Console.ReadLine());

                switch (select)
                {
                    case "1":
                        if (DateTimeCheck.checkDT())
                        {
                            orderUI();
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("Press any key to return to other options");
                            Console.ReadKey();
                            //optionsUI();
                        }
                        break;
                    case "2":
                        historyUI();
                        break;
                    case "3":
                        locationUI();
                        break;
                    case "4":
                        startupUI();
                        break;
                    default:
                        Console.WriteLine("Invalid Choice");
                        Console.WriteLine("Press any key to try again");
                        Console.ReadKey();
                        //Thread.Sleep(2000);
                        break;
                }
            }
        }
        */

        /*order
        public static void orderUI()
        {
            bool flag = false;
            while (true)
            {
                Console.Clear();
                string pizza;
                int pizzaAmount;
                Entity db = AccessDb.acc();

                var pizzas = Repository.GetPizza(db);
                Console.WriteLine("Large    Medium    Small    Pizzas");
                Console.WriteLine("----------------------------------");
                foreach (var pie in pizzas)
                {
                    double small = Math.Round((Double)pie.Small, 2);
                    double medium = Math.Round((Double)pie.Med, 2);
                    double large = Math.Round((Double)pie.Large, 2);
                    if (pie.PizzaType == "Custom")
                    {
                        Console.WriteLine($"${large}    ${medium}     ${small}    {pie.PizzaType}");
                    }
                    else
                    {
                        Console.WriteLine($"${large}   ${medium}     ${small}    {pie.PizzaType}");
                    }

                }



                Console.WriteLine();
                Console.WriteLine("1. Hawaiian                       o. Options");
                Console.WriteLine("2. Meat Lovers                    c. Clear Order");
                Console.WriteLine("3. Supreme");
                Console.WriteLine("4. Custom");
                Console.WriteLine();
                Console.WriteLine();
                if (flag == true)
                {
                    double preTotal = 0;
                    Console.WriteLine("Current Order:");
                    foreach (var piz in PizzaLt.PizzaList)
                    {
                        Console.WriteLine($" {piz.Amount} {piz.PizzaType} {piz.Size} {piz.Crust} crust pizza(s)");
                        preTotal = piz.Total + preTotal;
                        
                    }
                    Console.WriteLine($"Current Total ${preTotal}");
                }
                Console.WriteLine();
                Console.Write("Please select a type of Pizza, clear order, or return to user option: ");
                pizza = Console.ReadLine();


                if (pizza == "1" || pizza == "2" || pizza == "3" || pizza == "4")
                {
                    Console.Write("How many pizzas would you like? ");
                    string pizzaAm;

                    pizzaAm = Console.ReadLine();

                    int value;
                    if (int.TryParse(pizzaAm, out value))
                    {

                        pizzaAmount = Convert.ToInt32(pizzaAm);

                        string crust;
                        string size;
                        Console.WriteLine();
                        Console.WriteLine("Select Crust and Size");
                        Console.Write("t for thin, h for handtossed, p for pan: ");
                        crust = (Console.ReadLine());

                        if (crust == "t" || crust == "h" || crust == "p")
                        {
                            Console.Write("s for small, m for medium, and l for large: ");
                            size = Console.ReadLine();
                            if (size == "s" || size == "m" || size == "l")
                            {
                                int pizzaId = Convert.ToInt32(pizza); // converting for calculation

                                TypeChange tc = new TypeChange(); // converting user input to string equivalents
                                pizza = tc.returnPizza(pizza);
                                crust = tc.returnCrust(crust);
                                size = tc.returnSize(size);

                                Console.WriteLine();
                                Calculate cal = new Calculate();
                                double total = cal.calculateCostPreset(pizzaAmount, size, pizzaId); //send to a pizza cost method
                                double currentTotal = total;
                                if (total > 250)
                                {
                                    Console.WriteLine("Cannot spend more than 250!");
                                    Console.WriteLine("Press any key to restart your order");
                                    Console.ReadKey();
                                    PizzaLt.PizzaList.Clear();
                                    orderUI();
                                }

                                if (flag == true)
                                {
                                    foreach (var piz in PizzaLt.PizzaList)
                                    {
                                        Console.WriteLine($"{piz.Amount} {piz.PizzaType} {piz.Size} {piz.Crust} crust pizza(s)");
                                        total = total + piz.Total;
                                        if (total > 250)
                                        {
                                            Console.WriteLine("Cannot spend more than 250!");
                                            Console.WriteLine("Press any key to restart your order");
                                            Console.ReadKey();
                                            PizzaLt.PizzaList.Clear();
                                            orderUI();

                                        }
                                    }
                                }
                                total = Math.Round(total, 2);
                                Console.WriteLine($"{pizzaAmount} {pizza} {size} {crust} crust pizza(s)");
                                Console.WriteLine($"Your total is ${total}");
                                string confirm;
                                //Console.WriteLine("Will you like to add to your order? y/n: ")
                                Console.WriteLine("y. Confirm and purchase order");
                                Console.WriteLine("a. Add to your current order");
                                Console.WriteLine();
                                Console.Write("Or enter anything else to clear and restart order: ");
                                confirm = Console.ReadLine();
                                if (confirm == "y")
                                {

                                    PizzaLt.PizzaList.Add(new Pizzas(PCustomer.Id, currentTotal, pizzaAmount, pizza, size, crust));
                                    decimal tot;
                                    foreach (var piz in PizzaLt.PizzaList) // goes through the list of objects and stores them to the records table
                                    {
                                        tot = Convert.ToDecimal(piz.Total);
                                        tot = Math.Round(tot, 2);
                                        DateTime dateTime = DateTime.Now;
                                        Records records = new Records()
                                        {
                                            UserId = piz.UserID,
                                            Total = tot,
                                            DateT = dateTime,
                                            AmountP = piz.Amount,
                                            PizzaType = piz.PizzaType,
                                            Size = piz.Size,
                                            Crust = piz.Crust,
                                            LocatId = Location.Id
                                        };

                                        Repository.AddRecords(db, records);
                                    }
                                    Console.WriteLine("Thank You for your purchase");
                                    Thread.Sleep(2000);
                                    optionsUI();


                                }
                                else if (confirm == "a")
                                {
                                    flag = true; // will show total order for user by running through the List of orders

                                    PizzaLt.PizzaList.Add(new Pizzas(PCustomer.Id, currentTotal, pizzaAmount, pizza, size, crust)); // stores the current order to a generic list

                                    Console.WriteLine("Please Press any key to continue your order");
                                    Console.ReadKey();
                                }
                                else
                                {
                                    PizzaLt.PizzaList.Clear();
                                    Console.WriteLine("Please Press any key to restart your order");
                                    Console.ReadKey();

                                }

                            }
                            else
                            {
                                Console.WriteLine("Invalid Choice");
                                Console.WriteLine("Please Press any key to continue your order");
                                Console.ReadKey();
                            }
                        }
                        else
                        {

                            Console.WriteLine("Invalid Choice");
                            Console.WriteLine("Please Press any key to continue your order");
                            Console.ReadKey();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid Choice");
                        Console.WriteLine("Please Press any key to continue your order");
                        Console.ReadKey();
                    }
                }
                else if (pizza == "o")
                {
                    PizzaLt.PizzaList.Clear();
                    optionsUI();
                }
                else if (pizza == "c")
                {
                    PizzaLt.PizzaList.Clear();
                }
                else
                {
                    Console.WriteLine("Invalid Choice");
                    Console.WriteLine("Please Press any key to continue your order");
                    Console.ReadKey();
                }

            }
        }
        */

        /*history    
        public static void historyUI()
        {
            while (true)
            {
                string address;
                Console.Clear();
                Console.WriteLine("Order History");
                Console.WriteLine();
                Console.WriteLine(" Total        Date/Time        Amount of Pizzas   Type of Pizza   Size    Crust                Location");
                Console.WriteLine("-----------------------------------------------------------------------------------------------------------------");
                Entity db = AccessDb.acc();   //Singleton used to access data
                var records = Repository.GetRecordsReverse(db);

                foreach (var rec in records)
                {
                    if (rec.UserId == PCustomer.Id)
                    {
                        TypeChange type = new TypeChange();
                        address = type.returnLocation(rec.LocatId);
                        decimal? value = rec.Total;
                        decimal total = value ?? 0;
                        total = Math.Round(total, 2);
                        //Console.WriteLine($"${total}   {rec.DateT}         {rec.AmountP}             {rec.PizzaType}     {rec.Size}    {rec.Crust}");
                        Console.WriteLine(String.Format("{0,5:C}  {1,22}  {2,7}            {3,-12}  {4,-6}  {5,-11}  {6,0}"
                            , total, rec.DateT, rec.AmountP, rec.PizzaType, rec.Size, rec.Crust, address));

                    }

                }

                Console.WriteLine();
                Console.WriteLine("Press any key to return to other options");
                Console.ReadKey();
                optionsUI();
                break;

            }
        }
        */

        /* custom
        public static string customUI()
        {
            Console.WriteLine();
            Console.WriteLine("Select your toppings, 5 toppings max ($1.00 each)");
            Console.WriteLine();
            Console.WriteLine("1. pepperoni            5. mushroom              c. complete choosing toppings");
            Console.WriteLine("2. chicken              6. jalapenos");
            Console.WriteLine("3. sausage              7. spinach");
            Console.WriteLine("4. bacon                8. pineapple");
            int[] toppings = new int[] { 0, 0, 0, 0, 0 };
            int i = 0;
            do
            {
                string topp = Console.ReadLine();
                int topping;
                if (int.TryParse(topp, out topping))
                {
                    topping = Convert.ToInt32(topp);
                    if (topping > 0 && topping < 9)
                    {
                        toppings[i] = topping;
                        i++;
                    }
                    else
                    {
                        Console.WriteLine("Invalid Input");
                    }
                }
                else if (topp == "c")
                {
                    i = 5;
                }
                else
                {
                    Console.WriteLine("Invalid Input");

                }
            } while (i < 5);

            TypeChange type = new TypeChange();
            //string fullToppings = type.returnToppings(toppings);
            return type.returnToppings(toppings);

        }
        */

        /*empLogin
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

                foreach (var emp in employee)
                {
                    if (emp.Uname == Id && emp.Pword == passWord)
                    {
                        Pemployee.Id = emp.Id;
                        Pemployee.password = emp.Pword;
                        Pemployee.firstname = emp.Fname;
                        Pemployee.lastname = emp.Lname;
                        storelocationUI();
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
                        startupUI();
                        break;
                    default:
                        empLoginUI();
                        break;
                }


            }

        }
        */

        /*register
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
                    if (cus.Uname == newId)
                    {
                        Console.WriteLine($"Invalid username. There is already an account with the username '{newId}'");
                        Console.WriteLine("Press 'b' to return to last page or any other key to try again");
                        ConsoleKeyInfo key;
                        key = Console.ReadKey();
                        Console.WriteLine();
                        switch (key.Key)
                        {
                            case ConsoleKey.B:
                                startupUI();
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
                    Repository.AddCustomer(db, customer);

                    Console.WriteLine("Success! Your account has been created");
                    Thread.Sleep(2000);
                    startupUI();
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
                            startupUI();
                            break;
                        default:
                            registerUI();
                            //Thread.Sleep(2000);
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
                            startupUI();
                            break;
                        default:
                            registerUI();
                            //Thread.Sleep(2000);
                            break;
                    }
                }
            }
        }
        */

        /*storeLocation
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
                int value;
                if (int.TryParse(location, out value)) // location id and address is stored for record purposes
                {
                    locat = Convert.ToInt32(location);
                    if (locat <= counter && locat > 0)
                    {
                        foreach (var loc in locations)
                        {
                            if (loc.Id == locat)
                            {
                                Location.Id = loc.Id;
                                Location.Locat = loc.Locat;
                            }

                        }
                        singlestoreUI();
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
                                startupUI();
                                break;
                            default:

                                break;
                        }
                    }
                }
                else if(location == "a")
                {
                    totstoreUI();
                }
                else if(location == "s")
                {
                    startupUI();
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
                            startupUI();
                            break;
                        default:
                            break;
                    }
                }

            }
        }
        */

        /*singleStore
        public static void singlestoreUI()
        {
            Console.WriteLine($"Store ID: {Location.Id}");
            Console.WriteLine($"Store Address: {Location.Locat}");
            Console.WriteLine();
            Console.WriteLine("UserId   Total        Date/Time        Amount of Pizzas   Type of Pizza   Size    Crust");
            Console.WriteLine("-----------------------------------------------------------------------------------------");
            Entity db = AccessDb.acc();   //Singleton used to access data
            var records = Repository.GetRecordsReverse(db);

            foreach (var rec in records)
            {
                if (rec.LocatId == Location.Id)
                {
                    decimal? value = rec.Total;
                    decimal total = value ?? 0;
                    total = Math.Round(total, 2);

                    //Console.WriteLine(String.Format("{0,4} {1,6}  {2,9:C}  {3,22}  {4,7}             {5,-12}  {6,-7}  {7,-10}"
                     //   , rec.LocatId, rec.UserId, total, rec.DateT, rec.AmountP, rec.PizzaType, rec.Size, rec.Crust));

                    Console.WriteLine(String.Format("{0,4} {1,9:C}  {2,22}  {3,7}             {4,-12}  {5,-7}  {6,-10}"
                        , rec.UserId, total, rec.DateT, rec.AmountP, rec.PizzaType, rec.Size, rec.Crust));
                }

            }

            Console.WriteLine();
            Console.WriteLine("Press any key to return to store locations");
            Console.ReadKey();
            storelocationUI();

        }
        */

        /*totalStores
        public static void totstoreUI()
        {
            Console.WriteLine();
            Console.WriteLine("StoreId UserId   Total        Date/Time        Amount of Pizzas   Type of Pizza   Size    Crust");
            Console.WriteLine("------------------------------------------------------------------------------------------------");
            Entity db = AccessDb.acc();   //Singleton used to access data
            var records = Repository.GetRecordsReverse(db);

            foreach (var rec in records)
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
        */

    }
}
