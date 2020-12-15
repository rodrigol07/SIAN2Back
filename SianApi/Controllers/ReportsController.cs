using SianApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace SianApi.Controllers
{
    public class ReportsController : ApiController
    {
        private SianModel db = new SianModel();

        // GET: api/reports/descargaspizarramarca
        [HttpGet]
        [Route("api/reports/descargaspizarramarca")]
        [ResponseType(typeof(sp_DescargasPizarraMarca))]
        public async Task<IHttpActionResult> Get_DescargasPizarraMarca()
        {
            IEnumerable<sp_DescargasPizarraMarca> descargas;
            using (db)
            {
                descargas = await db.Database.SqlQuery<sp_DescargasPizarraMarca>("EXEC [AAGR].[SP_rep_DescargasPizarraMarca]").ToListAsync();
                return Ok(descargas);
            }
        }

        // GET: api/reports/enviosqapizarramarca
        [HttpGet]
        [Route("api/reports/enviosqapizarramarca")]
        [ResponseType(typeof(sp_EnviosQaPizarraMarca))]
        public async Task<IHttpActionResult> Get_EnviosQaPizarraMarca()
        {
            IEnumerable<sp_EnviosQaPizarraMarca> envios;
            using (db)
            {
                envios = await db.Database.SqlQuery<sp_EnviosQaPizarraMarca>("EXEC [AAGR].[SP_rep_EnviosQaPizarraMarca]").ToListAsync();
                return Ok(envios);
            }
        }

        // GET: api/reports/enviosprdpizarramarca
        [HttpGet]
        [Route("api/reports/enviosprdpizarramarca")]
        [ResponseType(typeof(sp_EnviosPrdPizarraMarca))]
        public async Task<IHttpActionResult> Get_EnviosPrdPizarraMarca()
        {
            IEnumerable<sp_EnviosPrdPizarraMarca> envios;
            using (db)
            {
                envios = await db.Database.SqlQuery<sp_EnviosPrdPizarraMarca>("EXEC [AAGR].[SP_rep_EnviosPrdPizarraMarca]").ToListAsync();
                return Ok(envios);
            }
        }
    }
}
