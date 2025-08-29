using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataBaseFirst.Models;

[Table("USUARIO")]
public partial class Usuario
{
    [Key]
    [Column("ID_USUARIO")]
    public int IdUsuario { get; set; }

    [Column("USUARIO")]
    [StringLength(100)]
    public string? Usuario1 { get; set; }

    [Column("CORREO")]
    [StringLength(100)]
    public string? Correo { get; set; }

    [Column("CLAVE")]
    [StringLength(300)]
    public string? Clave { get; set; }

    [Column("ID_ROL")]
    public int? IdRol { get; set; }

    [Column("ESTADO")]
    public bool? Estado { get; set; }

    [Column("FECHA_REGISTRO", TypeName = "datetime")]
    public DateTime? FechaRegistro { get; set; }

    [ForeignKey("IdRol")]
    [InverseProperty("Usuarios")]
    public virtual Rol? IdRolNavigation { get; set; }

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
}
