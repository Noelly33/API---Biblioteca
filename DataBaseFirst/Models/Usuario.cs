using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataBaseFirst.Models;

[Table("USUARIO")]
public partial class Usuario
{
    //Esta es la clase usuario
    [Key]
    [Column("ID_USUARIO")]
    public int IdUsuario { get; set; }

    [Column("NOMBRES")]
    [StringLength(50)]
    public string? Nombres { get; set; }

    [Column("APELLIDOS")]
    [StringLength(50)]
    public string? Apellidos { get; set; }

    [Column("CORREO")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Correo { get; set; }

    [Column("ESTADO")]
    public bool? Estado { get; set; }

    [Column("FECHA_REGISTRO", TypeName = "datetime")]
    public DateTime? FechaRegistro { get; set; }

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
}
