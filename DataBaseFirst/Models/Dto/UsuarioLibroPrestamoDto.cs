using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseFirst.Models.Dto
{
    public class UsuarioLibroPrestamoDto
    {
        public string? Nombres { get; set; }
        public string? Titulo { get; set; }
        public string? Nombre { get; set; }
        public DateTime Fecha_Prestamo { get; set; }
    }
}
