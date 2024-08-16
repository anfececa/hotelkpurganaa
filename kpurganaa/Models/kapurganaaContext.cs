using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace kpurganaa.Models
{
    public partial class kapurganaaContext : DbContext
    {
        public kapurganaaContext()
        {
        }

        public kapurganaaContext(DbContextOptions<kapurganaaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Habitacione> Habitaciones { get; set; } = null!;
        public virtual DbSet<Paquete> Paquetes { get; set; } = null!;
        public virtual DbSet<PaquetesServicio> PaquetesServicios { get; set; } = null!;
        public virtual DbSet<Permiso> Permisos { get; set; } = null!;
        public virtual DbSet<Persona> Personas { get; set; } = null!;
        public virtual DbSet<Reserva> Reservas { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<RolesPermiso> RolesPermisos { get; set; } = null!;
        public virtual DbSet<Servicio> Servicios { get; set; } = null!;
        public virtual DbSet<TiposHabitacione> TiposHabitaciones { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Habitacione>(entity =>
            {
                entity.HasKey(e => e.IdHabitacion);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .UseCollation("Modern_Spanish_CI_AS");

                entity.Property(e => e.EstadoHabitacion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .UseCollation("Modern_Spanish_CI_AS");

                entity.Property(e => e.NorHabitacion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .UseCollation("Modern_Spanish_CI_AS");

                entity.Property(e => e.PrecioHabitacion).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.IdTipoHabitacionNavigation)
                    .WithMany(p => p.Habitaciones)
                    .HasForeignKey(d => d.IdTipoHabitacion)
                    .HasConstraintName("FK_Habitaciones_TiposHabitaciones");
            });

            modelBuilder.Entity<Paquete>(entity =>
            {
                entity.HasKey(e => e.IdPaquete);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .UseCollation("Modern_Spanish_CI_AS");

                entity.Property(e => e.Estado)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .UseCollation("Modern_Spanish_CI_AS");

                entity.Property(e => e.NombrePaquete)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .UseCollation("Modern_Spanish_CI_AS");

                entity.Property(e => e.PrecioTotal).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.IdHabitacionNavigation)
                    .WithMany(p => p.Paquetes)
                    .HasForeignKey(d => d.IdHabitacion)
                    .HasConstraintName("FK_Paquetes_Habitaciones");
            });

            modelBuilder.Entity<PaquetesServicio>(entity =>
            {
                entity.HasKey(e => e.IdPaquetesServicios);

                entity.HasOne(d => d.IdPaqueteNavigation)
                    .WithMany(p => p.PaquetesServicios)
                    .HasForeignKey(d => d.IdPaquete)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PaquetesServicios_Paquetes");

                entity.HasOne(d => d.IdServicioNavigation)
                    .WithMany(p => p.PaquetesServicios)
                    .HasForeignKey(d => d.IdServicio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PaquetesServicios_Servicios");
            });

            modelBuilder.Entity<Permiso>(entity =>
            {
                entity.HasKey(e => e.IdPermisos);

                entity.Property(e => e.EstadoPermisos)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .UseCollation("Modern_Spanish_CI_AS");

                entity.Property(e => e.NombrePermiso)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .UseCollation("Modern_Spanish_CI_AS");
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.HasKey(e => e.IdPersonas);

                entity.Property(e => e.Apellidos)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .UseCollation("Modern_Spanish_CI_AS");

                entity.Property(e => e.Celular)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .UseCollation("Modern_Spanish_CI_AS");

                entity.Property(e => e.FechaNacimiento).HasColumnType("date");

                entity.Property(e => e.Nombres)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .UseCollation("Modern_Spanish_CI_AS");

                entity.Property(e => e.NroDocumento)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .UseCollation("Modern_Spanish_CI_AS");

                entity.Property(e => e.TipoDocumento)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .UseCollation("Modern_Spanish_CI_AS");
            });

            modelBuilder.Entity<Reserva>(entity =>
            {
                entity.HasKey(e => e.IdReserva);

                entity.Property(e => e.Abono).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.EstadoReserva)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .UseCollation("Modern_Spanish_CI_AS");

                entity.Property(e => e.FechaFin).HasColumnType("datetime");

                entity.Property(e => e.FechaInicio).HasColumnType("datetime");

                entity.Property(e => e.FechaReserva).HasColumnType("datetime");

                entity.Property(e => e.MontoTotal).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.IdPaqueteNavigation)
                    .WithMany(p => p.Reservas)
                    .HasForeignKey(d => d.IdPaquete)
                    .HasConstraintName("FK_Reservas_Paquetes");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Reservas)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_Reservas_Usuarios");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRol);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .UseCollation("Modern_Spanish_CI_AS");
            });

            modelBuilder.Entity<RolesPermiso>(entity =>
            {
                entity.HasKey(e => e.IdRolesPermisos);

                entity.Property(e => e.Estado)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .UseCollation("Modern_Spanish_CI_AS");

                entity.HasOne(d => d.IdPermisosNavigation)
                    .WithMany(p => p.RolesPermisos)
                    .HasForeignKey(d => d.IdPermisos)
                    .HasConstraintName("FK_RolesPermisos_Permisos");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.RolesPermisos)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("FK_RolesPermisos_Roles");
            });

            modelBuilder.Entity<Servicio>(entity =>
            {
                entity.HasKey(e => e.IdServicio)
                    .HasName("PK_Servicio");

                entity.Property(e => e.DescripcionServicio)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .UseCollation("Modern_Spanish_CI_AS");

                entity.Property(e => e.EstadoServicio)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .UseCollation("Modern_Spanish_CI_AS");

                entity.Property(e => e.NombreServicio)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .UseCollation("Modern_Spanish_CI_AS");

                entity.Property(e => e.PrecioServicio).HasColumnType("decimal(10, 2)");
            });

            modelBuilder.Entity<TiposHabitacione>(entity =>
            {
                entity.HasKey(e => e.IdTipoHabitacion)
                    .HasName("PK_TipoHabitaciones");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .UseCollation("Modern_Spanish_CI_AS");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .UseCollation("Modern_Spanish_CI_AS");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);

                entity.Property(e => e.ClaveUsuario)
                    .HasMaxLength(50)
                    .UseCollation("Modern_Spanish_CI_AS");

                entity.Property(e => e.CorreoUsuario)
                    .HasMaxLength(50)
                    .UseCollation("Modern_Spanish_CI_AS");

                entity.HasOne(d => d.IdPersonasNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdPersonas)
                    .HasConstraintName("FK_Usuarios_Personas");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("FK_Usuarios_Roles");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
