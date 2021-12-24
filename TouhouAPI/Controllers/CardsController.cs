using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TouhouArticleMaker.Domain;
using TouhouData;
using TouhouData.Context;

namespace TouhouAPI.Controllers
{
    [Produces("application/json")]
    [Route("Cards")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly TouhouContext _context;

        public CardsController(TouhouContext context)
        {
            _context = context;
        }

        // GET: api/Cards
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Card>>> GetCards()
        {
            return await _context.Cards.ToListAsync();
        }

        // GET: api/Cards/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Card>> GetCard(string id)
        {
            var card = await _context.Cards.FindAsync(id);

            if (card == null)
            {
                return NotFound();
            }

            return card;
        }

        // PUT: api/Cards/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCard(string id, Card card)
        {
            if (id != card.Id)
            {
                return BadRequest();
            }

            _context.Entry(card).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cards
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Card>> PostCard(Card card)
        {
            _context.Cards.Add(card);
            card.SetPhotoId(card.Photo);
            _context.Photos.Add(card.Photo);
           
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CardExists(card.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            //return CreatedAtAction("GetCard", new { id = card.Id }, card);
            return Ok(card);
        }

        // DELETE: api/Cards/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Card>> DeleteCard(string id)
        {
            var card = await _context.Cards.FindAsync(id);
            if (card == null)
            {
                return NotFound();
            }

            _context.Cards.Remove(card);
            await _context.SaveChangesAsync();

            return card;
        }

        private bool CardExists(string id)
        {
            return _context.Cards.Any(e => e.Id == id);
        }
    }
}
