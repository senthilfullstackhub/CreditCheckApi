namespace CreditCheck.UnitTest.RepositoryTest
{
    using CreditCheck.Models;
    using CreditCheck.UnitTest.MockData;
    using JetBrains.dotMemoryUnit;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;

    public class EfCoreCustomerRepositoryTest
    {
        private readonly EfCoreCustomerRepository _repo;
        private readonly Customer _newCustomer;

        public EfCoreCustomerRepositoryTest()
        {
            this._repo = new EfCoreCustomerRepository();
            this._newCustomer = Setup();
        }

        private Customer Setup()
        {
            return new Customer()
            {
                FirstName = "test",
                LastName = "sample",
                DateOfBirth = DateTime.Now,
                Salary = 25000M
            };
        }

        [DotMemoryUnit(FailIfRunWithoutSupport = false)]
        [Fact]
        public async Task CustomerGetAllAsync()
        {
            // Arrange

            // Act
            var customer = await _repo.GetAllAsync();

            // Assert
            Assert.Single(customer);
        }

        [DotMemoryUnit(FailIfRunWithoutSupport = false)]
        [Fact]
        public async Task CustomerGetByIdAsync()
        {
            // Arrange
            var id = 1;

            // Act
            var customer = await _repo.GetByIdAsync(id);

            // Assert
            Assert.Equal(id, customer.Id);
        }

        [DotMemoryUnit(FailIfRunWithoutSupport = false)]
        [Fact]
        public async Task CustomerAddAsync()
        {
            // Arrange
            var id = 1;

            // Act
            var customer = await _repo.AddAsync(this._newCustomer);

            // Assert
            Assert.Equal(id, customer.Id);
        }

        [DotMemoryUnit(FailIfRunWithoutSupport = false)]
        [Fact]
        public async Task CustomerUpdateAsync()
        {
            // Arrange
            var id = 1;

            // Act
            var customer = await _repo.UpdateAsync(id, this._newCustomer);

            // Assert
            Assert.Equal(id, customer.Id);
        }

        [DotMemoryUnit(FailIfRunWithoutSupport = false)]
        [Fact]
        public async Task CustomerDeleteAsync()
        {
            // Arrange
            var id = 1;

            // Act
            var customer = await _repo.DeleteAsync(id);

            // Assert
            Assert.Equal(id, customer.Id);
        }

    }
}
