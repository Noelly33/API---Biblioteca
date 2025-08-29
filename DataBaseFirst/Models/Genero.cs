using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataBaseFirst.Models;

[Table("GENERO")]
public partial class Genero
{
    [Key]
    [Column("ID_GENERO")]
    public int IdGenero { get; set; }

    [Column("NOMBRE")]
    [StringLength(50)]
    public string? Nombre { get; set; }

    [Column("ESTADO")]
    public bool? Estado { get; set; }

    [Column("FECHA_REGISTRO", TypeName = "datetime")]
    public DateTime? FechaRegistro { get; set; }

    [InverseProperty("IdGeneroNavigation")]
    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
