using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using SianApi.Models;

namespace SianApi.Controllers
{
    public class PizarraMarcaEnvioController : ApiController
    {
        private SianModel db = new SianModel();

        // GET: api/PizarraMarcaEnvio
        public IQueryable<tbl_PizarraMarcaEnvio> Gettbl_PizarraMarcaEnvio()
        {
            return db.tbl_PizarraMarcaEnvio;
        }

        /*
        // GET: api/PizarraMarcaEnvio/5
        [ResponseType(typeof(tbl_PizarraMarcaEnvio))]
        public async Task<IHttpActionResult> Gettbl_PizarraMarcaEnvio(int id)
        {
            tbl_PizarraMarcaEnvio tbl_PizarraMarcaEnvio = await db.tbl_PizarraMarcaEnvio.FindAsync(id);
            if (tbl_PizarraMarcaEnvio == null)
            {
                return NotFound();
            }
            return Ok(tbl_PizarraMarcaEnvio);
        }
        */

        // GET: api/PizarraMarcaEnvio/22023
        [HttpGet]
        [Route("api/PizarraMarcaEnvio/{indexSybase}")]
        [ResponseType(typeof(tbl_PizarraMarcaEnvio))]
        public async Task<IHttpActionResult> Gettbl_PizarraMarcaEnvio(int indexSybase)
        {
            IEnumerable<tbl_PizarraMarcaEnvio> tbl_PizarraMarcaEnvio = await db.tbl_PizarraMarcaEnvio.Where(x => x.nIndexSybase == indexSybase).OrderByDescending(p => p.nIdPizarraMarcaEnvio).Take(20).ToListAsync();
            return Ok(tbl_PizarraMarcaEnvio);
        }

        // GET: api/PizarraMarcaEnvio/json/5
        [HttpGet]
        [Route("api/PizarraMarcaEnvio/json/{id}")]
        [ResponseType(typeof(tbl_PizarraMarcaEnvio))]
        public async Task<IHttpActionResult> Gettbl_PizarraMarcaEnvioJson(int id)
        {
            var pizarraMarcaEnvio = await db.tbl_PizarraMarcaEnvio.FirstOrDefaultAsync(x => x.nIdPizarraMarcaEnvio == id);
            string jsonEnvio = pizarraMarcaEnvio.sJson;
            return Ok(jsonEnvio);
        }

        // GET: api/PizarraMarcaEnvio/response/5
        [HttpGet]
        [Route("api/PizarraMarcaEnvio/response/{id}")]
        [ResponseType(typeof(tbl_PizarraMarcaEnvio))]
        public async Task<IHttpActionResult> Gettbl_PizarraMarcaEnvioResponse(int id)
        {
            var pizarraMarcaEnvio = await db.tbl_PizarraMarcaEnvio.FirstOrDefaultAsync(x => x.nIdPizarraMarcaEnvio == id);
            string responseEnvio = pizarraMarcaEnvio.sResponse;
            return Ok(responseEnvio);
        }

        // PUT: api/PizarraMarcaEnvio/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Puttbl_PizarraMarcaEnvio(int id, tbl_PizarraMarcaEnvio tbl_PizarraMarcaEnvio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_PizarraMarcaEnvio.nIdPizarraMarcaEnvio)
            {
                return BadRequest();
            }

            db.Entry(tbl_PizarraMarcaEnvio).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_PizarraMarcaEnvioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/PizarraMarcaEnvio
        [ResponseType(typeof(tbl_PizarraMarcaEnvio))]
        public async Task<IHttpActionResult> Posttbl_PizarraMarcaEnvio(tbl_PizarraMarcaEnvio tbl_PizarraMarcaEnvio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_PizarraMarcaEnvio.Add(tbl_PizarraMarcaEnvio);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tbl_PizarraMarcaEnvio.nIdPizarraMarcaEnvio }, tbl_PizarraMarcaEnvio);
        }

        // DELETE: api/PizarraMarcaEnvio/5
        [ResponseType(typeof(tbl_PizarraMarcaEnvio))]
        public async Task<IHttpActionResult> Deletetbl_PizarraMarcaEnvio(int id)
        {
            tbl_PizarraMarcaEnvio tbl_PizarraMarcaEnvio = await db.tbl_PizarraMarcaEnvio.FindAsync(id);
            if (tbl_PizarraMarcaEnvio == null)
            {
                return NotFound();
            }

            db.tbl_PizarraMarcaEnvio.Remove(tbl_PizarraMarcaEnvio);
            await db.SaveChangesAsync();

            return Ok(tbl_PizarraMarcaEnvio);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_PizarraMarcaEnvioExists(int id)
        {
            return db.tbl_PizarraMarcaEnvio.Count(e => e.nIdPizarraMarcaEnvio == id) > 0;
        }
    }
}