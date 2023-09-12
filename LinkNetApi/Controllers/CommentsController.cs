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
    public class CommentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CommentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Comments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetComment()
        {
          if (_context.Comment == null)
          {
              return NotFound();
          }
            return await _context.Comment.ToListAsync();
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetComment(Guid id)
        {
          if (_context.Comment == null)
          {
              return NotFound();
          }
            var comment = await _context.Comment.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return comment;
        }

        // GET: api/Comments/getArticleComments/id
        [HttpGet("getArticleComments/{article_id}")]
        public async Task<ActionResult<IEnumerable<Comment>>> getArticleComments(Guid article_id)
        {
            var comments = await _context.Comment
                .Where(a => a.article_id == article_id)
                .OrderByDescending(a => a.created_at)
                .ToListAsync();

            if (comments == null || comments.Count == 0)
            {
                return NotFound();
            }

            Response response = new Response
            {
                status = 200,
                data = comments
            };

            return comments;
        }

        //// GET: api/Comments/getArticleUserComment/{article_id}
        //[HttpGet("getArticleUserComment/{article_id}")]
        //public async Task<ActionResult<IEnumerable<Comment>>> getArticleUserComment(Guid article_id)
        //{
        //    var comments = await _context.Comment
        //        .Where(a => a.article_id == article_id)
        //        .Include(c => c.User) // 包含User对象的信息
        //        .OrderByDescending(a => a.created_at)
        //        .ToListAsync();

        //    if (comments == null || comments.Count == 0)
        //    {
        //        return NotFound();
        //    }

        //    Response response = new Response
        //    {
        //        status = 200,
        //        data = comments
        //    };

        //    return comments;
        //}

        // PUT: api/Comments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Response>> PutComment(Guid id, Comment comment)
        {
            Response response = new Response
            {
                data = comment
            };

            if (id != comment.id)
            {
                return BadRequest();
            }

            _context.Entry(comment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                response.status = 200;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction(nameof(GetComment), new { id = comment.id }, response);
        }

        //// POST: api/Comments
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Comment>> PostComment(Comment comment)
        //{
        //  if (_context.Comment == null)
        //  {
        //      return Problem("Entity set 'ApplicationDbContext.Comment'  is null.");
        //  }
        //    _context.Comment.Add(comment);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetComment", new { id = comment.id }, comment);
        //}

        // POST: api/Comments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Response>> PostComment(Comment comment)
        {
            Response response = new Response
            {
                data = comment
            };

            if (_context.Comment == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Comment'  is null.");
            }
            else
            {
                response.status = 200;
            }
            _context.Comment.Add(comment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComment", new { id = comment.id }, response);
        }

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<HttpResponseMessage> DeleteComment(Guid id)
        {
            if (_context.Comment == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            var comment = await _context.Comment.FindAsync(id);
            if (comment == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            _context.Comment.Remove(comment);
            await _context.SaveChangesAsync();

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        //// DELETE: api/Comments/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteComment(Guid id)
        //{
        //    if (_context.Comment == null)
        //    {
        //        return NotFound();
        //    }
        //    var comment = await _context.Comment.FindAsync(id);
        //    if (comment == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Comment.Remove(comment);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool CommentExists(Guid id)
        {
            return (_context.Comment?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
