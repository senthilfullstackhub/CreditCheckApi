namespace CreditCheck.EFCore
{
    using CreditCheck.Data;
    using CreditCheck.Models;

    public class EfCoreCustomerRepository : EfCoreRepository<Customer, AppDbContext>
    {
        public EfCoreCustomerRepository(AppDbContext context) : base(context)
        { }
    }
}
