using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SianApi.Models
{
    public class sp_TiendaEnvioPrd
    {
        public int nIdAgregadorTienda { get; set; }
        public int nIdAgregador { get; set; }
        public string sAgregador { get; set; }
        public int nIdEmpresa { get; set; }
        public string sNombreComercial { get; set; }
        public string nIdTiendaSap { get; set; }
        public int nIdPizarraMarca { get; set; }
        public int nIndexSybase { get; set; }
        public string sMenuSybase { get; set; }
        public DateTime dUltimaDescarga { get; set; }
        public int nGrupoHorario { get; set; }
        public string sNombreTienda { get; set; }
        public string sIdTiendaUber { get; set; }
        public string sUrlAgregador { get; set; }
        public int nEstado { get; set; }
    }
}