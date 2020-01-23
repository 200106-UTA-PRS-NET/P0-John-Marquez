using PizzaBox.Domain;
using PizzaBox.Domain.Models;
using PizzaBox.Storing;
using PizzaBox.Storing.Repository;
using System;

namespace PizzaBox.Client
{
    static public class DateTimeCheck
    {
        
        static public bool checkDT() //checks if last order meets time and date conditions for current order
        {
            DateTime now = DateTime.Now;

            Entity db = AccessDb.acc();
            
            var records = Repository.GetRecordsReverse(db);
            foreach (var rec in records)  // 
            {
                if(rec.UserId == PCustomer.Id && rec.LocatId == Location.Id)
                { 
                    TimeSpan interval = now - rec.DateT; // Timespan struct to calculate the interval between the two dates.                                                         

                    if (interval.TotalDays < 1) // If totaldays less than 1, does not allow purchace
                    {
                        Console.WriteLine("Must wait 24 hours before next purchase at this location");
                        Console.WriteLine($"Last order here on {rec.DateT}");
                        return false;
                    }
                    
                }
                else if (rec.UserId == PCustomer.Id)
                {
                    TimeSpan interval = now - rec.DateT; // Timespan struct to calculate the interval between the two dates.                                                         

                    if (interval.TotalHours < 2) // If totalhours less than 2, does not allow purchace
                    {
                        Console.WriteLine("Must wait 2 hours before next purchase in general");
                        Console.WriteLine($"Last order on {rec.DateT}");
                        return false;
                    }
                }


            }            
            return true;
        }
    }
}
