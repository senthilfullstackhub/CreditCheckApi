namespace CreditCheck.UnitTest.MockData
{
    using CreditCheck.Data;
    using CreditCheck.EFCore;
    using CreditCheck.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public class EfCoreCustomerRepository : EfCoreRepository<Customer, AppDbContext>
    {
        public EfCoreCustomerRepository(AppDbContext context = null) : base(context)
        { }

        public void Dispose()
        { }

        public Task<List<Customer>> GetAllAsync()
            => new Customer
            {
                Id = 1,
                FirstName = "Test",
                LastName = "Unit",
                DateOfBirth = DateTime.Now.AddYears(-25),
                Salary = 32000.00M,
                IsEligible = true
            }.AsListTask();

        public Task<Customer> GetByIdAsync(int id)
            => new Customer
            {
                Id = id,
                FirstName = "Test",
                LastName = "Unit",
                DateOfBirth = DateTime.Now.AddYears(-25),
                Salary = 32000.00M,
                IsEligible = true
            }.AsTask();

        public Task<Customer> AddAsync(Customer customer)
        {
            customer.Id = 1;
            return customer.AsTask();
        }

        public Task<Customer> UpdateAsync(int id, Customer customer)
        {
            customer.Id = id;
            return customer.AsTask();
        }

        public Task<Customer> DeleteAsync(int id)
        {
            var customer = new Customer()
            {
                Id = id,
                FirstName = "Test",
                LastName = "Unit",
                DateOfBirth = DateTime.Now.AddYears(-25),
                Salary = 32000.00M,
                IsEligible = true
            };
            return customer.AsTask();
        }
    }
}
