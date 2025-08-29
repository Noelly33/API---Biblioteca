using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataBaseFirst.Models;

[Table("CLIENTE")]
public partial class Cliente
{
    [Key]
    [Column("ID_CLIENTE")]
    public int IdCliente { get; set; }

    [Column("NOMBRES")]
    [StringLength(50)]
    public string? Nombres { get; set; }

    [Column("APELLIDOS")]
    [StringLength(50)]
    public string? Apellidos { get; set; }

    [Column("CEDULA")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Cedula { get; set; }

    [Column("ESTADO")]
    public bool? Estado { get; set; }

    [Column("FECHA_REGISTRO", TypeName = "datetime")]
    public DateTime? FechaRegistro { get; set; }

    [InverseProperty("IdClienteNavigation")]
    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
}
