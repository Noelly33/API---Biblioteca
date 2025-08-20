using System;
using System.Collections.Generic;
using DataBaseFirst.Models;
using DataBaseFirst.Models.Dto;
using Microsoft.EntityFrameworkCore;

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

    public virtual DbSet<Libro> Libros { get; set; }

    public virtual DbSet<Prestamo> Prestamos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public DbSet<LibroPrestamoDto> LibroPrestamo { get; set; } 
    public DbSet<AutorLibroDto> AutorLibro { get; set; }
    public DbSet<UsuarioLibroPrestamoDto> UsuarioLibroPrestamo { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-TFVCTTI\\SQLEXPRESS;Initial Catalog=BIBLIOTECA;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Autor>(entity =>
        {
            entity.HasKey(e => e.IdAutor).HasName("PK__AUTOR__DA37C1374938CB01");

            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Libro>(entity =>
        {
            entity.HasKey(e => e.IdLibro).HasName("PK__LIBRO__93FF0A067A303A37");

            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdAutorNavigation).WithMany(p => p.Libros).HasConstraintName("FK__LIBRO__ID_AUTOR__4CA06362");
        });

        modelBuilder.Entity<Prestamo>(entity =>
        {
            entity.HasKey(e => e.IdPrestamos).HasName("PK__PRESTAMO__B6D7A3E25B44ED18");

            entity.HasOne(d => d.IdLibroNavigation).WithMany(p => p.Prestamos).HasConstraintName("FK__PRESTAMOS__ID_LI__5441852A");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Prestamos).HasConstraintName("FK__PRESTAMOS__ID_US__534D60F1");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__USUARIO__91136B90B88654B0");

            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<LibroPrestamoDto>().HasNoKey();
        modelBuilder.Entity<AutorLibroDto>().HasNoKey();
        modelBuilder.Entity<UsuarioLibroPrestamoDto>().HasNoKey();

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
