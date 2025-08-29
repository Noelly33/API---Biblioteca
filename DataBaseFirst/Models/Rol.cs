using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataBaseFirst.Models;

[Table("ROL")]
public partial class Rol
{
    [Key]
    [Column("ID_ROL")]
    public int IdRol { get; set; }

    [Column("NOMBRE")]
    [StringLength(50)]
    public string? Nombre { get; set; }

    [Column("FECHA_REGISTRO", TypeName = "datetime")]
    public DateTime? FechaRegistro { get; set; }

    [InverseProperty("IdRolNavigation")]
    public virtual ICollection<Permiso> Permisos { get; set; } = new List<Permiso>();

    [InverseProperty("IdRolNavigation")]
    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
