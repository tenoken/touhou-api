using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TouhouArticleMaker.Domain;
using TouhouData.Context;

namespace TouhouAPI.Controllers
{
    [Produces("application/json")]
    [Route("Articles")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        readonly TouhouContext _context;
        public ArticlesController(TouhouContext context)
        {
            _context = context;
        }
        // GET: api/Article
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticles()
        {
            return await _context.Articles.ToListAsync();            
        }

        // GET: api/Article/5
        //[HttpGet("{id}", Name = "Get")]
        [HttpGet("{id}")]
        public async Task<Article> GetArticle(string id)
        {
            var article = await _context.Articles.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return article;
        }

        // POST: api/Article
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Article article)
        {
            try
            {
                if (!article.IsValid)
                    return BadRequest();

                var userId = HttpContext.User.Claims.Skip(1).Take(1).FirstOrDefault().Value;
                var authorId = HttpContext.User.Claims.Skip(1).Take(1).FirstOrDefault().Value;

                var articleAuthor = await _context.Authors.FirstOrDefaultAsync(a => a.UserName.Text == userId);

                if (articleAuthor == null)
                    return Unauthorized();

                article.SetAuthor(articleAuthor);

                _context.Articles.Add(article);

                foreach (var section in article.Sections)
                {
                    section.SetArticleId(article);
                    _context.Sections.Add(section);
                }

                await _context.SaveChangesAsync();

                return Ok(article);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Article/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Article article)
        {
            if (id != article.Id)
                return BadRequest();

            if (!article.IsValid)
                return BadRequest();

            var userId = HttpContext.User.Claims.Skip(1).Take(1).FirstOrDefault().Value;

            var articleAuthor = await _context.Authors.FirstOrDefaultAsync(a => a.UserName.Text == userId);

            if (articleAuthor == null)
                return Unauthorized();

            var articleToBeUpadated = await _context.Articles.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id && x.AuthorId == articleAuthor.Id);

            _context.Entry(articleToBeUpadated).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(article);
        }

        // DELETE: api/ApiWithActions/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {

            var userId = HttpContext.User.Claims.Skip(1).Take(1).FirstOrDefault().Value;

            var articleAuthor = await _context.Authors.FirstOrDefaultAsync(a => a.UserName.Text == userId);

            if (articleAuthor == null)
                return Unauthorized();

            var article = await _context.Articles.FindAsync(id);
            if (article == null || article.AuthorId != articleAuthor.Id)
            {
                return NotFound();
            }

            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
