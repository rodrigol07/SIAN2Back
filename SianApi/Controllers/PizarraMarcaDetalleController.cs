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
    public class PizarraMarcaDetalleController : ApiController
    {
        private SianModel db = new SianModel();

        // GET: api/PizarraMarcaDetalle
        public IQueryable<tbl_PizarraMarcaDetalle> Gettbl_PizarraMarcaDetalle()
        {
            return db.tbl_PizarraMarcaDetalle;
        }

        //// GET: api/PizarraMarcaDetalle/5
        //[ResponseType(typeof(tbl_PizarraMarcaDetalle))]
        //public async Task<IHttpActionResult> Gettbl_PizarraMarcaDetalle(int id)
        //{
        //    tbl_PizarraMarcaDetalle tbl_PizarraMarcaDetalle = await db.tbl_PizarraMarcaDetalle.FindAsync(id);
        //    if (tbl_PizarraMarcaDetalle == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(tbl_PizarraMarcaDetalle);
        //}

        // GET: api/PizarraMarcaDetalle/{indexSybase}
        [HttpGet]
        [ResponseType(typeof(tbl_PizarraMarcaDetalle))]
        [Route("api/PizarraMarcaDetalle/{indexSybase}")]
        public async Task<IHttpActionResult> Gettbl_PizarraMarcaDetalle(int indexSybase)
        {
            IEnumerable<tbl_PizarraMarcaDetalle> pizarraMarcaDetalle = await db.tbl_PizarraMarcaDetalle.Where(x => x.nIndexSybase == indexSybase).ToListAsync();
            return Ok(pizarraMarcaDetalle);
        }

        // PUT: api/PizarraMarcaDetalle/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Puttbl_PizarraMarcaDetalle(int id, tbl_PizarraMarcaDetalle tbl_PizarraMarcaDetalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_PizarraMarcaDetalle.nIdPizarraMarcaDetalle)
            {
                return BadRequest();
            }

            db.Entry(tbl_PizarraMarcaDetalle).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_PizarraMarcaDetalleExists(id))
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

        // POST: api/PizarraMarcaDetalle
        [ResponseType(typeof(tbl_PizarraMarcaDetalle))]
        public async Task<IHttpActionResult> Posttbl_PizarraMarcaDetalle(tbl_PizarraMarcaDetalle tbl_PizarraMarcaDetalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_PizarraMarcaDetalle.Add(tbl_PizarraMarcaDetalle);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tbl_PizarraMarcaDetalle.nIdPizarraMarcaDetalle }, tbl_PizarraMarcaDetalle);
        }

        // DELETE: api/PizarraMarcaDetalle/5
        [ResponseType(typeof(tbl_PizarraMarcaDetalle))]
        public async Task<IHttpActionResult> Deletetbl_PizarraMarcaDetalle(int id)
        {
            tbl_PizarraMarcaDetalle tbl_PizarraMarcaDetalle = await db.tbl_PizarraMarcaDetalle.FindAsync(id);
            if (tbl_PizarraMarcaDetalle == null)
            {
                return NotFound();
            }

            db.tbl_PizarraMarcaDetalle.Remove(tbl_PizarraMarcaDetalle);
            await db.SaveChangesAsync();

            return Ok(tbl_PizarraMarcaDetalle);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_PizarraMarcaDetalleExists(int id)
        {
            return db.tbl_PizarraMarcaDetalle.Count(e => e.nIdPizarraMarcaDetalle == id) > 0;
        }
    }
}