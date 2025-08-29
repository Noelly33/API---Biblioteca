using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseFirst.Models.Dto
{
    public class UsuarioRolDto
    {
        public int id_usuario { get; set; }
        public string? usuario { get; set; }
        public string? correo { get; set; }
        public string? clave { get; set; }
        public int id_rol { get; set; }
        public string? nombre_rol { get; set; }
        public bool estado { get; set; }
        public DateTime fecha_registro { get; set; }
    }
}
