using DVDStorage2.Data;
using DVDStorage2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DvdStorage3Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        public DvdStorageContext Context { get; set; }

        public GenreController(DvdStorageContext context)
        {
            Context = context;
        }

        // GET: api/<GenreController>
        [HttpGet]
        public ActionResult<IEnumerable<Genre>> Get()
        {
            return Context.Genres;
        }

        // GET api/<GenreController>/5
        [HttpGet("{id}")]
        public ActionResult Get(Guid id)
        {
            try
            {
                return Ok(Context.Genres.FirstOrDefault(x => x.Id.Equals(id)));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // POST api/<GenreController>
        [HttpPost]
        public ActionResult Post([FromBody] Genre genre)
        {
            if (genre == null)
            {
                return BadRequest();
            }
            Context.Genres.Add(genre);
            Context.SaveChanges();
            return Ok(genre);
        }

        // PUT api/<GenreController>/5
        [HttpPut("{id}")]
        public ActionResult Put(Guid id, [FromBody] Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id.Equals(genre.Id))
            {
                return BadRequest();
            }

            Context.Entry(genre).State = EntityState.Modified;

            try
            {
                Context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Context.Genres.Any(x => x.Id.Equals(id)))
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

        // DELETE api/<GenreController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            Genre? genre = Context.Genres.Find(id);
            if (genre == null)
            {
                return NotFound();
            }

            Context.Genres.Remove(genre);
            Context.SaveChanges();

            return NoContent();
        }
    }
}
