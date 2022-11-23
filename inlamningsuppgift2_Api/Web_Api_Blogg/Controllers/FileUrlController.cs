using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Api_Blogg.Data;
using Web_Api_Blogg.Models.Entities;

namespace Web_Api_Blogg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUrlController : ControllerBase
    {
        private readonly SqlContext _context;

        public FileUrlController(SqlContext context)
        {
            _context = context;
        }

        // GET: api/FileUrl
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FileUrlEntity>>> GetFileUrls()
        {
            return await _context.FileUrls.ToListAsync();
        }

        // GET: api/FileUrl/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FileUrlEntity>> GetFileUrlEntity(int id)
        {
            var fileUrlEntity = await _context.FileUrls.FindAsync(id);

            if (fileUrlEntity == null)
            {
                return NotFound();
            }

            return fileUrlEntity;
        }

        // PUT: api/FileUrl/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFileUrlEntity(int id, FileUrlEntity fileUrlEntity)
        {
            if (id != fileUrlEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(fileUrlEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FileUrlEntityExists(id))
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

        // POST: api/FileUrl
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FileUrlEntity>> PostFileUrlEntity(FileUrlEntity fileUrlEntity)
        {
            _context.FileUrls.Add(fileUrlEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFileUrlEntity", new { id = fileUrlEntity.Id }, fileUrlEntity);
        }

        // DELETE: api/FileUrl/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFileUrlEntity(int id)
        {
            var fileUrlEntity = await _context.FileUrls.FindAsync(id);
            if (fileUrlEntity == null)
            {
                return NotFound();
            }

            _context.FileUrls.Remove(fileUrlEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FileUrlEntityExists(int id)
        {
            return _context.FileUrls.Any(e => e.Id == id);
        }
    }
}
