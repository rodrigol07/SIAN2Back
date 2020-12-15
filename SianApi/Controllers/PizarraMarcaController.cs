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
    public class PizarraMarcaController : ApiController
    {
        private SianModel db = new SianModel();

        // GET: api/PizarraMarca
        public IQueryable<tbl_PizarraMarca> Gettbl_PizarraMarca()
        {
            return db.tbl_PizarraMarca;
        }

        // GET: api/PizarraMarca/5
        [ResponseType(typeof(tbl_PizarraMarca))]
        public async Task<IHttpActionResult> Gettbl_PizarraMarca(int id)
        {
            tbl_PizarraMarca tbl_PizarraMarca = await db.tbl_PizarraMarca.FindAsync(id);
            if (tbl_PizarraMarca == null)
            {
                return NotFound();
            }

            return Ok(tbl_PizarraMarca);
        }

        // PUT: api/PizarraMarca/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Puttbl_PizarraMarca(int id, tbl_PizarraMarca tbl_PizarraMarca)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_PizarraMarca.nIdPizarraMarca)
            {
                return BadRequest();
            }

            db.Entry(tbl_PizarraMarca).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_PizarraMarcaExists(id))
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

        // POST: api/PizarraMarca
        [ResponseType(typeof(tbl_PizarraMarca))]
        public async Task<IHttpActionResult> Posttbl_PizarraMarca(tbl_PizarraMarca tbl_PizarraMarca)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_PizarraMarca.Add(tbl_PizarraMarca);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tbl_PizarraMarca.nIdPizarraMarca }, tbl_PizarraMarca);
        }

        // DELETE: api/PizarraMarca/5
        [ResponseType(typeof(tbl_PizarraMarca))]
        public async Task<IHttpActionResult> Deletetbl_PizarraMarca(int id)
        {
            tbl_PizarraMarca tbl_PizarraMarca = await db.tbl_PizarraMarca.FindAsync(id);
            if (tbl_PizarraMarca == null)
            {
                return NotFound();
            }

            db.tbl_PizarraMarca.Remove(tbl_PizarraMarca);
            await db.SaveChangesAsync();

            return Ok(tbl_PizarraMarca);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_PizarraMarcaExists(int id)
        {
            return db.tbl_PizarraMarca.Count(e => e.nIdPizarraMarca == id) > 0;
        }
    }
}