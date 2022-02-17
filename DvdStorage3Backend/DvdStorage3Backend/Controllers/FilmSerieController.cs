using DVDStorage2.Data;
using DVDStorage2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DvdStorage3Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmSerieController : ControllerBase
    {
        public DvdStorageContext Context { get; set; }

        public FilmSerieController(DvdStorageContext context)
        {
            Context = context;
        }

        // GET: api/<FilmSeriesController>
        [HttpGet]
        public ActionResult<IEnumerable<FilmSerie>> Get()
        {
            return Ok(Context.FilmSeries.Include(x => x.Dvds));
        }

        // GET api/<FilmSeriesController>/5
        [HttpGet("{id}")]
        public ActionResult<FilmSerie> Get(Guid id)
        {
            try
            {
                return Ok(Context.FilmSeries.Include(x => x.Dvds).FirstOrDefault(x => x.Id.Equals(id)));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // POST api/<FilmSeriesController>
        [HttpPost]
        public ActionResult Post([FromBody] FilmSerie filmSerie)
        {
            if (filmSerie == null)
            {
                return BadRequest();
            }

            Context.AttachRange(filmSerie.Dvds);
            
            Context.FilmSeries.Add(filmSerie);
            Context.SaveChanges();
            return Ok(filmSerie);
        }

        // PUT api/<FilmSeriesController>/5
        [HttpPut("{id}")]
        public ActionResult Put(Guid id, [FromBody] FilmSerie filmSerie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id.Equals(filmSerie.Id))
            {
                return BadRequest();
            }
            
            Context.AttachRange(filmSerie.Dvds);
            Context.Entry(filmSerie).State = EntityState.Modified;

            try
            {
                Context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Context.FilmSeries.Any(x => x.Id.Equals(id)))
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

        // DELETE api/<FilmSeriesController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            FilmSerie? filmSerie = Context.FilmSeries.Find(id);
            if (filmSerie == null)
            {
                return NotFound();
            }

            Context.FilmSeries.Remove(filmSerie);
            Context.SaveChanges();

            return NoContent();
        }
    }
}
