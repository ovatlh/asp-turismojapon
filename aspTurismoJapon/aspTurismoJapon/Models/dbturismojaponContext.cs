using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace aspTurismoJapon.Models
{
    public partial class dbturismojaponContext : DbContext
    {
        public dbturismojaponContext()
        {
        }

        public dbturismojaponContext(DbContextOptions<dbturismojaponContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Atracciones> Atracciones { get; set; }
        public virtual DbSet<Ciudades> Ciudades { get; set; }
        public virtual DbSet<Comidas> Comidas { get; set; }
        public virtual DbSet<Tipoatraccion> Tipoatraccion { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=localhost; user id=root; password=root; database=dbturismojapon;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Atracciones>(entity =>
            {
                entity.ToTable("atracciones");

                entity.HasIndex(e => e.IdCiudad)
                    .HasName("fk_Atracciones_Ciudades1_idx");

                entity.HasIndex(e => e.IdTipo)
                    .HasName("fk_Atracciones_TipoAtraccion1_idx");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Contenido)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.IdCiudad).HasColumnType("int(11)");

                entity.Property(e => e.IdTipo).HasColumnType("int(11)");

                entity.Property(e => e.Portada).HasColumnType("varchar(45)");

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasColumnType("varchar(45)");

                entity.HasOne(d => d.IdCiudadNavigation)
                    .WithMany(p => p.Atracciones)
                    .HasForeignKey(d => d.IdCiudad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Atracciones_Ciudades1");

                entity.HasOne(d => d.IdTipoNavigation)
                    .WithMany(p => p.Atracciones)
                    .HasForeignKey(d => d.IdTipo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Atracciones_TipoAtraccion1");
            });

            modelBuilder.Entity<Ciudades>(entity =>
            {
                entity.ToTable("ciudades");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Contenido)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Portada).HasColumnType("varchar(45)");
            });

            modelBuilder.Entity<Comidas>(entity =>
            {
                entity.ToTable("comidas");

                entity.HasIndex(e => e.IdCiudad)
                    .HasName("fk_Comidas_Ciudades1_idx");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.IdCiudad).HasColumnType("int(11)");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Portada).HasColumnType("varchar(45)");

                entity.HasOne(d => d.IdCiudadNavigation)
                    .WithMany(p => p.Comidas)
                    .HasForeignKey(d => d.IdCiudad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Comidas_Ciudades1");
            });

            modelBuilder.Entity<Tipoatraccion>(entity =>
            {
                entity.ToTable("tipoatraccion");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasColumnType("varchar(45)");
            });

            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.ToTable("usuarios");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Contrasena)
                    .IsRequired()
                    .HasColumnType("varchar(128)");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Rol)
                    .IsRequired()
                    .HasColumnType("varchar(20)");
            });
        }
    }
}
