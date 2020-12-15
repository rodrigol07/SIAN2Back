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
    public class AgregadorHorarioController : ApiController
    {
        private SianModel db = new SianModel();

        // GET: api/AgregadorHorario
        public IQueryable<tbl_AgregadorHorario> Gettbl_AgregadorHorario()
        {
            return db.tbl_AgregadorHorario;
        }

        // GET: api/AgregadorHorario/5
        //[ResponseType(typeof(tbl_AgregadorHorario))]
        //public async Task<IHttpActionResult> Gettbl_AgregadorHorario(int id)
        //{
        //    tbl_AgregadorHorario tbl_AgregadorHorario = await db.tbl_AgregadorHorario.FindAsync(id);
        //    if (tbl_AgregadorHorario == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(tbl_AgregadorHorario);
        //}

        // GET: api/AgregadorHorario/1
        [HttpGet]
        [ResponseType(typeof(tbl_AgregadorHorario))]
        [Route("api/AgregadorHorario/{nGrupoHorario}")]
        public async Task<IHttpActionResult> Gettbl_AgregadorHorario(int nGrupoHorario)
        {
            IEnumerable<tbl_AgregadorHorario> agregadorHorario = await db.tbl_AgregadorHorario.Where(x => x.nGrupoHorario == nGrupoHorario).ToListAsync();
            return Ok(agregadorHorario);
        }

        // PUT: api/AgregadorHorario/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Puttbl_AgregadorHorario(int id, tbl_AgregadorHorario tbl_AgregadorHorario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_AgregadorHorario.nIdAgregadorHorario)
            {
                return BadRequest();
            }

            db.Entry(tbl_AgregadorHorario).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_AgregadorHorarioExists(id))
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

        // POST: api/AgregadorHorario
        [ResponseType(typeof(tbl_AgregadorHorario))]
        public async Task<IHttpActionResult> Posttbl_AgregadorHorario(tbl_AgregadorHorario tbl_AgregadorHorario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_AgregadorHorario.Add(tbl_AgregadorHorario);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tbl_AgregadorHorario.nIdAgregadorHorario }, tbl_AgregadorHorario);
        }

        // DELETE: api/AgregadorHorario/5
        [ResponseType(typeof(tbl_AgregadorHorario))]
        public async Task<IHttpActionResult> Deletetbl_AgregadorHorario(int id)
        {
            tbl_AgregadorHorario tbl_AgregadorHorario = await db.tbl_AgregadorHorario.FindAsync(id);
            if (tbl_AgregadorHorario == null)
            {
                return NotFound();
            }

            db.tbl_AgregadorHorario.Remove(tbl_AgregadorHorario);
            await db.SaveChangesAsync();

            return Ok(tbl_AgregadorHorario);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_AgregadorHorarioExists(int id)
        {
            return db.tbl_AgregadorHorario.Count(e => e.nIdAgregadorHorario == id) > 0;
        }
    }
}