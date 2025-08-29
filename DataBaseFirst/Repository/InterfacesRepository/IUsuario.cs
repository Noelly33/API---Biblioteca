using DataBaseFirst.Models;
using DataBaseFirst.Models.Dto;
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
        //Task<Usuario1> ObtenerUsuarioNombre(string nombres);
        Task<Usuario> ObtenerUsuarioID(int id);
        Task<UsuarioRolDto> IniciarSesion(LoginDto login);
        Task<int> RegistrarUsuario(Usuario usuario);
        Task<int> EditarUsuario(Usuario usuario);
        Task<int> EliminarUsuario(int id);
    }
}
