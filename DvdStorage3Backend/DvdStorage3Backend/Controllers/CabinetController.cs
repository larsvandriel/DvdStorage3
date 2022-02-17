using DVDStorage2.Data;
using DVDStorage2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DvdStorage3Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CabinetController : ControllerBase
    {
        public DvdStorageContext Context { get; set; }

        public CabinetController(DvdStorageContext context)
        {
            Context = context;
        }

        // GET: api/<CabinetController>
        [HttpGet]
        public ActionResult<IEnumerable<Cabinet>> Get()
        {
            return Ok(Context.Cabinets.Include(x => x.Shelves).ThenInclude(y => y.Dvds));
        }

        // GET api/<CabinetController>/5
        [HttpGet("{id}")]
        public ActionResult<Cabinet> Get(Guid id)
        {
            try
            {
                return Ok(Context.Cabinets.Include(x => x.Shelves).ThenInclude(y => y.Dvds).FirstOrDefault(x => x.Id.Equals(id)));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // POST api/<CabinetController>
        [HttpPost]
        public ActionResult Post([FromBody] Cabinet cabinet)
        {
            if (cabinet == null)
            {
                return BadRequest();
            }
            
            Context.AttachRange(cabinet.Shelves);
            
            Context.Add(cabinet);
            Context.SaveChanges();
            return Ok(cabinet);
        }

        // PUT api/<CabinetController>/5
        [HttpPut("{id}")]
        public ActionResult Put(Guid id, [FromBody] Cabinet cabinet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id.Equals(cabinet.Id))
            {
                return BadRequest();
            }

            Context.AttachRange(cabinet.Shelves);
            
            Context.Entry(cabinet).State = EntityState.Modified;

            try
            {
                Context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Context.Cabinets.Any(x => x.Id.Equals(id)))
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

        // DELETE api/<CabinetController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            Cabinet? cabinet = Context.Cabinets.Find(id);
            if (cabinet == null)
            {
                return NotFound();
            }

            Context.Cabinets.Remove(cabinet);
            Context.SaveChanges();

            return NoContent();
        }
    }
}
