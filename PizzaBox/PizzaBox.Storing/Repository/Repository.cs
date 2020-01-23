using PizzaBox.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace PizzaBox.Storing.Repository
{
    
    public static class Repository
    {
        
        //Getting Repository
        public static IEnumerable<Employee> GetEmployees(Entity db) // creates query to gather total employee table information
        {
            var query = from e in db.Employee
                        select e;

            return query;
        }
        
        public static IEnumerable<Customer> GetCustomer(Entity db) // creates query to gather total customer table information
        {
            var query = from e in db.Customer
                        select e;

            return query;
        }

        public static IEnumerable<Pizza> GetPizza(Entity db)  // creates query to gather total pizza table information
        {
            var query = from e in db.Pizza
                        select e;

            return query;
        }

        public static IEnumerable<Records> GetRecords(Entity db)  // creates query to gather total records table information
        {
            var query = from e in db.Records
                        select e;

            return query;
        }

        public static IEnumerable<Locations> GetLocations(Entity db)  // creates query to gather total location table information
        {
            var query = from e in db.Locations
                        select e;

            return query;
        }

        public static IEnumerable<Records> GetRecordsReverse(Entity db)  // creates query to gather total records table information
        {                                                                // in reverse based on date and time
            var query = from e in db.Records
                        orderby e.DateT descending
                        select e;

            return query;
        }


        // Adding Repositories
        public static void AddCustomer(Entity db, Customer customer)
        {
            db.Customer.Add(customer);// this will generate insert query for customers
            db.SaveChanges();// this will execute the above generate insert query
        }

        public static void AddRecords(Entity db, Records records)
        {
            db.Records.Add(records);// this will generate insert query for records
            db.SaveChanges();// this will execute the above generate insert query
        }
        

    }
    
}
