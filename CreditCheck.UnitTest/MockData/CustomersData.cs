using CreditCheck.Models;
using System;
using System.Collections.Generic;

namespace CreditCheck.UnitTest.MockData
{
    public static class CustomersData
    {
        public static Customer GetUser()
                 => new Customer
                 {
                     FirstName = null,
                     LastName = "Doe",
                     DateOfBirth = DateTime.Now.AddYears(-20),
                     Salary = 30000.00M
                 };
        public static List<Customer> GetUsers()
                => new List<Customer>()
                {
                        new Customer
                        {
                            FirstName = null,
                            LastName = "Doe",
                            DateOfBirth = DateTime.Now.AddYears(-20),
                            Salary = 30000.00M
                        }
                };
    }
}