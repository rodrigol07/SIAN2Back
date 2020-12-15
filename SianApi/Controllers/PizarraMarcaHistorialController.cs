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
    public class PizarraMarcaHistorialController : ApiController
    {
        private SianModel db = new SianModel();

        // GET: api/PizarraMarcaHistorial
        public IQueryable<tbl_PizarraMarcaHistorial> Gettbl_PizarraMarcaHistorial()
        {
            return db.tbl_PizarraMarcaHistorial;
        }

        // GET: api/PizarraMarcaHistorial/Ultimos
        [HttpGet]
        [Route("api/PizarraMarcaHistorial/ultimos")]
        [ResponseType(typeof(tbl_PizarraMarcaHistorial))]
        public async Task<IHttpActionResult> Gettbl_PizarraMarcaHistorialUltimos()
        {
            IEnumerable<tbl_PizarraMarcaHistorial> tbl_PizarraMarcaHistorial = await db.tbl_PizarraMarcaHistorial.OrderByDescending(p => p.nIdPizarraMarcaHistorial).Take(30).ToListAsync();
            return Ok(tbl_PizarraMarcaHistorial);
        }

        // GET: api/PizarraMarcaHistorial/5
        [ResponseType(typeof(tbl_PizarraMarcaHistorial))]
        public async Task<IHttpActionResult> Gettbl_PizarraMarcaHistorial(int id)
        {
            tbl_PizarraMarcaHistorial tbl_PizarraMarcaHistorial = await db.tbl_PizarraMarcaHistorial.FindAsync(id);
            if (tbl_PizarraMarcaHistorial == null)
            {
                return NotFound();
            }

            return Ok(tbl_PizarraMarcaHistorial);
        }

        // PUT: api/PizarraMarcaHistorial/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Puttbl_PizarraMarcaHistorial(int id, tbl_PizarraMarcaHistorial tbl_PizarraMarcaHistorial)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_PizarraMarcaHistorial.nIdPizarraMarcaHistorial)
            {
                return BadRequest();
            }

            db.Entry(tbl_PizarraMarcaHistorial).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_PizarraMarcaHistorialExists(id))
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

        // POST: api/PizarraMarcaHistorial
        [ResponseType(typeof(tbl_PizarraMarcaHistorial))]
        public async Task<IHttpActionResult> Posttbl_PizarraMarcaHistorial(tbl_PizarraMarcaHistorial tbl_PizarraMarcaHistorial)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_PizarraMarcaHistorial.Add(tbl_PizarraMarcaHistorial);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tbl_PizarraMarcaHistorial.nIdPizarraMarcaHistorial }, tbl_PizarraMarcaHistorial);
        }

        // DELETE: api/PizarraMarcaHistorial/5
        [ResponseType(typeof(tbl_PizarraMarcaHistorial))]
        public async Task<IHttpActionResult> Deletetbl_PizarraMarcaHistorial(int id)
        {
            tbl_PizarraMarcaHistorial tbl_PizarraMarcaHistorial = await db.tbl_PizarraMarcaHistorial.FindAsync(id);
            if (tbl_PizarraMarcaHistorial == null)
            {
                return NotFound();
            }

            db.tbl_PizarraMarcaHistorial.Remove(tbl_PizarraMarcaHistorial);
            await db.SaveChangesAsync();

            return Ok(tbl_PizarraMarcaHistorial);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_PizarraMarcaHistorialExists(int id)
        {
            return db.tbl_PizarraMarcaHistorial.Count(e => e.nIdPizarraMarcaHistorial == id) > 0;
        }
    }
}