namespace SianApi.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SianModel : DbContext
    {
        public SianModel()
            : base("name=SianModel")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<tbl_Agregador> tbl_Agregador { get; set; }
        public virtual DbSet<tbl_AgregadorHorario> tbl_AgregadorHorario { get; set; }
        public virtual DbSet<tbl_AgregadorTienda> tbl_AgregadorTienda { get; set; }
        public virtual DbSet<tbl_AgregadorTiendaHorario> tbl_AgregadorTiendaHorario { get; set; }
        public virtual DbSet<tbl_Empresa> tbl_Empresa { get; set; }
        public virtual DbSet<tbl_PizarraMarca> tbl_PizarraMarca { get; set; }
        public virtual DbSet<tbl_PizarraMarcaDetalle> tbl_PizarraMarcaDetalle { get; set; }
        public virtual DbSet<tbl_PizarraMarcaEnvio> tbl_PizarraMarcaEnvio { get; set; }
        public virtual DbSet<tbl_PizarraMarcaHistorial> tbl_PizarraMarcaHistorial { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tbl_Agregador>()
                .Property(e => e.sCodigo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Agregador>()
                .Property(e => e.sNombre)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Agregador>()
                .Property(e => e.sEstado)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Agregador>()
                .HasMany(e => e.tbl_AgregadorTienda)
                .WithRequired(e => e.tbl_Agregador)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_AgregadorHorario>()
                .Property(e => e.sDiaNumero)
                .IsFixedLength();

            modelBuilder.Entity<tbl_AgregadorHorario>()
                .Property(e => e.sDiaNombre)
                .IsFixedLength();

            modelBuilder.Entity<tbl_AgregadorHorario>()
                .Property(e => e.sHoraInicio)
                .IsFixedLength();

            modelBuilder.Entity<tbl_AgregadorHorario>()
                .Property(e => e.sHoraFin)
                .IsFixedLength();

            modelBuilder.Entity<tbl_AgregadorHorario>()
                .Property(e => e.sEstado)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_AgregadorTienda>()
                .Property(e => e.nIdTiendaSap)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_AgregadorTienda>()
                .Property(e => e.sIdTiendaUber)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_AgregadorTienda>()
                .Property(e => e.sUrlAgregador)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_AgregadorTiendaHorario>()
                .Property(e => e.nIdAgregadorTienda)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_AgregadorTiendaHorario>()
                .Property(e => e.sEstado)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Empresa>()
                .Property(e => e.sCodigo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Empresa>()
                .Property(e => e.sRuc)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Empresa>()
                .Property(e => e.sNombre)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Empresa>()
                .Property(e => e.sNombreComercial)
                .IsUnicode(false);

            //modelBuilder.Entity<tbl_Empresa>()
            //    .Property(e => e.sEmail)
            //    .IsUnicode(false);

            //modelBuilder.Entity<tbl_Empresa>()
            //    .Property(e => e.sTelefono)
            //    .IsUnicode(false);

            //modelBuilder.Entity<tbl_Empresa>()
            //    .Property(e => e.sFax)
            //    .IsUnicode(false);

            //modelBuilder.Entity<tbl_Empresa>()
            //    .Property(e => e.sDireccion)
            //    .IsUnicode(false);

            //modelBuilder.Entity<tbl_Empresa>()
            //    .Property(e => e.sColor)
            //    .IsUnicode(false);

            //modelBuilder.Entity<tbl_Empresa>()
            //    .Property(e => e.sEmpresaPixel)
            //    .IsUnicode(false);

            //modelBuilder.Entity<tbl_Empresa>()
            //    .Property(e => e.sGrupoCorporativo)
            //    .IsFixedLength();

            //modelBuilder.Entity<tbl_Empresa>()
            //    .Property(e => e.sEstadoUso)
            //    .IsFixedLength()
            //    .IsUnicode(false);

            //modelBuilder.Entity<tbl_Empresa>()
            //    .Property(e => e.sEmpresaSAP)
            //    .IsUnicode(false);

            //modelBuilder.Entity<tbl_Empresa>()
            //    .Property(e => e.sAbreviado)
            //    .IsUnicode(false);

            modelBuilder.Entity<tbl_Empresa>()
                .HasMany(e => e.tbl_AgregadorTienda)
                .WithRequired(e => e.tbl_Empresa)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_PizarraMarca>()
                .Property(e => e.sMenuSybase)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_PizarraMarca>()
                .Property(e => e.sMarca)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_PizarraMarca>()
                .Property(e => e.sAgregador)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_PizarraMarca>()
                .Property(e => e.sDescripcion)
                .IsUnicode(false);

            //modelBuilder.Entity<tbl_PizarraMarca>()
            //    .Property(e => e.sPublicadoPrd)
            //    .IsUnicode(false);

            modelBuilder.Entity<tbl_PizarraMarcaDetalle>()
                .Property(e => e.sMenu)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_PizarraMarcaDetalle>()
                .Property(e => e.sOrdenMenu)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_PizarraMarcaDetalle>()
                .Property(e => e.sCodigoPadre)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_PizarraMarcaDetalle>()
                .Property(e => e.sProductoPadre)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_PizarraMarcaDetalle>()
                .Property(e => e.nPrecioPadre)
                .HasPrecision(10, 4);

            modelBuilder.Entity<tbl_PizarraMarcaDetalle>()
                .Property(e => e.sCodigoPregunta)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_PizarraMarcaDetalle>()
                .Property(e => e.sPregunta)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_PizarraMarcaDetalle>()
                .Property(e => e.sCodigoRespuesta)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_PizarraMarcaDetalle>()
                .Property(e => e.sRespuesta)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_PizarraMarcaDetalle>()
                .Property(e => e.nPrecioRespuesta)
                .HasPrecision(10, 4);

            modelBuilder.Entity<tbl_PizarraMarcaDetalle>()
                .Property(e => e.sDescripcionProductoPadreUber)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_PizarraMarcaDetalle>()
                .Property(e => e.sDescripcionProductoPadreGlovo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_PizarraMarcaDetalle>()
                .Property(e => e.sImagenUber)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_PizarraMarcaDetalle>()
                .Property(e => e.sImagenGlovo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_PizarraMarcaEnvio>()
                .Property(e => e.sMenuSybase)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_PizarraMarcaEnvio>()
                .Property(e => e.sMarca)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_PizarraMarcaEnvio>()
                .Property(e => e.sAgregador)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_PizarraMarcaEnvio>()
                .Property(e => e.sStatus)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_PizarraMarcaEnvio>()
                .Property(e => e.sAprobado)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_PizarraMarcaEnvio>()
                .Property(e => e.sCreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_PizarraMarcaHistorial>()
                .Property(e => e.sMenuSybase)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_PizarraMarcaHistorial>()
                .Property(e => e.sEvento)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_PizarraMarcaHistorial>()
                .Property(e => e.sDescripcion)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_PizarraMarcaHistorial>()
                .Property(e => e.sCreatedBy)
                .IsUnicode(false);
        }
    }
}
