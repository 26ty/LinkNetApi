using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LinkNetApi.Models;
using System.Net;

namespace LinkNetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollectionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CollectionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Collections
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Collection>>> GetCollection()
        {
          if (_context.Collection == null)
          {
              return NotFound();
          }
            return await _context.Collection.ToListAsync();
        }

        // GET: api/Collections/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Collection>> GetCollection(Guid id)
        {
          if (_context.Collection == null)
          {
              return NotFound();
          }
            var collection = await _context.Collection.FindAsync(id);

            if (collection == null)
            {
                return NotFound();
            }

            return collection;
        }

        // GET: api/Collections/getUserCollections/user
        [HttpGet("getUserCollections/{user_id}")]
        public async Task<ActionResult<IEnumerable<Collection>>> GetUserCollection(Guid user_id)
        {
            var collection = await _context.Collection
                .Where(a => a.user_id == user_id)
                .OrderByDescending(a => a.created_at)
                .ToListAsync();
            if (collection == null || collection.Count == 0)
            {
                return NotFound();
            }

            return collection;
        }

        // PUT: api/Collections/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCollection(Guid id, Collection collection)
        {
            if (id != collection.id)
            {
                return BadRequest();
            }

            _context.Entry(collection).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CollectionExists(id))
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

        // POST: api/Collections
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Response>> PostCollection(Collection collection)
        {
            Response response = new Response
            {
                data = collection
            };

            if (_context.Collection == null)
            {
              return Problem("Entity set 'ApplicationDbContext.Collection'  is null.");
            }
            else
            {
                response.status = 200;
            }

            _context.Collection.Add(collection);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCollection", new { id = collection.id }, response);
        }

        // DELETE: api/Collections/5
        [HttpDelete("{id}")]
        public async Task<HttpResponseMessage> DeleteCollection(Guid id)
        {
            if (_context.Collection == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            var collection = await _context.Collection.FindAsync(id);
            if (collection == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            _context.Collection.Remove(collection);
            await _context.SaveChangesAsync();

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        private bool CollectionExists(Guid id)
        {
            return (_context.Collection?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
