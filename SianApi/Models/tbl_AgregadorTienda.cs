namespace SianApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.Serialization;

    [Table("AAGR.tbl_AgregadorTienda")]
    public partial class tbl_AgregadorTienda
    {
        [Key]
        [Column(Order = 0)]
        public int nIdAgregadorTienda { get; set; }

        public int nIdAgregador { get; set; }

        public int nIdEmpresa { get; set; }

        [Required]
        [StringLength(20)]
        public string nIdTiendaSap { get; set; }

        [StringLength(255)]
        public string sNombreTienda { get; set; }

        [StringLength(500)]
        public string sIdTiendaUber { get; set; }

        [StringLength(500)]
        public string sUrlAgregador { get; set; }

        public int nEstado { get; set; }

        public int nUsuarioCreacion { get; set; }

        public DateTime dFechaCreacion { get; set; }

        public int nUsuarioModificacion { get; set; }

        public DateTime dFechaModificacion { get; set; }

        public virtual tbl_Agregador tbl_Agregador { get; set; }

        public virtual tbl_Empresa tbl_Empresa { get; set; }
    }
}
