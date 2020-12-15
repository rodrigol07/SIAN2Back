namespace SianApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AAGR.tbl_PizarraMarcaDetalle")]
    public partial class tbl_PizarraMarcaDetalle
    {
        [Key]
        [Column(Order = 0)]
        public int nIdPizarraMarcaDetalle { get; set; }

        public int nIndexSybase { get; set; }

        [Required]
        [StringLength(50)]
        public string sMenu { get; set; }

        [Required]
        [StringLength(255)]
        public string sOrdenMenu { get; set; }

        public int nOrderPos { get; set; }

        public int nProdPos { get; set; }

        [Required]
        [StringLength(255)]
        public string sCodigoPadre { get; set; }

        [StringLength(255)]
        public string sProductoPadre { get; set; }

        public decimal? nPrecioPadre { get; set; }

        public int? nOrdenPregunta { get; set; }

        [StringLength(255)]
        public string sCodigoPregunta { get; set; }

        [StringLength(255)]
        public string sPregunta { get; set; }

        public int? nMinimo { get; set; }

        public int? nMaximo { get; set; }

        public int? nOrdenRespuesta { get; set; }

        [StringLength(255)]
        public string sCodigoRespuesta { get; set; }

        [StringLength(255)]
        public string sRespuesta { get; set; }

        public decimal? nPrecioRespuesta { get; set; }

        [StringLength(255)]
        public string sDescripcionProductoPadreUber { get; set; }

        [StringLength(255)]
        public string sDescripcionProductoPadreGlovo { get; set; }

        [StringLength(255)]
        public string sImagenUber { get; set; }

        [StringLength(255)]
        public string sImagenGlovo { get; set; }

        public DateTime dCreatedAt { get; set; }
    }
}
