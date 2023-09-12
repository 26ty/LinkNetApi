using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LinkNetApi.Models;
using System.Net.Http;
using System.Net;

namespace LinkNetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ArticlesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Articles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticle()
        {
          if (_context.Article == null)
          {
              return NotFound();
          }
            return await _context.Article.OrderByDescending(a => a.updated_at).ToListAsync();
        }

        // GET: api/Articles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Article>> GetArticle(Guid id)
        {
          if (_context.Article == null)
          {
              return NotFound();
          }
            var article = await _context.Article.FindAsync(id);

            if (article == null)
            {
                return NotFound();
            }

            return article;
        }

        // GET: api/Articles/getUserArticles/user
        [HttpGet("getUserArticles/{user_id}")]
        public async Task<ActionResult<IEnumerable<Article>>> GetUserArticle(Guid user_id)
        {
            var articles = await _context.Article
                .Where(a => a.user_id == user_id)
                .OrderByDescending(a => a.updated_at)
                .ToListAsync();
            if(articles == null || articles.Count == 0)
            {
                return NotFound();
            }

            return articles;
        }

        // GET: api/Articles/getRandomArticles/5
        [HttpGet("getRandomArticles/{count?}")]
        public async Task<ActionResult<IEnumerable<Article>>> GetRandomArticles(int? count = null)
        {
            if(count.HasValue && count.Value <= 0)
            {
                return BadRequest("Count must be a positive number.");
            }

            //var random = new Random();
            var randomArticles = await _context.Article
                .OrderBy(a => Guid.NewGuid()) // 使用Guid.NewGuid()生成隨機排序
                .Take(count.Value)
                .ToListAsync();

            return randomArticles;
        }

        //// PUT: api/Articles/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutArticle(Guid id, Article article)
        //{
        //    if (id != article.id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(article).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ArticleExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // PUT: api/Articles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Response>> PutArticle(Guid id, Article article)
        {
            Response response = new Response
            {
                data = article
            };

            if (id != article.id)
            {
                return BadRequest();
            }

            _context.Entry(article).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                response.status = 200;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction(nameof(GetArticle), new { id = article.id }, response);
        }

        //// POST: api/Articles
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Article>> PostArticle(Article article)
        //{
        //  if (_context.Article == null)
        //  {
        //      return Problem("Entity set 'ApplicationDbContext.Article'  is null.");
        //  }
        //    _context.Article.Add(article);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetArticle", new { id = article.id }, article);
        //}

        // POST: api/Articles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Response>> PostArticle(Article article)
        {
            Response response = new Response
            {
                data = article
            };

            if (_context.Article == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Article'  is null.");
            }
            else
            {
                response.status = 200;
            }
            _context.Article.Add(article);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArticle", new { id = article.id }, response);
        }

        // DELETE: api/Articles/5
        [HttpDelete("{id}")]
        public async Task<HttpResponseMessage> DeleteArticle(Guid id)
        {
            if (_context.Article == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            var article = await _context.Article.FindAsync(id);
            if (article == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            _context.Article.Remove(article);
            await _context.SaveChangesAsync();

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        //// DELETE: api/Articles/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteArticle(Guid id)
        //{
        //    if (_context.Article == null)
        //    {
        //        return NotFound();
        //    }
        //    var article = await _context.Article.FindAsync(id);
        //    if (article == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Article.Remove(article);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool ArticleExists(Guid id)
        {
            return (_context.Article?.Any(e => e.id == id)).GetValueOrDefault();
        }

        // POST: api/Articles/uploadImageFile
        // 圖片上傳
        [HttpPost("uploadImageFile")]
        public ActionResult<Response> PostImage(IFormFile imageFile)
        {
            try
            {
                if (imageFile == null)
                {
                    return BadRequest("No image file uploaded.");
                }

                string fileName = imageFile.FileName;
                byte[] imageBytes;

                using (var memoryStream = new MemoryStream())
                {
                    imageFile.CopyTo(memoryStream);
                    imageBytes = memoryStream.ToArray();
                }

                String imagePath = SaveImageToFile(imageBytes, fileName);

                Response response = new Response
                {
                    status = 200,
                    message = "ImageFile upload success!",
                    data = imagePath
                };

                //return Ok("Image uploaded successfully.");
                return response;
            }
            catch (Exception ex)
            {
                return BadRequest("Error uploading image: " + ex.Message);
            }
        }

        private String SaveImageToFile(byte[] imageBytes, string fileName)
        {
            string imagePath = Path.Combine("File", fileName);

            using (FileStream fs = new FileStream(imagePath, FileMode.Create))
            {
                fs.Write(imageBytes, 0, imageBytes.Length);
            }

            return fileName;
        }


        private readonly string _imageFolderPath = "File"; // 圖片資料夾路徑


        // GET: api/Articles/ImageFile
        /*圖片上傳*/
        [HttpGet("getImageFile/{imageName}")]
        public IActionResult GetImage(string imageName)
        {
            try
            {
                var imagePath = Path.Combine(_imageFolderPath, imageName);
                var imageBytes = System.IO.File.ReadAllBytes(imagePath);

                return File(imageBytes, "image/jpeg"); 
            }
            catch (Exception ex)
            {
                return BadRequest("Error get image: " + ex.Message);
            }
            
        }

    }

    public class ImageUploadModel
    {
        public string Base64Image { get; set; }
    }
}
