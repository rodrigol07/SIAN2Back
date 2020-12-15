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
    public class AgregadorTiendaHorarioController : ApiController
    {
        private SianModel db = new SianModel();

        // GET: api/AgregadorTiendaHorario
        public IQueryable<tbl_AgregadorTiendaHorario> Gettbl_AgregadorTiendaHorario()
        {
            return db.tbl_AgregadorTiendaHorario;
        }

        // GET: api/AgregadorTiendaHorario/5
        [ResponseType(typeof(tbl_AgregadorTiendaHorario))]
        public async Task<IHttpActionResult> Gettbl_AgregadorTiendaHorario(int id)
        {
            tbl_AgregadorTiendaHorario tbl_AgregadorTiendaHorario = await db.tbl_AgregadorTiendaHorario.FindAsync(id);
            if (tbl_AgregadorTiendaHorario == null)
            {
                return NotFound();
            }

            return Ok(tbl_AgregadorTiendaHorario);
        }

        // PUT: api/AgregadorTiendaHorario/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Puttbl_AgregadorTiendaHorario(int id, tbl_AgregadorTiendaHorario tbl_AgregadorTiendaHorario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_AgregadorTiendaHorario.nIdAgregadorTiendaHorario)
            {
                return BadRequest();
            }

            db.Entry(tbl_AgregadorTiendaHorario).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_AgregadorTiendaHorarioExists(id))
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

        // POST: api/AgregadorTiendaHorario
        [ResponseType(typeof(tbl_AgregadorTiendaHorario))]
        public async Task<IHttpActionResult> Posttbl_AgregadorTiendaHorario(tbl_AgregadorTiendaHorario tbl_AgregadorTiendaHorario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_AgregadorTiendaHorario.Add(tbl_AgregadorTiendaHorario);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tbl_AgregadorTiendaHorario.nIdAgregadorTiendaHorario }, tbl_AgregadorTiendaHorario);
        }

        // DELETE: api/AgregadorTiendaHorario/5
        [ResponseType(typeof(tbl_AgregadorTiendaHorario))]
        public async Task<IHttpActionResult> Deletetbl_AgregadorTiendaHorario(int id)
        {
            tbl_AgregadorTiendaHorario tbl_AgregadorTiendaHorario = await db.tbl_AgregadorTiendaHorario.FindAsync(id);
            if (tbl_AgregadorTiendaHorario == null)
            {
                return NotFound();
            }

            db.tbl_AgregadorTiendaHorario.Remove(tbl_AgregadorTiendaHorario);
            await db.SaveChangesAsync();

            return Ok(tbl_AgregadorTiendaHorario);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_AgregadorTiendaHorarioExists(int id)
        {
            return db.tbl_AgregadorTiendaHorario.Count(e => e.nIdAgregadorTiendaHorario == id) > 0;
        }
    }
}