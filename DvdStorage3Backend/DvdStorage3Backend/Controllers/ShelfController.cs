using DVDStorage2.Data;
using DVDStorage2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DvdStorage3Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShelfController : ControllerBase
    {
        public DvdStorageContext Context { get; set; }

        public ShelfController(DvdStorageContext context)
        {
            Context = context;
        }

        // GET: api/<ShelfController>
        [HttpGet]
        public ActionResult<IEnumerable<Shelf>> Get()
        {
            return Ok(Context.Shelves.Include(x => x.Dvds));
        }

        // GET api/<ShelfController>/5
        [HttpGet("{id}")]
        public ActionResult<Shelf> Get(Guid id)
        {
            try
            {
                return Ok(Context.Shelves.Include(x => x.Dvds).FirstOrDefault(x => x.Id.Equals(id)));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // POST api/<ShelfController>
        [HttpPost]
        public ActionResult Post([FromBody] Shelf shelf)
        {
            if (shelf == null)
            {
                return BadRequest();
            }

            Context.AttachRange(shelf.Dvds);

            Context.Shelves.Add(shelf);
            Context.SaveChanges();
            return Ok(shelf);
        }

        // PUT api/<ShelfController>/5
        [HttpPut("{id}")]
        public ActionResult Put(Guid id, [FromBody] Shelf shelf)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id.Equals(shelf.Id))
            {
                return BadRequest();
            }

            Context.AttachRange(shelf.Dvds);

            Context.Entry(shelf).State = EntityState.Modified;

            try
            {
                Context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Context.Shelves.Any(x => x.Id.Equals(id)))
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

        // DELETE api/<ShelfController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            Shelf? shelf = Context.Shelves.Find(id);
            if (shelf == null)
            {
                return NotFound();
            }

            Context.Shelves.Remove(shelf);
            Context.SaveChanges();

            return NoContent();
        }
    }
}
