using DVDStorage2.Data;
using DVDStorage2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DVDStorage2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DVDController : ControllerBase
    {
        public DvdStorageContext Context { get; set; }

        public DVDController(DvdStorageContext context)
        {
            Context = context;
        }

        // GET: api/<DVDController>
        [HttpGet]
        public ActionResult<IEnumerable<Dvd>> Get()
        {
            return Ok(Context.Dvds.Include(x => x.Genres).Include(x => x.SpokenLanguages).Include(x => x.Subtitles));
        }

        // GET api/<DVDController>/5
        [HttpGet("{id}")]
        public ActionResult<Dvd> Get(Guid id)
        {
            try
            {
                return Ok(Context.Dvds.Include(x => x.Genres).Include(x => x.SpokenLanguages).Include(x => x.Subtitles).FirstOrDefault(x => x.Id.Equals(id)));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // POST api/<DVDController>
        [HttpPost]
        public ActionResult Post([FromBody] Dvd dvd)
        {
            if (dvd == null)
            {
                return BadRequest();
            }
            
            Context.AttachRange(dvd.Genres);
            Context.AttachRange(dvd.SpokenLanguages);
            Context.AttachRange(dvd.Subtitles);

            Context.Dvds.Add(dvd);
            Context.SaveChanges();
            return Ok(dvd);
        }

        // PUT api/<DVDController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Dvd dvd)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(id.Equals(dvd.Id))
            {
                return BadRequest();
            }

            Context.AttachRange(dvd.Genres);
            Context.AttachRange(dvd.SpokenLanguages);
            Context.AttachRange(dvd.Subtitles);

            Context.Entry(dvd).State = EntityState.Modified;

            try
            {
                Context.SaveChanges();
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!Context.Dvds.Any(x => x.Id.Equals(id)))
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

        // DELETE api/<DVDController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            Dvd? dvd = Context.Dvds.Find(id);
            if(dvd == null)
            {
                return NotFound();
            }

            Context.Dvds.Remove(dvd);
            Context.SaveChanges();

            return NoContent();
        }
    }
}
