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
    [Route("Photos")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly TouhouContext _context;

        public PhotosController(TouhouContext context)
        {
            _context = context;
        }

        // GET: api/Photos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Photo>>> GetPhotos()
        {
            return await _context.Photos.ToListAsync();
        }

        // GET: api/Photos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Photo>> GetPhoto(string id)
        {
            var photo = await _context.Photos.FindAsync(id);

            if (photo == null)
            {
                return NotFound();
            }

            return photo;
        }

        // PUT: api/Photos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPhoto(string id, Photo photo)
        {
            if (id != photo.Id)
            {
                return BadRequest();
            }

            _context.Entry(photo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhotoExists(id))
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

        // POST: api/Photos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[Authorize]
        //[HttpPost]
        //public async Task<IActionResult> PostPhoto([FromBody] Photo photo)
        //{
        //    var userId = HttpContext.User.Claims.First().Value;

        //    _context.Photos.Add(photo);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (PhotoExists(photo.Id))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    //return CreatedAtAction("GetPhoto", new { id = photo.Id }, photo);
        //    return Ok(photo);
        //}

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Photo photo)
        {
            var userId = HttpContext.User.Claims.First().Value;

            //Set the owner id bellow;

            _context.Photos.Add(photo);
            await _context.SaveChangesAsync();

            return Ok(photo);
        }

        // DELETE: api/Photos/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Photo>> DeletePhoto(string id)
        {
            var photo = await _context.Photos.FindAsync(id);
            if (photo == null)
            {
                return NotFound();
            }

            _context.Photos.Remove(photo);
            await _context.SaveChangesAsync();

            return photo;
        }

        private bool PhotoExists(string id)
        {
            return _context.Photos.Any(e => e.Id == id);
        }
    }
}
