namespace SianApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AAGR.tbl_AgregadorTiendaHorario")]
    public partial class tbl_AgregadorTiendaHorario
    {
        [Key]
        [Column(Order = 0)]
        public int nIdAgregadorTiendaHorario { get; set; }

        [Required]
        [StringLength(20)]
        public string nIdAgregadorTienda { get; set; }

        public int? nIdPizarraMarca { get; set; }

        public int? nGrupoHorario { get; set; }

        [Required]
        [StringLength(1)]
        public string sEstado { get; set; }

        public int? nUsuarioCreacion { get; set; }

        public DateTime? dFechaCreacion { get; set; }
    }
}
