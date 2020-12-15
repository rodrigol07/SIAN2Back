using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SianApi.Models
{
    public class sp_DescargasPizarraMarca
    {
        public string Marca { get; set; }
        public int Descargas { get; set; }
    }
}