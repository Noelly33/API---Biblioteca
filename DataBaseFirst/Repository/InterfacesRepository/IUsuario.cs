using DataBaseFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseFirst.Repository.InterfacesRepository
{
    public interface IUsuario
    {
        Task<List<Usuario>> ListaUsuarios();
        Task<Usuario> ObtenerUsuarioNombre(string nombres);
        Task<int> RegistrarUsuario(Usuario usuario);
        Task<int> EditarUsuario(Usuario usuario);
        Task<int> EliminarUsuario(int id);
    }
}
