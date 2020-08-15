namespace CreditCheck.Controllers
{
    using CreditCheck.EFCore;
    using CreditCheck.Models;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : CreditCheckDBController<Customer, EfCoreCustomerRepository>
    {
        public CustomersController(EfCoreCustomerRepository repository, Data.AppDbContext context) : base(repository, context)
        { }
    }
}
