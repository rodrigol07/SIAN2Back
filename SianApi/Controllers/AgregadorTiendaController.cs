using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using SianApi.Models;

namespace SianApi.Controllers
{
    public class AgregadorTiendaController : ApiController
    {
        private SianModel db = new SianModel();

        // GET: api/AgregadorTienda/5 //id
        // GET: api/AgregadorTienda/?id=5 //id
        // GET: api/AgregadorTienda/?empresa=7 //empresa
        // GET: api/AgregadorTienda/?agregador=1 //agregador
        [Route("api/AgregadorTienda/{id:int?}/{empresa:int?}/{agregador:int?}")]
        public async Task<IHttpActionResult> Gettbl_AgregadorTienda([FromUri]int? id = null, [FromUri]int? agregador = null, [FromUri]int? empresa = null)
        {
            if (id == null && agregador == null && empresa == null)
            {
                return Ok(db.tbl_AgregadorTienda);
            }
            else
            {
                List<tbl_AgregadorTienda> agregadorTienda = await db.tbl_AgregadorTienda.Where(x => x.nIdAgregadorTienda == id || (x.nIdAgregador == agregador && x.nIdEmpresa == empresa) && x.nEstado == 1).ToListAsync();
                return Ok(agregadorTienda);
            }
        }

        // PUT: api/AgregadorTienda/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Puttbl_AgregadorTienda(int id, tbl_AgregadorTienda tbl_AgregadorTienda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_AgregadorTienda.nIdAgregadorTienda)
            {
                return BadRequest();
            }

            db.Entry(tbl_AgregadorTienda).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_AgregadorTiendaExists(id))
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

        // POST: api/AgregadorTienda
        [ResponseType(typeof(tbl_AgregadorTienda))]
        public async Task<IHttpActionResult> Posttbl_AgregadorTienda(tbl_AgregadorTienda tbl_AgregadorTienda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_AgregadorTienda.Add(tbl_AgregadorTienda);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tbl_AgregadorTienda.nIdAgregadorTienda }, tbl_AgregadorTienda);
        }

        // DELETE: api/AgregadorTienda/5
        [ResponseType(typeof(tbl_AgregadorTienda))]
        public async Task<IHttpActionResult> Deletetbl_AgregadorTienda(int id)
        {
            tbl_AgregadorTienda tbl_AgregadorTienda = await db.tbl_AgregadorTienda.FindAsync(id);
            if (tbl_AgregadorTienda == null)
            {
                return NotFound();
            }

            db.tbl_AgregadorTienda.Remove(tbl_AgregadorTienda);
            await db.SaveChangesAsync();

            return Ok(tbl_AgregadorTienda);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_AgregadorTiendaExists(int id)
        {
            return db.tbl_AgregadorTienda.Count(e => e.nIdAgregadorTienda == id) > 0;
        }
    }
}