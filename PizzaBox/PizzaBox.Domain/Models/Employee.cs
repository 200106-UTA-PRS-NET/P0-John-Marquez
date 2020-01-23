using System;
using System.Collections.Generic;

namespace PizzaBox.Domain.Models
{
    public partial class Employee
    {
        public int Id { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Uname { get; set; }
        public string Pword { get; set; }
    }
}
