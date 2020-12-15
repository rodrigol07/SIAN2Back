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
    public class EmpresaController : ApiController
    {
        private SianModel db = new SianModel();

        // GET: api/Empresa
        public IQueryable<tbl_Empresa> Gettbl_Empresa()
        {
            return db.tbl_Empresa;
        }

        // GET: api/Empresa/5
        [ResponseType(typeof(tbl_Empresa))]
        public async Task<IHttpActionResult> Gettbl_Empresa(int id)
        {
            tbl_Empresa tbl_Empresa = await db.tbl_Empresa.FindAsync(id);
            if (tbl_Empresa == null)
            {
                return NotFound();
            }

            return Ok(tbl_Empresa);
        }

        // PUT: api/Empresa/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Puttbl_Empresa(int id, tbl_Empresa tbl_Empresa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_Empresa.nIdEmpresa)
            {
                return BadRequest();
            }

            db.Entry(tbl_Empresa).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_EmpresaExists(id))
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

        // POST: api/Empresa
        [ResponseType(typeof(tbl_Empresa))]
        public async Task<IHttpActionResult> Posttbl_Empresa(tbl_Empresa tbl_Empresa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_Empresa.Add(tbl_Empresa);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (tbl_EmpresaExists(tbl_Empresa.nIdEmpresa))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tbl_Empresa.nIdEmpresa }, tbl_Empresa);
        }

        // DELETE: api/Empresa/5
        [ResponseType(typeof(tbl_Empresa))]
        public async Task<IHttpActionResult> Deletetbl_Empresa(int id)
        {
            tbl_Empresa tbl_Empresa = await db.tbl_Empresa.FindAsync(id);
            if (tbl_Empresa == null)
            {
                return NotFound();
            }

            db.tbl_Empresa.Remove(tbl_Empresa);
            await db.SaveChangesAsync();

            return Ok(tbl_Empresa);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_EmpresaExists(int id)
        {
            return db.tbl_Empresa.Count(e => e.nIdEmpresa == id) > 0;
        }
    }
}