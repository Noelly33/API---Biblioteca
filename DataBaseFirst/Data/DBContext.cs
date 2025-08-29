using DataBaseFirst.Models;
using DataBaseFirst.Models.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace DataBaseFirst.Data;

public partial class DBContext : DbContext
{
    public DBContext()
    {
    }

    public DBContext(DbContextOptions<DBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Autor> Autors { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Genero> Generos { get; set; }

    public virtual DbSet<Libro> Libros { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<Permiso> Permisos { get; set; }

    public virtual DbSet<Prestamo> Prestamos { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }
    public DbSet<LibroPrestamoDto> LibroPrestamo { get; set; }
    public DbSet<ClienteLibroPrestamoDto> ClienteLibroPrestamo { get; set; }
    public DbSet<AutorLibroDto> AutorLibro { get; set; }
    public DbSet<LoginDto> Login { get; set; }
    public DbSet<UsuarioRolDto> UsuarioRol { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server= DESKTOP-TFVCTTI\\SQLEXPRESS;Database=BIBLIOTECA;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Autor>(entity =>
        {
            entity.HasKey(e => e.IdAutor).HasName("PK__AUTOR__DA37C137BFB5B497");

            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__CLIENTE__23A3413063619AA5");

            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.IdGenero).HasName("PK__GENERO__F35167E14F91AB0F");

            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Libro>(entity =>
        {
            entity.HasKey(e => e.IdLibro).HasName("PK__LIBRO__93FF0A06A8C610FA");

            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdAutorNavigation).WithMany(p => p.Libros).HasConstraintName("FK__LIBRO__ID_AUTOR__5DCAEF64");

            entity.HasOne(d => d.IdGeneroNavigation).WithMany(p => p.Libros).HasConstraintName("FK__LIBRO__ID_GENERO__5CD6CB2B");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.IdMenu).HasName("PK__MENU__4728FC609D228085");

            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Permiso>(entity =>
        {
            entity.HasKey(e => e.IdPermiso).HasName("PK__PERMISO__AC74EBF668315AD4");

            entity.HasOne(d => d.IdMenuNavigation).WithMany(p => p.Permisos).HasConstraintName("FK__PERMISO__ID_MENU__5070F446");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Permisos).HasConstraintName("FK__PERMISO__ID_ROL__4F7CD00D");
        });

        modelBuilder.Entity<Prestamo>(entity =>
        {
            entity.HasKey(e => e.IdPrestamo).HasName("PK__PRESTAMO__3D5A1E6C0FEFA188");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Prestamos).HasConstraintName("FK__PRESTAMO__ID_CLI__6754599E");

            entity.HasOne(d => d.IdLibroNavigation).WithMany(p => p.Prestamos).HasConstraintName("FK__PRESTAMO__ID_LIB__693CA210");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Prestamos).HasConstraintName("FK__PRESTAMO__ID_USU__68487DD7");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__ROL__203B0F684542EAA3");

            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__USUARIO__91136B9002F6B8AD");

            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios).HasConstraintName("FK__USUARIO__ID_ROL__534D60F1");
        });

        modelBuilder.Entity<LibroPrestamoDto>().HasNoKey();
        modelBuilder.Entity<ClienteLibroPrestamoDto>().HasNoKey();
        modelBuilder.Entity<AutorLibroDto>().HasNoKey();
        modelBuilder.Entity<LoginDto>().HasNoKey();
        modelBuilder.Entity<UsuarioRolDto>().HasNoKey();

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
