namespace SianApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.Serialization;

    [Table("AAGR.tbl_Agregador")]
    public partial class tbl_Agregador
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Agregador()
        {
            tbl_AgregadorTienda = new HashSet<tbl_AgregadorTienda>();
            tbl_PizarraMarca = new HashSet<tbl_PizarraMarca>();
        }

        [Key]
        public int nIdAgregador { get; set; }

        [StringLength(20)]
        public string sCodigo { get; set; }

        [StringLength(150)]
        public string sNombre { get; set; }

        [StringLength(1)]
        public string sEstado { get; set; }

        public int? nUsuarioCreacion { get; set; }

        public DateTime? dFechaCreacion { get; set; }

        public int? nUsuarioModificacion { get; set; }

        public DateTime? dFechaModificacion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_AgregadorTienda> tbl_AgregadorTienda { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_PizarraMarca> tbl_PizarraMarca { get; set; }
    }
}
