using PizzaBox.Domain;
using PizzaBox.Domain.Models;
using PizzaBox.Storing;
using PizzaBox.Storing.Repository;
using System;
using System.Threading;

namespace PizzaBox.Client.UserInterface
{
    public static class UserUI
    {
        //Login User Interface
        //prompts user to login
        //checks if username and password entered is present in the database
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

                var customers = Repository.GetCustomer(db);  //variable to store all customer table information
                foreach (var cus in customers) // checks if username and password are present in the database
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
                        MainUI.startupUI();
                        break;
                    default:
                        loginUI();
                        break;
                }

            }
        } 

        //Location User Interface
        //promps the user to choose a location for order
        //location information is stored for future references
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

                var locations = Repository.GetLocations(db); //gets all locations from the location table
                foreach (var loc in locations)
                {
                    Console.WriteLine($"{loc.Id}.  {loc.Locat}");
                    counter++; // counter used to see how many locations are in the system, can add more locations if needed
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
                                Location.Id = loc.Id; // stores location information
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
                                MainUI.startupUI(); //return to Main Menu
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
                            MainUI.startupUI();
                            break;
                        default:
                            break;
                    }
                }

            }
        }
        
        //Options User Interface
        //Allows user to Place an order at set location
        //Allows user to view its full order history
        //Allows user to select another location
        //Allows user to sign out
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
                        if (DateTimeCheck.checkDT()) // will check if user has odered from this location in the last 24 hours
                                                     // or last 2 hours in any location
                        {
                            orderUI();
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("Press any key to return to other options");
                            Console.ReadKey();
                        }
                        break;
                    case "2":
                        historyUI(); // send to order history user interface
                        break;
                    case "3":
                        locationUI(); // send to reselect a location user interface
                        break;
                    case "4":
                        MainUI.startupUI(); // send to Main Menu
                        break;
                    default:
                        Console.WriteLine("Invalid Choice");
                        Console.WriteLine("Press any key to try again");
                        Console.ReadKey();
                        break;
                }
            }
        }

        //Order User Interface
        //Allows user to order both preset and custom pizzas
        //Allows multiple orders before comfirmation
        //Allows resetting of order for convenience
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
                Console.WriteLine("Large    Medium    Small    Pizzas"); // prints all pizzas and menus prices on console for user
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
                    foreach (var piz in PizzaLt.PizzaList) //prints current order of user
                    {
                        Console.WriteLine($" {piz.Amount} {piz.PizzaType} {piz.Size} {piz.Crust} crust pizza(s) {piz.Topping}");
                        preTotal = piz.Total + preTotal;

                    }
                    Console.WriteLine($"Current Total ${preTotal}"); // prints current total
                }
                Console.WriteLine();
                Console.Write("Please select a type of Pizza, Clear Order, or Return to User Option: ");
                pizza = Console.ReadLine();
                Console.WriteLine();
                string toppings = "";
                if(pizza == "4")
                {
                    toppings = customUI(); // returs the toppings chosen from user
                }

                if (pizza == "1" || pizza == "2" || pizza == "3" || pizza == "4")
                {
                    Console.Write("How many pizzas would you like? ");
                    string pizzaAm;

                    pizzaAm = Console.ReadLine();

                    int value;
                    if (int.TryParse(pizzaAm, out value)) // checks if user type was an integer
                    {

                        pizzaAmount = Convert.ToInt32(pizzaAm);
                        if(pizzaAmount > 99) // if user asks for 100 pizzas or more, the program will not allow it
                        {
                            Console.WriteLine("Cannot purchase more than 99 pizzas");
                            Console.WriteLine("Please Press any key to continue your order");
                            Console.ReadKey();
                            orderUI();
                        }
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
                                int pizzaId = Convert.ToInt32(pizza); // converting user input to pizza Id

                                TypeChange tc = new TypeChange(); // converting user input to string equivalents
                                pizza = tc.returnPizza(pizza);
                                crust = tc.returnCrust(crust);
                                size = tc.returnSize(size);
                                double total;
                                Console.WriteLine();
                                if(pizzaId == 4)
                                {
                                    Calculate cal = new Calculate();
                                    total = cal.calculateCostCustom(pizzaAmount, size, toppings, pizzaId); //send to a pizza cost method custom
                                }
                                else
                                {
                                    Calculate cal = new Calculate();
                                    total = cal.calculateCostPreset(pizzaAmount, size, pizzaId); //send to a pizza cost method preset
                                }
 
                                double currentTotal = total;
                                if (total > 250) // total cost cannot be more than 250 
                                {
                                    Console.WriteLine("Cannot spend more than 250!");
                                    Console.WriteLine("Press any key to restart your order");
                                    Console.ReadKey();
                                    PizzaLt.PizzaList.Clear();
                                    orderUI();
                                }

                                if (flag == true)
                                {
                                    foreach (var piz in PizzaLt.PizzaList) // runs through the list of objects to display order on console
                                    {
                                        Console.WriteLine($"{piz.Amount} {piz.PizzaType} {piz.Size} {piz.Crust} crust pizza(s) {piz.Topping}");
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
                                Console.WriteLine($"{pizzaAmount} {pizza} {size} {crust} crust pizza(s) {toppings}"); // displays current order
                                Console.WriteLine($"Your total is ${total}");
                                string confirm;
                                Console.WriteLine();
                                Console.WriteLine("y. Confirm and purchase order");
                                Console.WriteLine("a. Add to your current order");
                                Console.WriteLine();
                                Console.Write("Or enter anything else to clear and restart order: ");
                                confirm = Console.ReadLine();
                                if (confirm == "y") // confirming order begins the process to store user order
                                {

                                    PizzaLt.PizzaList.Add(new Pizzas(PCustomer.Id, currentTotal, pizzaAmount, pizza, size, crust, toppings));
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
                                else if (confirm == "a") // adding will allow user to add another pizza to their order
                                {
                                    flag = true; // will show total order for user by running through the List of orders

                                    PizzaLt.PizzaList.Add(new Pizzas(PCustomer.Id, currentTotal, pizzaAmount, pizza, size, crust, toppings)); // stores the current order to a generic list

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
                else if (pizza == "o") // will clear the list of objects and return to user interface
                {
                    PizzaLt.PizzaList.Clear();
                    optionsUI();
                }
                else if (pizza == "c") // will clear the list of objects to reset order
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

        //Custom User Interface
        //Allows user to select up to 5 toppings for its custom pizza
        public static string customUI()
        {
            Console.WriteLine();
            Console.WriteLine("Select your toppings, 5 toppings max ($1.00 each)");
            Console.WriteLine();
            Console.WriteLine("1. pepperoni            5. mushroom              c. complete choosing toppings");
            Console.WriteLine("2. chicken              6. jalapenos");
            Console.WriteLine("3. sausage              7. spinach");
            Console.WriteLine("4. bacon                8. pineapple");
            Console.WriteLine();
            int[] toppings = new int[] { 0, 0, 0, 0, 0 };
            int i = 0;
            do // runs through loop to populate toppings array
            {
                Console.Write("Add a topping: ");
                string topp = Console.ReadLine();
                int topping;
                if (int.TryParse(topp, out topping)) 
                {
                    topping = Convert.ToInt32(topp); // each toppping has its own interger value, stored in array
                    if (topping > 0 && topping < 9)
                    {
                        //Console.WriteLine($"Added Topping  {i}");
                        toppings[i] = topping;
                        i++;
                        Console.WriteLine($"Topping Added");
                    }
                    else
                    {
                        Console.WriteLine("Invalid Input");
                    }
                }
                else if (topp == "c") // complete chosing of toppings
                {
                    i = 5;
                }
                else
                {
                    Console.WriteLine("Invalid Input");

                }
            } while (i < 5);

            Console.WriteLine();
            TypeChange type = new TypeChange();
            return type.returnToppings(toppings); // converts array to string of toppings then returned to options UI

        }

        //Order History User Interface
        //Displays total order history of user
        //total, date/time, amount of pizzas, type of pizza, size, crust, and location for each order
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
                    if (rec.UserId == PCustomer.Id) // runs through record and check if user id matches foreign key in records, then displays history
                    {
                        TypeChange type = new TypeChange();
                        address = type.returnLocation(rec.LocatId);
                        decimal? value = rec.Total;
                        decimal total = value ?? 0;
                        total = Math.Round(total, 2);
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
    }
}
