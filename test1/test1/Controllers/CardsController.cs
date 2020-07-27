using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test1.Models;

namespace test1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        CardsContext db;
        public CardsController(CardsContext context)
        {
            db = context;
            if (!db.Cards.Any())
            {
                db.Cards.Add(new Card { Title = "Milk", Url = "/img/milk.jpg" });
                db.Cards.Add(new Card { Title = "Bread", Url = "/img/bread.jpg" });
                db.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Card>>> Get()
        {
            return await db.Cards.ToListAsync();
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Card>> Get(int id)
        {
            Card card = await db.Cards.FirstOrDefaultAsync(x => x.Id == id);
            if (card == null)
                return NotFound();
            return new ObjectResult(card);
        }

        // POST api/users
        [HttpPost]
        public async Task<ActionResult<Card>> Post(Card card)
        {
            if (card == null)
            {
                return BadRequest();
            }

            db.Cards.Add(card);
            await db.SaveChangesAsync();
            return Ok(card);
        }

        // PUT api/users/
        [HttpPut]
        public async Task<ActionResult<Card>> Put(Card card)
        {
            if (card == null)
            {
                return BadRequest();
            }
            if (!db.Cards.Any(x => x.Id == card.Id))
            {
                return NotFound();
            }

            db.Update(card);
            await db.SaveChangesAsync();
            return Ok(card);
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Card>> Delete(int id)
        {
            Card card = db.Cards.FirstOrDefault(x => x.Id == id);
            if (card == null)
            {
                return NotFound();
            }
            db.Cards.Remove(card);
            await db.SaveChangesAsync();
            return Ok(card);
        }
    }
}
