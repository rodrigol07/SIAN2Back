namespace SianApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.Serialization;

    [Table("AAGR.tbl_PizarraMarca")]
    public partial class tbl_PizarraMarca
    {
        [Key]
        [Column(Order = 0)]
        public int nIdPizarraMarca { get; set; }

        public int nIndexSybase { get; set; }

        [Required]
        [StringLength(50)]
        public string sMenuSybase { get; set; }

        [Required]
        [StringLength(100)]
        public string sMarca { get; set; }

        [Required]
        [StringLength(100)]
        public string sAgregador { get; set; }

        public int? nIdAgregador { get; set; }

        public int? nIdEmpresa { get; set; }

        [Required]
        [StringLength(255)]
        public string sDescripcion { get; set; }

        [StringLength(255)]
        public string sUrlQaAgregador { get; set; }

        public DateTime? dUltimaDescarga { get; set; }

        public DateTime? dUltimaPublicacionQa { get; set; }

        public DateTime? dUltimaPublicacionPrd { get; set; }

        [StringLength(255)]
        public string sPublicadoPrd { get; set; }

        public int sEstado { get; set; }

        public DateTime dCreatedAt { get; set; }

        public virtual tbl_Agregador tbl_Agregador { get; set; }

        public virtual tbl_Empresa tbl_Empresa { get; set; }
    }
}
