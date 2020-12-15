using LoginHelper;
using Newtonsoft.Json.Linq;
using SianApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace SianApi.Controllers
{
    public class LoginController : ApiController
    {
        private SianModel db = new SianModel();
        JObject json;
        ClsEncriptar VbDesencriptar = new ClsEncriptar();
        string keycode;

        // POST: api/login/validausuario
        [HttpPost]
        [Route("api/login/validausuario")]
        public async Task<IHttpActionResult> Post_ValidaUsuario([FromBody]JObject data)
        {
            string dataUsername = data["username"].ToString();
            string dataPassword = data["password"].ToString();

            if(dataUsername == "" || dataPassword == "")
            {
                return BadRequest("Debe ingresar username o password");
            }

            using (db)
            {
                sp_UsuarioLogin usuario = await db.Database.SqlQuery<sp_UsuarioLogin>("EXEC [AAGR].[SP_sel_UsuarioLogin] @login", new SqlParameter("@login", dataUsername)).FirstOrDefaultAsync();

                if(usuario == null)
                {
                    return NotFound();
                }

                keycode = VbDesencriptar.Encode(dataPassword).ToString();

                if (keycode != usuario.Clave) // Si usuario no autorizado
                {
                    return BadRequest("Usuario no autorizado");
                }

                if (usuario.Login == dataUsername) // Si usuario autorizado
                {
                    json = (
                        new JObject(
                            new JProperty("usuario", usuario.Login),
                            new JProperty("nombre", usuario.Nombre),
                            new JProperty("clave", usuario.Clave),
                            new JProperty("email", usuario.Email),
                            new JProperty("fecha_creacion", usuario.FechaCreacion),
                            new JProperty("message", "Usuario validado con éxito")
                        )
                    );
                }

                return Ok(json);
            }
        }
    }
}
