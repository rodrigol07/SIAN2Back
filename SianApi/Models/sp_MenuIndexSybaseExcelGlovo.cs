using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SianApi.Models
{
    public class sp_MenuIndexSybaseExcelGlovo
    {
        public string Menu { get; set; }
        public string SuperCollection { get; set; }
        public string Collection { get; set; }
        public string Seccion { get; set; }
        public Int16 OrderPos { get; set; }
        public Int16 ProdPos { get; set; }
        public string CodigoPadre { get; set; }
        public string ProductoPadre { get; set; }
        public decimal PrecioPadre { get; set; }
        public int OrdenPregunta { get; set; }
        public string CodigoPregunta { get; set; }
        public string Pregunta { get; set; }
        public Int16 Minimo { get; set; }
        public Int16 Maximo { get; set; }
        public Int16 OrdenRespuesta { get; set; }
        public string CodigoRespuesta { get; set; }
        public string Respuesta { get; set; }
        public decimal PrecioRespuesta { get; set; }
        public string DescripcionProductoPadreGlovo { get; set; }
        public string ImagenGlovo { get; set; }
        public DateTime VigenciaFechaInicio { get; set; }
        public DateTime VigenciaFechaFin { get; set; }
    }
}