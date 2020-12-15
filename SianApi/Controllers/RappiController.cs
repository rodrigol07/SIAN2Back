using Newtonsoft.Json.Linq;
using SianApi.Librerias.Rappi;
using SianApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace SianApi.Controllers
{
    public class RappiController : ApiController
    {
        private SianModel db = new SianModel();
        RappiExcel rappiGenerator = new RappiExcel();

        // GET: api/rappi/excelgenerate/22066
        [HttpGet]
        [Route("api/rappi/excelgenerate/{indexSybase}")]
        [ResponseType(typeof(JObject))]
        public async Task<IHttpActionResult> Get_ExcelGenerate(int indexSybase)
        {
            string marca;
            string marcaName;
            string filename;
            string path;
            string agregador;
            string agregadorName;
            JObject json;
            string fecha = DateTime.Now.ToString("ddMMyyyy-HHmmss");

            using (db)
            {
                db.Database.CommandTimeout = 180;
                tbl_PizarraMarca pizarraMarca = await db.tbl_PizarraMarca.Where(x => x.nIndexSybase == indexSybase).FirstOrDefaultAsync();
                List<tbl_PizarraMarcaDetalle> pizarraMarcaDetalle = await db.tbl_PizarraMarcaDetalle.Where(x => x.nIndexSybase == indexSybase).ToListAsync();

                if (pizarraMarca == null)
                {
                    return NotFound();
                }

                if (pizarraMarcaDetalle.Count() == 0)
                {
                    return BadRequest("No se puede generar Excel sin elementos");
                }

                marca = pizarraMarca.sMarca;
                marcaName = marca.Trim().ToLower().Replace(" ", "");
                agregador = pizarraMarca.sAgregador;
                agregadorName = agregador.ToLower();

                if (agregador != "Rappi")
                {
                    return BadRequest("Pizarra no pertenece a agregador Rappi");
                }

                try
                {
                    filename = indexSybase + "-" + marcaName + "-" + agregadorName + "-" + fecha + ".xls";
                    path = @"C:\Agregadores\" + filename;
                    rappiGenerator.generarExcel(path, marca, pizarraMarcaDetalle);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                json = (
                    new JObject(
                        new JProperty("indexSybase", indexSybase),
                        new JProperty("marca", marca),
                        new JProperty("agregador", agregador),
                        new JProperty("filename", filename),
                        // new JProperty("path", path),
                        new JProperty("datetime", fecha),
                        new JProperty("message", "Archivo excel generado correctamente para agregador Rappi")
                    )
                );

                return Ok(json);
            }
        }
    }
}
