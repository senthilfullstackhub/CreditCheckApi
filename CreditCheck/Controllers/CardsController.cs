namespace CreditCheck.Controllers
{
    using CreditCheck.EFCore;
    using CreditCheck.Models;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : CreditCheckDBController<Card, EfCoreCardRepository>
    {
        public CardsController(EfCoreCardRepository repository, Data.AppDbContext context) : base(repository, context)
        { }
    }
}
