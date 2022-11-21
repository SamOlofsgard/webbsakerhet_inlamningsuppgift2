using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Api_Blogg.Data;
using Web_Api_Blogg.Models.Entities;

namespace Web_Api_Blogg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloggPostsController : ControllerBase
    {
        private readonly SqlContext _context;

        public BloggPostsController(SqlContext context)
        {
            _context = context;
        }

        // GET: api/BloggPosts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BloggPostsEntity>>> GetBloggPosts()
        {
            return await _context.BloggPosts.ToListAsync();
        }

        // GET: api/BloggPosts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BloggPostsEntity>> GetBloggPostsEntity(int id)
        {
            var bloggPostsEntity = await _context.BloggPosts.FindAsync(id);

            if (bloggPostsEntity == null)
            {
                return NotFound();
            }

            return bloggPostsEntity;
        }

        // PUT: api/BloggPosts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBloggPostsEntity(int id, BloggPostsEntity bloggPostsEntity)
        {
            if (id != bloggPostsEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(bloggPostsEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BloggPostsEntityExists(id))
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

        // POST: api/BloggPosts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<BloggPostsEntity>> PostBloggPostsEntity(BloggPostsEntity bloggPostsEntity)
        {
            _context.BloggPosts.Add(bloggPostsEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBloggPostsEntity", new { id = bloggPostsEntity.Id }, bloggPostsEntity);
        }

        // DELETE: api/BloggPosts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBloggPostsEntity(int id)
        {
            var bloggPostsEntity = await _context.BloggPosts.FindAsync(id);
            if (bloggPostsEntity == null)
            {
                return NotFound();
            }

            _context.BloggPosts.Remove(bloggPostsEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BloggPostsEntityExists(int id)
        {
            return _context.BloggPosts.Any(e => e.Id == id);
        }
    }
}
