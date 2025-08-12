using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataBaseFirst.Models;

[Table("AUTOR")]
public partial class Autor
{
    //Esta es la clase autor
    [Key]
    [Column("ID_AUTOR")]
    public int IdAutor { get; set; }

    [Column("NOMBRE")]
    [StringLength(50)]
    public string? Nombre { get; set; }

    [Column("NACIONALIDAD")]
    [StringLength(50)]
    public string? Nacionalidad { get; set; }

    [Column("ESTADO")]
    public bool? Estado { get; set; }

    [Column("FECHA_REGISTRO", TypeName = "datetime")]
    public DateTime? FechaRegistro { get; set; }

    [InverseProperty("IdAutorNavigation")]
    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
