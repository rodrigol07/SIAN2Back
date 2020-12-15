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
using System.Web.Http.Description;
using SianApi.Models;

namespace SianApi.Controllers
{
    public class AgregadorController : ApiController
    {
        private SianModel db = new SianModel();

        // GET: api/Agregador
        public IQueryable<tbl_Agregador> Gettbl_Agregador()
        {
            return db.tbl_Agregador;
        }

        // GET: api/Agregador/5
        [ResponseType(typeof(tbl_Agregador))]
        public async Task<IHttpActionResult> Gettbl_Agregador(int id)
        {
            tbl_Agregador tbl_Agregador = await db.tbl_Agregador.FindAsync(id);
            if (tbl_Agregador == null)
            {
                return NotFound();
            }

            return Ok(tbl_Agregador);
        }

        // PUT: api/Agregador/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Puttbl_Agregador(int id, tbl_Agregador tbl_Agregador)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_Agregador.nIdAgregador)
            {
                return BadRequest();
            }

            db.Entry(tbl_Agregador).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_AgregadorExists(id))
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

        // POST: api/Agregador
        [ResponseType(typeof(tbl_Agregador))]
        public async Task<IHttpActionResult> Posttbl_Agregador(tbl_Agregador tbl_Agregador)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_Agregador.Add(tbl_Agregador);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tbl_Agregador.nIdAgregador }, tbl_Agregador);
        }

        // DELETE: api/Agregador/5
        [ResponseType(typeof(tbl_Agregador))]
        public async Task<IHttpActionResult> Deletetbl_Agregador(int id)
        {
            tbl_Agregador tbl_Agregador = await db.tbl_Agregador.FindAsync(id);
            if (tbl_Agregador == null)
            {
                return NotFound();
            }

            db.tbl_Agregador.Remove(tbl_Agregador);
            await db.SaveChangesAsync();

            return Ok(tbl_Agregador);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_AgregadorExists(int id)
        {
            return db.tbl_Agregador.Count(e => e.nIdAgregador == id) > 0;
        }
    }
}