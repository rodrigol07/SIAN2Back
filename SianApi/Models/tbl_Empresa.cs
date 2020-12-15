namespace SianApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.Serialization;

    [Table("AAGR.tbl_Empresa")]
    [KnownTypeAttribute(typeof(tbl_Empresa))]
    public partial class tbl_Empresa
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Empresa()
        {
            tbl_AgregadorTienda = new HashSet<tbl_AgregadorTienda>();
            tbl_PizarraMarca = new HashSet<tbl_PizarraMarca>();
        }

        [Key]
        public int nIdEmpresa { get; set; }

        [StringLength(20)]
        public string sCodigo { get; set; }

        [StringLength(20)]
        public string sRuc { get; set; }

        [StringLength(100)]
        public string sNombre { get; set; }

        [StringLength(100)]
        public string sNombreComercial { get; set; }

        //[StringLength(100)]
        //public string sEmail { get; set; }

        //[StringLength(100)]
        //public string sTelefono { get; set; }

        //[StringLength(20)]
        //public string sFax { get; set; }

        //[StringLength(100)]
        //public string sDireccion { get; set; }

        //[StringLength(50)]
        //public string sColor { get; set; }

        //public int? nUsuarioCreacion { get; set; }

        //public DateTime? dFechaCreacion { get; set; }

        //public int? nUsuarioModificacion { get; set; }

        //public DateTime? dFechaModificacion { get; set; }

        //public int? nCompanyPIXEL { get; set; }

        //[StringLength(20)]
        //public string sEmpresaPixel { get; set; }

        //[StringLength(100)]
        //public string sGrupoCorporativo { get; set; }

        //[StringLength(1)]
        //public string sEstadoUso { get; set; }

        //public int? nOrden { get; set; }

        //[StringLength(6)]
        //public string sEmpresaSAP { get; set; }

        //[Column(TypeName = "date")]
        //public DateTime? dFechaVivoSAP { get; set; }

        //[StringLength(20)]
        //public string sAbreviado { get; set; }

        //public int? nReporte { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_AgregadorTienda> tbl_AgregadorTienda { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_PizarraMarca> tbl_PizarraMarca { get; set; }
    }
}
