namespace CreditCheck.EFCore
{
    using CreditCheck.Data;
    using CreditCheck.Models;

    public class EfCoreCardRepository : EfCoreRepository<Card, AppDbContext>
    {
        public EfCoreCardRepository(AppDbContext context) : base(context)
        { }
    }
}
