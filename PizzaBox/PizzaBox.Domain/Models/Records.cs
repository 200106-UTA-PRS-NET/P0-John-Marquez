using System;
using System.Collections.Generic;

namespace PizzaBox.Domain.Models
{
    public partial class Records
    {
        public int Id { get; set; }
        public int? LocatId { get; set; }
        public int? UserId { get; set; }
        public decimal? Total { get; set; }
        public DateTime DateT { get; set; }
        public int? AmountP { get; set; }
        public string PizzaType { get; set; }
        public string Size { get; set; }
        public string Crust { get; set; }

        public virtual Locations Locat { get; set; }
        public virtual Customer User { get; set; }
    }
}
