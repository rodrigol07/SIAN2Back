namespace SianApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AAGR.tbl_PizarraMarcaHistorial")]
    public partial class tbl_PizarraMarcaHistorial
    {
        [Key]
        [Column(Order = 0)]
        public int nIdPizarraMarcaHistorial { get; set; }

        public int nIndexSybase { get; set; }

        [Required]
        [StringLength(50)]
        public string sMenuSybase { get; set; }

        [Required]
        [StringLength(50)]
        public string sEvento { get; set; }

        [Required]
        public string sDescripcion { get; set; }

        [Required]
        [StringLength(255)]
        public string sCreatedBy { get; set; }

        public DateTime dCreatedAt { get; set; }
    }
}
