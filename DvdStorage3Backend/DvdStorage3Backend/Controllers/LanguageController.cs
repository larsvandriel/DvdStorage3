using DVDStorage2.Data;
using DVDStorage2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DvdStorage3Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        public DvdStorageContext Context { get; set; }

        public LanguageController(DvdStorageContext context)
        {
            Context = context;
        }

        // GET: api/<LanguageController>
        [HttpGet]
        public ActionResult<IEnumerable<Language>> Get()
        {
            return Ok(Context.Languages);
        }

        // GET api/<LanguageController>/5
        [HttpGet("{id}")]
        public ActionResult<Language> Get(Guid id)
        {
            try
            {
                return Ok(Context.Languages.FirstOrDefault(x => x.Id.Equals(id)));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // POST api/<LanguageController>
        [HttpPost]
        public ActionResult Post([FromBody] Language language)
        {
            if (language == null)
            {
                return BadRequest();
            }
            Context.Languages.Add(language);
            Context.SaveChanges();
            return Ok(language);
        }

        // PUT api/<LanguageController>/5
        [HttpPut("{id}")]
        public ActionResult Put(Guid id, [FromBody] Language language)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id.Equals(language.Id))
            {
                return BadRequest();
            }

            Context.Entry(language).State = EntityState.Modified;

            try
            {
                Context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Context.Languages.Any(x => x.Id.Equals(id)))
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

        // DELETE api/<LanguageController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            Language? language = Context.Languages.Find(id);
            if (language == null)
            {
                return NotFound();
            }

            Context.Languages.Remove(language);
            Context.SaveChanges();

            return NoContent();
        }
    }
}
