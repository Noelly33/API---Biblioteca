using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataBaseFirst.Models;

[Table("PERMISO")]
public partial class Permiso
{
    [Key]
    [Column("ID_PERMISO")]
    public int IdPermiso { get; set; }

    [Column("ID_ROL")]
    public int? IdRol { get; set; }

    [Column("ID_MENU")]
    public int? IdMenu { get; set; }

    [ForeignKey("IdMenu")]
    [InverseProperty("Permisos")]
    public virtual Menu? IdMenuNavigation { get; set; }

    [ForeignKey("IdRol")]
    [InverseProperty("Permisos")]
    public virtual Rol? IdRolNavigation { get; set; }
}
