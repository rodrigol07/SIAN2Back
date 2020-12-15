using Newtonsoft.Json.Linq;
using SianApi.Librerias.Glovo;
using SianApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace SianApi.Controllers
{
    public class GlovoController : ApiController
    {
        private SianModel db = new SianModel();
        GlovoJson glovoGenerador = new GlovoJson();
        GlovoClient glovoClient = new GlovoClient();
        GlovoExcel glovoGenerator = new GlovoExcel();

        // GET: api/glovo/jsongenerate/22066
        [HttpGet]
        [Route("api/glovo/jsongenerate/{indexSybase}")]
        [ResponseType(typeof(JObject))]
        public async Task<IHttpActionResult> Get_JsonGenerate(int indexSybase)
        {
            string marca;
            string marcaName;
            JObject json;

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
                    return BadRequest("No se puede generar JSON sin elementos");
                }

                marca = pizarraMarca.sMarca;
                marcaName = marca.Trim().ToLower();
                if (pizarraMarca.sAgregador != "Glovo")
                {
                    return BadRequest("Pizarra no pertenece a agregador Glovo");
                }

                json = glovoGenerador.generarListaJson(pizarraMarcaDetalle, marca);
                return Ok(json);
            }
        }

        // GET: api/glovo/excelgenerate/22066
        [HttpGet]
        [Route("api/glovo/excelgenerate/{indexSybase}")]
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

                if (pizarraMarca == null)
                {
                    return NotFound();
                }

                marca = pizarraMarca.sMarca;
                marcaName = marca.Trim().ToLower().Replace(" ", "");
                agregador = pizarraMarca.sAgregador;
                agregadorName = agregador.ToLower();

                if (agregador != "Glovo")
                {
                    return BadRequest("Pizarra no pertenece a agregador Glovo");
                }

                List<sp_MenuIndexSybaseExcelGlovo> listaExcel = new List<sp_MenuIndexSybaseExcelGlovo>();

                switch (marca)
                {
                    case "Bembos":
                        listaExcel = await db.Database.SqlQuery<sp_MenuIndexSybaseExcelGlovo>("EXEC [AAGR].[SP_sel_MenuIndexSybase_BB_Glovo] @indexSybase, @marca", new SqlParameter("@indexSybase", indexSybase), new SqlParameter("@marca", marca)).ToListAsync();
                        break;
                    case "Popeyes":
                        listaExcel = await db.Database.SqlQuery<sp_MenuIndexSybaseExcelGlovo>("EXEC [AAGR].[SP_sel_MenuIndexSybase_PP_Glovo] @indexSybase, @marca", new SqlParameter("@indexSybase", indexSybase), new SqlParameter("@marca", marca)).ToListAsync();
                        break;
                    case "Papa Johns":
                        listaExcel = await db.Database.SqlQuery<sp_MenuIndexSybaseExcelGlovo>("EXEC [AAGR].[SP_sel_MenuIndexSybase_PJ_Glovo] @indexSybase, @marca", new SqlParameter("@indexSybase", indexSybase), new SqlParameter("@marca", marca)).ToListAsync();
                        break;
                    default:
                        return BadRequest("Funcionalidad no implementada para la marca " + marca + ".");
                }

                if (listaExcel.Count() == 0)
                {
                    return BadRequest("No se puede generar archivo excel sin elementos");
                }

                try
                {
                    filename = indexSybase + "-" + marcaName + "-" + agregadorName + "-" + fecha + ".xls";
                    path = @"C:\Agregadores\" + filename;
                    glovoGenerator.generarExcel(path, marca, listaExcel);
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
                        // new JProperty("datetime", fecha),
                        new JProperty("message", "Archivo excel generado correctamente para agregador Glovo")
                    )
                );

                return Ok(json);
            }
        }

        // POST: api/glovo/jsonsendqa
        [HttpPost]
        [Route("api/glovo/jsonsendqa/")]
        [ResponseType(typeof(JObject))]
        public async Task<IHttpActionResult> Post_JsonSendQa([FromBody]JObject data)
        {
            int indexSybase = int.Parse(data["nIndexSybase"].ToString());
            //string marca = data["sMarca"].ToString();
            string marca = "China Wok";
            string marcaName = marca.Trim().ToLower().Replace(" ", "");
            string agregador = data["sAgregador"].ToString();
            JObject glovoJson;
            string path;
            string filename;
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
                    return BadRequest("No se puede generar JSON sin elementos");
                }

                if (pizarraMarca.sAgregador != "Glovo")
                {
                    return BadRequest("Pizarra no pertenece a agregador Glovo");
                }

                glovoJson = glovoGenerador.generarListaJson(pizarraMarcaDetalle, marca);

                string glovoJsonString = glovoJson.ToString(Newtonsoft.Json.Formatting.None);

                filename = indexSybase + "-" + marcaName + "-" + agregador + "-" + fecha + ".json";
                path = @"\\10.92.5.8\menu\" + filename;
                if (!File.Exists(path))
                {
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        sw.WriteLine(glovoJsonString);
                    }
                }

                HttpResponseMessage response = await glovoClient.PostMenuAsync(marca, filename);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    try
                    {
                        var sqlQuery = await db.Database.ExecuteSqlCommandAsync("EXEC [AAGR].[SP_ins_PizarraMarcaEnvio] @indexSybase, @status, @json, @response", new SqlParameter("@indexSybase", indexSybase), new SqlParameter("@status", "QA"), new SqlParameter("@json", glovoJsonString), new SqlParameter("@response", response.ToString()));
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                return Ok("Pizarra enviada correctamente a QA de agregador Glovo");
            }
        }
    }
}
