using PizzaBox.Domain.Models;
using PizzaBox.Storing;
using PizzaBox.Storing.Repository;
using System;

namespace PizzaBox.Client
{
    public class TypeChange
    {
        public string returnPizza(string pizType) //converts user input to pizzaType
        {

            int pizza = Convert.ToInt32(pizType);
          
            Entity db = AccessDb.acc();
            
            var pizzas = Repository.GetPizza(db);

            foreach (var pie in pizzas)
            {
                if (pie.Id == pizza)
                {
                    pizType = pie.PizzaType;
                }

            }
            

            return pizType;
        }

        public string returnCrust(string crustType) //converts user input letter crust to crustType
        {
            switch (crustType)
            {
                case "t":
                    crustType = "thin";
                    break;
                case "h":
                    crustType = "handtossed";
                    break;
                case "p":
                    crustType = "pan";
                    break;
                default:

                    break;
            }

            return crustType;
        }

        public string returnSize(string sizeType) //converts user input letter size to sizeType
        {
            switch (sizeType)
            {
                case "s":
                    sizeType = "small";
                    break;
                case "m":
                    sizeType = "medium";
                    break;
                case "l":
                    sizeType = "large";
                    break;
                default:

                    break;
            }

            return sizeType;
        }

        public string returnLocation(int? locationID) // converts location ID to location Address for user history view
        {
            string locAddress = "Location Unknown";
            Entity db = AccessDb.acc();
            var locations = Repository.GetLocations(db);

            foreach (var loc in locations)
            { 
                if (loc.Id == locationID)
                {
                    locAddress = loc.Locat;
                }

            }

            return locAddress;
        }
        
        public string returnToppings(int[] toppings) // converts array of toppings to string of toppings
        {
            string topTotal = "with ";
            for (int i = 0; i < 5; i++)
            {
                switch (toppings[i])
                {
                    case 1:
                        topTotal = topTotal + ",pepperoni";
                        break;
                    case 2:
                        topTotal = topTotal + ",chicken";
                        break;
                    case 3:
                        topTotal = topTotal + ",sausage";
                        break;
                    case 4:
                        topTotal = topTotal + ",bacon";
                        break;
                    case 5:
                        topTotal = topTotal + ",mushroom";
                        break;
                    case 6:
                        topTotal = topTotal + ",jalapeno";
                        break;
                    case 7:
                        topTotal = topTotal + ",spinach";
                        break;
                    case 8:
                        topTotal = topTotal + ",pineapple";
                        break;
                    case 0:
                        i = 5;
                        break;
                    default:
                        Console.WriteLine("Default case");
                        break;
                }
            }
            return topTotal;
        }
        


    }
}
