using DataBaseFirst.Models;
using DataBaseFirst.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Shared;

namespace DataBaseFirst.Repository.InterfacesService
{
    public interface IUsuarioService
    {
        Task<ApiResponse<List<Usuario>>> ListaUsuarios();
        //Task<ApiResponse<Usuario1>> ObtenerUsuarioNombre(string nombres);
        Task<ApiResponse<Usuario>> ObtenerUsuarioID(int id);
        Task<ApiResponse<UsuarioRolDto>> IniciarSesion(LoginDto login);
        Task<ApiResponse<object>> RegistrarUsuario(Usuario usuario);
        Task<ApiResponse<object>> EditarUsuario(Usuario usuario);
        Task<ApiResponse<int>> EliminarUsuario(int id);
    }
}
