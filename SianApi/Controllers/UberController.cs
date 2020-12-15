using Newtonsoft.Json.Linq;
using SianApi.Librerias.Ubereats;
using SianApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace SianApi.Controllers
{
    public class UberController : ApiController
    {
        private SianModel db = new SianModel();
        UberJson uberGenerador = new UberJson();
        UberClient uberClient = new UberClient();

        // GET: api/uber/jsongenerate/22065
        [HttpGet]
        [Route("api/uber/jsongenerate/{indexSybase}")]
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
                if (pizarraMarca.sAgregador != "Uber")
                {
                    return BadRequest("Pizarra no pertenece a agregador Uber");
                }

                json = uberGenerador.generarListaJson(pizarraMarcaDetalle, marca, indexSybase);
                return Ok(json);
            }
        }

        // POST: api/uber/jsonsendqa
        [HttpPost]
        [Route("api/uber/jsonsendqa/")]
        [ResponseType(typeof(JObject))]
        public async Task<IHttpActionResult> Post_JsonSendQa([FromBody]JObject data)
        {
            int indexSybase = int.Parse(data["nIndexSybase"].ToString());
            string marca = data["sMarca"].ToString();
            // string marca = "China Wok";
            string marcaName = marca.Trim().ToLower().Replace(" ", "");
            string agregador = data["sAgregador"].ToString();
            Boolean reset = Boolean.Parse(data["reset"].ToString());
            JObject uberJson;
            string uberJsonString;

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

                if (pizarraMarca.sAgregador != "Uber")
                {
                    return BadRequest("Pizarra no pertenece a agregador Uber");
                }
                
                uberJson = uberGenerador.generarListaJson(pizarraMarcaDetalle, marca, indexSybase);
                string token = await uberClient.GetTokenAsync();

                if(reset)
                {
                    uberJsonString = "{\"sections\": []}";
                }
                else
                {
                    uberJsonString = uberJson.ToString(Newtonsoft.Json.Formatting.None);
                }
                
                HttpResponseMessage response = await uberClient.PutMenuAsync(token, uberJsonString, marca);

                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    var sqlQuery = await db.Database.ExecuteSqlCommandAsync("EXEC [AAGR].[SP_ins_PizarraMarcaEnvio] @indexSybase, @status, @json, @response", new SqlParameter("@indexSybase", indexSybase), new SqlParameter("@status", "QA"), new SqlParameter("@json", uberJsonString), new SqlParameter("@response", response.ToString()));
                    return Ok("Pizarra enviada correctamente a QA de agregador Uber");
            }
                else
            {
                return BadRequest(response.ReasonPhrase);
            }
        }
        }

        // POST: api/uber/jsonsendprd
        [HttpPost]
        [Route("api/uber/jsonsendprd/")]
        [ResponseType(typeof(JObject))]
        public async Task<IHttpActionResult> Post_JsonSendPrd([FromBody]JObject data)
        {
            int indexSybase = int.Parse(data["nIndexSybase"].ToString());
            string marca = data["sMarca"].ToString();
            string marcaName = marca.Trim().ToLower().Replace(" ", "");
            string agregador = data["sAgregador"].ToString();
            int idTienda = int.Parse(data["nIdAgregadorTienda"].ToString());
            int grupoHorario = int.Parse(data["nGrupoHorario"].ToString());
            // Boolean reset = Boolean.Parse(data["reset"].ToString());
            JObject uberJson;
            string uberJsonString;
            // string tiendaUberId;

            using (db)
            {
                db.Database.CommandTimeout = 180;
                tbl_PizarraMarca pizarraMarca = await db.tbl_PizarraMarca.Where(x => x.nIndexSybase == indexSybase).FirstOrDefaultAsync();
                List<tbl_PizarraMarcaDetalle> pizarraMarcaDetalle = await db.tbl_PizarraMarcaDetalle.Where(x => x.nIndexSybase == indexSybase).ToListAsync();

                //Grupo Horario
                IEnumerable<tbl_AgregadorHorario> agregadorHorario = await db.tbl_AgregadorHorario.Where(x => x.nGrupoHorario == grupoHorario).ToListAsync();

                if (pizarraMarca == null)
                {
                    return NotFound();
                }

                if (pizarraMarcaDetalle.Count() == 0)
                {
                    return BadRequest("No se puede generar JSON sin elementos");
                }

                if (pizarraMarca.sAgregador != "Uber")
                {
                    return BadRequest("Pizarra no pertenece a agregador Uber");
                }

                if (idTienda < 1)
                {
                    return BadRequest("Tienda es obligatorio");
                }

                tbl_AgregadorTienda tienda = await db.tbl_AgregadorTienda.Where(x => x.nIdAgregadorTienda == idTienda).FirstOrDefaultAsync();
                uberJson = uberGenerador.generarListaJson(pizarraMarcaDetalle, marca, indexSybase, agregadorHorario);
                string token = await uberClient.GetTokenAsyncPrd();

                //if (reset)
                //{
                //    uberJsonString = "{\"sections\": []}";
                //}
                //else
                //{
                //    uberJsonString = uberJson.ToString(Newtonsoft.Json.Formatting.None);
                //}

                uberJsonString = uberJson.ToString(Newtonsoft.Json.Formatting.None);
                // uberJsonString = "{\"sections\": []}";

                HttpResponseMessage response = await uberClient.PutMenuAsync(token, uberJsonString, marca, tienda.sIdTiendaUber);

                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    var sqlQuery = await db.Database.ExecuteSqlCommandAsync("EXEC [AAGR].[SP_ins_PizarraMarcaEnvio] @indexSybase, @status, @json, @response", new SqlParameter("@indexSybase", indexSybase), new SqlParameter("@status", "PRD"), new SqlParameter("@json", uberJsonString), new SqlParameter("@response", response.ToString()));
                    return Ok("Pizarra enviada correctamente a tienda PRD de agregador Uber");
                }
                else
                {
                    return BadRequest(response.ReasonPhrase);
                }
            }
        }
    }
}
