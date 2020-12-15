using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SianApi.Models
{
    public class sp_UsuarioLogin
    {
        public int IdUsuario { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Nombres { get; set; }
        public string Nombre { get; set; }
        public string Login { get; set; }
        public string Clave { get; set; }
        public string Email { get; set; }
        public int UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}