using DataBaseFirst.Models;
using DataBaseFirst.Repository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Utilities;

namespace DataBaseFirst.Service
{
    public class UsuarioService
    {

        public readonly UsuarioRepository _usuarioRepository;

        public UsuarioService(UsuarioRepository usuarioRepository)
        {

            _usuarioRepository = usuarioRepository;
        }

        public async Task<ApiResponse<List<Usuario>>> ListaUsuarios()
        {
            var listaUsuarios = await _usuarioRepository.ListaUsuarios();

            if (listaUsuarios == null)
                return new ApiResponse<List<Usuario>> { IsSuccess = false, Message = Mensajes.MESSAGE_QUERY_EMPTY, Data = listaUsuarios };

            return new ApiResponse<List<Usuario>> { IsSuccess = true, Message = Mensajes.MESSAGE_QUERY_SUCCESS, Data = listaUsuarios };

        }

        public async Task<ApiResponse<Usuario>> ObtenerUsuarioNombre(string nombres)
        {
            var nombreUsuario = await _usuarioRepository.ObtenerUsuarioNombre(nombres);

            if (nombres == null)
                return new ApiResponse<Usuario> { IsSuccess = false, Message = Mensajes.MESSAGE_QUERY_EMPTY, Data = nombreUsuario };

            return new ApiResponse<Usuario> { IsSuccess = true, Message = Mensajes.MESSAGE_QUERY_SUCCESS, Data = nombreUsuario };
        }

        public async Task<ApiResponse<Usuario>> ObtenerUsuarioID(int id)
        {
            var idUsuario = await _usuarioRepository.ObtenerUsuarioID(id);

            if (idUsuario == null)
                return new ApiResponse<Usuario> { IsSuccess = false, Message = Mensajes.MESSAGE_QUERY_EMPTY, Data = idUsuario };

            return new ApiResponse<Usuario> { IsSuccess = true, Message = Mensajes.MESSAGE_QUERY_SUCCESS, Data = idUsuario };

        }

        public async Task<ApiResponse<object>> RegistrarUsuario(Usuario usuario)
        {
            if (usuario == null)
                return new ApiResponse<object> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR, Data = usuario };

            if (string.IsNullOrWhiteSpace(usuario.Nombres) || string.IsNullOrWhiteSpace(usuario.Apellidos) || string.IsNullOrWhiteSpace(usuario.Correo))
                return new ApiResponse<object> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR, Data = usuario };

            var validacion = new Regex("^[a-zA-ZáéíóúÁÉÍÓÚñN\\s]+$");
            if (!validacion.IsMatch(usuario.Nombres) || !validacion.IsMatch(usuario.Apellidos))
                return new ApiResponse<object> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR, Data = usuario };

            var validacion2 = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (validacion.IsMatch(usuario.Correo))
                return new ApiResponse<object> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR, Data = usuario };

            var resultado = await _usuarioRepository.RegistrarUsuario(usuario);
            if (resultado > 0)
                return new ApiResponse<object> { IsSuccess = true, Message = Mensajes.MESSAGE_REGISTER };

            return new ApiResponse<object> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR, Data = usuario };
        }

        public async Task<ApiResponse<object>> EditarUsuario(Usuario usuario)
        {
            var validacionPrincipal = await _usuarioRepository.ObtenerUsuarioID(usuario.IdUsuario);
            if (validacionPrincipal == null)
                return new ApiResponse<object> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR };

            if (usuario == null)
                return new ApiResponse<object> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR, Data = usuario };

            if (string.IsNullOrWhiteSpace(usuario.Nombres) || string.IsNullOrWhiteSpace(usuario.Apellidos) || string.IsNullOrWhiteSpace(usuario.Correo))
                return new ApiResponse<object> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR, Data = usuario };

            var validacion = new Regex("^[a-zA-ZáéíóúÁÉÍÓÚñN\\s]+$");
            if (!validacion.IsMatch(usuario.Nombres) || !validacion.IsMatch(usuario.Apellidos))
                return new ApiResponse<object> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR, Data = usuario };

            var validacion2 = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (validacion.IsMatch(usuario.Correo))
                return new ApiResponse<object> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR, Data = usuario };

            var resultado = await _usuarioRepository.EditarUsuario(usuario);
            if (resultado > 0)
                return new ApiResponse<object> { IsSuccess = true, Message = Mensajes.MESSAGE_UPDATE };

            return new ApiResponse<object> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR, Data = usuario };
        }

        public async Task<ApiResponse<int>> EliminarUsuario(int id)
        {
            try
            {
                var usuario = await _usuarioRepository.ObtenerUsuarioID(id);
                if (usuario == null)
                    return new ApiResponse<int> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR };

                var resultado = await _usuarioRepository.EliminarUsuario(id);
                if (resultado > 0)
                    return new ApiResponse<int> { IsSuccess = true, Message = Mensajes.MESSAGE_DELETE };

                return new ApiResponse<int> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR };
            }
            catch (SqlException ex) when(ex.Number == 547)
            {
                return new ApiResponse<int> { IsSuccess = false, Message = Mensajes.MESSAGE_DELETE_ERROR };
            }
        }
    }
}
