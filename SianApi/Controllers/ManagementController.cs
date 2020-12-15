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
    public class ManagementController : ApiController
    {
        private SianModel db = new SianModel();

        // POST: api/management/descargapizarradetalle
        [HttpPost]
        [Route("api/management/descargapizarradetalle/")]
        public async Task<IHttpActionResult> Post_DescargaPizarraDetalle(tbl_PizarraMarca pizarraMarca)
        {
            using (db)
            {
                db.Database.CommandTimeout = 180;

                try
                {
                    if (pizarraMarca.nIndexSybase == 0 || pizarraMarca.sMarca == null)
                    {
                        return BadRequest();
                    }
                    else
                    {
                        var response = await db.Database.ExecuteSqlCommandAsync("EXEC [AAGR].[SP_ins_PizarraMarcaDetalleSybase] @indexSybase, @marca", new SqlParameter("@indexSybase", pizarraMarca.nIndexSybase), new SqlParameter("@marca", pizarraMarca.sMarca));
                        return Ok(response);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        // POST: api/management/tiendaEnvioPrd
        [HttpPost]
        [Route("api/management/tiendaEnvioPrd/")]
        public async Task<IHttpActionResult> Post_TiendaEnvioPrd([FromBody]JObject tiendasEnvio)
        {
            string key = null;
            string value = null;

            using (db)
            {
                if (tiendasEnvio == null)
                {
                    return BadRequest();
                }
                else
                {
                    var tiendas = new List<int>();
                    foreach (var t in tiendasEnvio)
                    {
                        if(t.Value.ToString() == "True")
                        {
                            key = t.Key.ToString().Replace("tienda-", "");
                            value = t.Value.ToString();
                            tiendas.Add(int.Parse(key));
                        }
                    }

                    var tiendasEnvioPrd = new List<sp_TiendaEnvioPrd>();
                    foreach(int a in tiendas)
                    {
                        try
                        {
                            sp_TiendaEnvioPrd tiendaEnvioPrd = await db.Database.SqlQuery<sp_TiendaEnvioPrd>("EXEC [AAGR].[SP_sel_TiendaEnvioPrd] @id", new SqlParameter("@id", a.ToString())).FirstOrDefaultAsync();
                            if (tiendaEnvioPrd != null)
                            {
                                tiendasEnvioPrd.Add(tiendaEnvioPrd);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                    return Ok(tiendasEnvioPrd);
                }
            }
        }
    }
}
