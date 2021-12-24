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
    [Route("Sections")]
    [ApiController]
    public class SectionsController : ControllerBase
    {
        private readonly TouhouContext _context;

        public SectionsController(TouhouContext context)
        {
            _context = context;
        }

        // GET: api/Sections
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Section>>> GetSections()
        {
            return await _context.Sections.ToListAsync();
        }

        // GET: api/Sections/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Section>> GetSection(string id)
        {
            var section = await _context.Sections.FindAsync(id);

            if (section == null)
            {
                return NotFound();
            }

            return section;
        }

        // PUT: api/Sections/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSection(string id, Section section)
        {
            if (id != section.Id)
            {
                return BadRequest();
            }

            _context.Entry(section).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SectionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(section); //NoContent();
        }

        // POST: api/Sections
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Section>> PostSection(Section section)
        {
            _context.Sections.Add(section);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SectionExists(section.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSection", new { id = section.Id }, section);
        }

        // DELETE: api/Sections/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Section>> DeleteSection(string id)
        {
            var section = await _context.Sections.FindAsync(id);
            if (section == null)
            {
                return NotFound();
            }

            _context.Sections.Remove(section);
            await _context.SaveChangesAsync();

            return section;
        }

        private bool SectionExists(string id)
        {
            return _context.Sections.Any(e => e.Id == id);
        }
    }
}
