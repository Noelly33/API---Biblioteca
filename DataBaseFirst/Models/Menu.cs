using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataBaseFirst.Models;

[Table("MENU")]
public partial class Menu
{
    [Key]
    [Column("ID_MENU")]
    public int IdMenu { get; set; }

    [Column("NOMBRE")]
    [StringLength(100)]
    public string? Nombre { get; set; }

    [Column("URL_MENU")]
    [StringLength(100)]
    public string? UrlMenu { get; set; }

    [Column("FECHA_REGISTRO", TypeName = "datetime")]
    public DateTime? FechaRegistro { get; set; }

    [InverseProperty("IdMenuNavigation")]
    public virtual ICollection<Permiso> Permisos { get; set; } = new List<Permiso>();
}
