namespace CreditCheck.Controllers
{
    using CreditCheck.Common.Extensions;
    using CreditCheck.Data;
    using CreditCheck.Models;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public abstract class CreditCheckDBController<TEntity, TRepository> : ControllerBase
        where TEntity : class, IEntity
        where TRepository : IRepository<TEntity>
    {
        private readonly TRepository repository;
        private readonly AppDbContext context;

        public CreditCheckDBController(TRepository repository, AppDbContext context)
        {
            this.repository = repository;
            this.context = context;
        }

        // GET: api/[controller]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TEntity>>> Get()
        {
            return await repository.GetAll();
        }

        // GET: api/[controller]/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TEntity>> Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var entity = await repository.Get(id);

            if (entity == null)
            {
                return NotFound();
            }

            return entity;
        }

        // PUT: api/[controller]/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, TEntity entity)
        {
            if (id != entity.Id)
            {
                return BadRequest();
            }

            entity.UpdatedOn = DateTime.Now;
            entity.UpdatedBy = HttpContext.User.Identity.Name;

            await repository.Update(entity);
            return Ok(entity);
        }

        // POST: api/[controller]
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<TEntity>> Post([FromBody] TEntity entity)
        {
            if (ModelState.IsValid)
            {
                if (entity.GetType() == typeof(Customer))
                {
                    var customer = (Customer)Convert.ChangeType(entity, typeof(Customer));
                    var customerCard = this.context.Cards.Where(x => x.AgeLimit <= customer.DateOfBirth.ToGetAge()
                                    && x.SalaryMin <= customer.Salary).OrderByDescending(o => o.SalaryMin).FirstOrDefault();

                    customer.IsEligible = (customerCard != null) ? true : false;
                    customer.CardId = customerCard?.Id;
                }

                entity.CreatedOn = DateTime.Now;
                entity.CreatedBy = "SYSTEM"; //HttpContext.User.Identity.Name;
                await repository.Add(entity);
                return CreatedAtAction("Get", new { id = entity.Id }, entity);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // DELETE: api/[controller]/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TEntity>> Delete(int id)
        {
            var entity = await repository.Delete(id);
            if (entity == null)
            {
                return NotFound();
            }
            return entity;
        }
    }
}
