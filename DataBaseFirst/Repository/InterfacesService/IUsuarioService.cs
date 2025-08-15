using DataBaseFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace DataBaseFirst.Repository.InterfacesService
{
    public interface IUsuarioService
    {
        Task<ApiResponse<List<Usuario>>> ListaUsuarios();
        Task<ApiResponse<Usuario>> ObtenerUsuarioNombre(string nombres);
        Task<ApiResponse<Usuario>> ObtenerUsuarioID(int id);
        Task<ApiResponse<object>> RegistrarUsuario(Usuario usuario);
        Task<ApiResponse<object>> EditarUsuario(Usuario usuario);
        Task<ApiResponse<int>> EliminarUsuario(int id);
    }
}
