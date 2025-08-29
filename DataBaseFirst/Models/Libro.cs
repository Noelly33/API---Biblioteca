using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataBaseFirst.Models;

[Table("LIBRO")]
public partial class Libro
{
    [Key]
    [Column("ID_LIBRO")]
    public int IdLibro { get; set; }

    [Column("TITULO")]
    [StringLength(100)]
    public string? Titulo { get; set; }

    [Column("ID_GENERO")]
    public int? IdGenero { get; set; }

    [Column("ID_AUTOR")]
    public int? IdAutor { get; set; }

    [Column("NUMERO_PAGINAS")]
    public int? NumeroPaginas { get; set; }

    [Column("FECHA_PUBLICACION")]
    public DateOnly? FechaPublicacion { get; set; }

    [Column("ESTADO")]
    public bool? Estado { get; set; }

    [Column("FECHA_REGISTRO", TypeName = "datetime")]
    public DateTime? FechaRegistro { get; set; }

    [ForeignKey("IdAutor")]
    [InverseProperty("Libros")]
    public virtual Autor? IdAutorNavigation { get; set; }

    [ForeignKey("IdGenero")]
    [InverseProperty("Libros")]
    public virtual Genero? IdGeneroNavigation { get; set; }

    [InverseProperty("IdLibroNavigation")]
    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
}
