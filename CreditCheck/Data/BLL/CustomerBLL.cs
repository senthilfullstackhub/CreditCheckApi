namespace CreditCheck.Data.BLL
{
    using CreditCheck.Common.Extensions;
    using CreditCheck.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public static class CustomerBLL
    {
        public static void ValidateCustomer(Customer customer, AppDbContext context)
        {
            var customerCard = context.Cards.Where(x => x.AgeLimit <= customer.DateOfBirth.ToGetAge()
                                   && x.SalaryMin <= customer.Salary).OrderByDescending(o => o.SalaryMin).FirstOrDefault();

            customer.IsEligible = (customerCard != null) ? true : false;
            customer.CardId = customerCard?.Id;
        }
    }
}
