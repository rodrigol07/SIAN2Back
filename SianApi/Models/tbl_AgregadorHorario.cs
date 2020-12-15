namespace SianApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AAGR.tbl_AgregadorHorario")]
    public partial class tbl_AgregadorHorario
    {
        [Key]
        [Column(Order = 0)]
        public int nIdAgregadorHorario { get; set; }

        public int nGrupoHorario { get; set; }

        [Required]
        [StringLength(10)]
        public string sDiaNumero { get; set; }

        [StringLength(10)]
        public string sDiaNombre { get; set; }

        [StringLength(10)]
        public string sHoraInicio { get; set; }

        [StringLength(10)]
        public string sHoraFin { get; set; }

        [Required]
        [StringLength(1)]
        public string sEstado { get; set; }

        public int? nUsuarioCreacion { get; set; }

        public DateTime? dFechaCreacion { get; set; }

        public int? nUsuarioModificacion { get; set; }

        public DateTime? dFechaModificacion { get; set; }
    }
}
