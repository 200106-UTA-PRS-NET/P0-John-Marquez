using PizzaBox.Domain.Models;
using System;
using Xunit;

namespace PizzaBox.Testing
{
    public class PizzaTesting
    {
        private readonly Customer cus  = new Customer();
        private readonly Employee emp = new Employee();
        private readonly Records rec = new Records();
        [Fact]
        public void Name_NonEmptyValue_StoresCorrectly()
        {
            string name = "John";
            cus.Fname = name;
            Assert.Equal(name, cus.Fname);
        }

        [Fact]
        public void Username_NonEmptyValue_StoresCorrectly()
        {
            string uname = "lsk123";
            emp.Uname = uname;
            Assert.Equal(uname, emp.Uname);
        }

        [Fact]
        public void Record_NonEmptyValue_StoresCorrectly()
        {
            string size = "large";
            rec.Size = size;
            Assert.Equal(size, rec.Size);
        }

    }
}
