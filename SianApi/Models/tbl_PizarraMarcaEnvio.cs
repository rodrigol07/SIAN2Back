namespace SianApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AAGR.tbl_PizarraMarcaEnvio")]
    public partial class tbl_PizarraMarcaEnvio
    {
        [Key]
        [Column(Order = 0)]
        public int nIdPizarraMarcaEnvio { get; set; }

        public int nIndexSybase { get; set; }

        [Required]
        [StringLength(50)]
        public string sMenuSybase { get; set; }

        [Required]
        [StringLength(50)]
        public string sMarca { get; set; }

        [Required]
        [StringLength(50)]
        public string sAgregador { get; set; }

        [Required]
        [StringLength(50)]
        public string sStatus { get; set; }

        [Required]
        [StringLength(50)]
        public string sAprobado { get; set; }

        [Required]
        public string sJson { get; set; }

        public string sResponse { get; set; }

        [Required]
        [StringLength(255)]
        public string sCreatedBy { get; set; }

        public DateTime dCreatedAt { get; set; }
    }
}
