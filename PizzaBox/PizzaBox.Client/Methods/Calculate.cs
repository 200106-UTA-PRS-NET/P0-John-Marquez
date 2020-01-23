using PizzaBox.Domain.Models;
using PizzaBox.Storing;
using PizzaBox.Storing.Repository;
using System;

namespace PizzaBox.Client
{
    public class Calculate //calculates the cost of each seperate order. pizza, size, and amount
    {
        double tax = .0825;
        double taxTotal = 0;
        double total = 0;
        double pizzaPrice = 0;


        public double calculateCostPreset(int amount, string pizzaSize, int pizzaId) // amount of pizzas, size of crust, and size of pizza(s)
        {

            Entity db = AccessDb.acc();

            var pizzas = Repository.GetPizza(db);
            foreach (var pie in pizzas) // runs through pizza table finds the pizza of choice and checks the menu in the database for standard price
            {
                if (pie.Id == pizzaId)
                {
                    switch (pizzaSize)
                    {
                        case "small":
                            decimal? value = pie.Small;
                            decimal value2 = value ?? 0;
                            pizzaPrice = Convert.ToDouble(value2);
                            break;
                        case "medium":
                            decimal? value3 = pie.Med;
                            decimal value4 = value3 ?? 0;
                            pizzaPrice = Convert.ToDouble(value4);
                            break;
                        case "large":
                            decimal? value5 = pie.Large;
                            decimal value6 = value5 ?? 0;
                            pizzaPrice = Convert.ToDouble(value6);
                            break;
                        default:
                            Console.WriteLine("Shouldn't Happen");
                            break;
                    }
                }

            }

            // Calculating total with tax

            total = (total + (pizzaPrice * amount));
            taxTotal = (total * tax);
            total = taxTotal + total;

            total = Math.Round((Double)total, 2);


            return total;
        }

        public double calculateCostCustom(int amount, string pizzaSize, string toppings, int pizzaId) // amount of pizzas, size of crust, and size of pizza(s)
        {

            int toppn = 0; // check how many toppings are in the string

            foreach (char c in toppings)
            {
                if (c == ',') toppn++;
            }


            int totalToppings = toppn * 1;

            Entity db = AccessDb.acc();

            var pizzas = Repository.GetPizza(db);
            foreach (var pie in pizzas) // finds the pizza of choice and checks the menu in the database for standard price
            {
                if (pie.Id == pizzaId)
                {
                    switch (pizzaSize)
                    {
                        case "small":
                            decimal? value = pie.Small;
                            decimal value2 = value ?? 0;
                            pizzaPrice = Convert.ToDouble(value2);
                            break;
                        case "medium":
                            decimal? value3 = pie.Med;
                            decimal value4 = value3 ?? 0;
                            pizzaPrice = Convert.ToDouble(value4);
                            break;
                        case "large":
                            decimal? value5 = pie.Large;
                            decimal value6 = value5 ?? 0;
                            pizzaPrice = Convert.ToDouble(value6);
                            break;
                        default:
                            Console.WriteLine("Shouldn't Happen");
                            break;
                    }
                }
            }

            // Calculating total with tax

            total = (total + (pizzaPrice * amount)) + totalToppings;
            taxTotal = (total * tax);
            total = taxTotal + total;

            total = Math.Round((Double)total, 2);

            return total;
        }

    }
}

