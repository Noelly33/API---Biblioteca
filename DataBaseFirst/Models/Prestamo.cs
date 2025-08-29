using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataBaseFirst.Models;

[Table("PRESTAMO")]
public partial class Prestamo
{
    [Key]
    [Column("ID_PRESTAMO")]
    public int IdPrestamo { get; set; }

    [Column("ID_CLIENTE")]
    public int? IdCliente { get; set; }

    [Column("ID_USUARIO")]
    public int? IdUsuario { get; set; }

    [Column("ID_LIBRO")]
    public int? IdLibro { get; set; }

    [Column("FECHA_PRESTAMO")]
    public DateOnly? FechaPrestamo { get; set; }

    [Column("FECHA_DEVOLUCION")]
    public DateOnly? FechaDevolucion { get; set; }

    [Column("ESTADO")]
    public bool? Estado { get; set; }

    [ForeignKey("IdCliente")]
    [InverseProperty("Prestamos")]
    public virtual Cliente? IdClienteNavigation { get; set; }

    [ForeignKey("IdLibro")]
    [InverseProperty("Prestamos")]
    public virtual Libro? IdLibroNavigation { get; set; }

    [ForeignKey("IdUsuario")]
    [InverseProperty("Prestamos")]
    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
