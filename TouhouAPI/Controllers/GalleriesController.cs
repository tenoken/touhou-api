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
    //[Route("api/[controller]")]
    [Produces("application/json")]
    [Route("Galleries")]
    [ApiController]
    public class GalleriesController : ControllerBase
    {
        private readonly TouhouContext _context;

        public GalleriesController(TouhouContext context)
        {
            _context = context;
        }

        // GET: api/Galleries
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gallery>>> GetGalleries()
        {
            return await _context.Galleries.ToListAsync();
        }

        // GET: api/Galleries/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Gallery>> GetGallery(string id)
        {
            var gallery = await _context.Galleries.FindAsync(id);

            if (gallery == null)
            {
                return NotFound();
            }

            return gallery;
        }

        // PUT: api/Galleries/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGallery(string id, Gallery gallery)
        {
            if (id != gallery.Id)
            {
                return BadRequest();
            }

            _context.Entry(gallery).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GalleryExists(id))
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

        // POST: api/Galleries
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[Authorize]
        //[HttpPost]
        //public async Task<IActionResult> PostGallery([FromBody] Gallery gallery)
        //{
        //    _context.Galleries.Add(gallery);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (GalleryExists(gallery.Id))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    //return CreatedAtAction("GetGallery", new { id = gallery.Id }, gallery);
        //    return Ok(gallery);
        //}

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Gallery gallery)
        {
            var userId = HttpContext.User.Claims.First().Value;

            //Set the owner id bellow;

            foreach (var photo in gallery.Photos)
            {
                photo.SetGallery(gallery);
                _context.Photos.Add(photo);
            }

            _context.Galleries.Add(gallery);
            await _context.SaveChangesAsync();

            return Ok(gallery);
        }

        // DELETE: api/Galleries/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Gallery>> DeleteGallery(string id)
        {
            var gallery = await _context.Galleries.FindAsync(id);
            if (gallery == null)
            {
                return NotFound();
            }

            _context.Galleries.Remove(gallery);
            await _context.SaveChangesAsync();

            return gallery;
        }

        private bool GalleryExists(string id)
        {
            return _context.Galleries.Any(e => e.Id == id);
        }
    }
}
