using DataBaseFirst.Models;
using DataBaseFirst.Models.Dto;
using DataBaseFirst.Repository;
using DataBaseFirst.Repository.InterfacesService;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Utilities.Shared;

namespace DataBaseFirst.Service
{
    public class LibroService : ILibroService
    {
        public readonly LibroRepository _libroRepository;

        public LibroService(LibroRepository libroRepository)
        {

            _libroRepository = libroRepository;
        }

        public async Task<ApiResponse<List<Libro>>> ListaLibros()
        {
            var listaLibros = await _libroRepository.ListaLibros();

            if (listaLibros == null)
                return new ApiResponse<List<Libro>> { IsSuccess = false, Message = Mensajes.MESSAGE_QUERY_EMPTY, Data = listaLibros };

            return new ApiResponse<List<Libro>> { IsSuccess = true, Message = Mensajes.MESSAGE_QUERY_SUCCESS, Data = listaLibros };

        }

        public async Task<ApiResponse<Libro>> BuscarPorTitulo(string titulo)
        {
            var tituloLibro = await _libroRepository.BuscarPorTitulo(titulo);

            if (titulo == null)
                return new ApiResponse<Libro> { IsSuccess = false, Message = Mensajes.MESSAGE_QUERY_EMPTY, Data = tituloLibro };

            return new ApiResponse<Libro> { IsSuccess = true, Message = Mensajes.MESSAGE_QUERY_SUCCESS, Data = tituloLibro };
        }

        public async Task<ApiResponse<Libro>> BuscarPorId(int id)
        {
            var idLibro = await _libroRepository.BuscarPorId(id);

            if (idLibro == null)
                return new ApiResponse<Libro> { IsSuccess = false, Message = Mensajes.MESSAGE_QUERY_EMPTY, Data = idLibro };

            return new ApiResponse<Libro> { IsSuccess = true, Message = Mensajes.MESSAGE_QUERY_SUCCESS, Data = idLibro };

        }

        public async Task<ApiResponse<LibroPrestamoDto>> ObtenerLibroPrestamo(string titulo)
        {
            var libroPrestamo = await _libroRepository.ObtenerLibroPrestamo(titulo);

            if (libroPrestamo == null)
                return new ApiResponse<LibroPrestamoDto> { IsSuccess = false, Message = Mensajes.MESSAGE_QUERY_EMPTY, Data = libroPrestamo };

            return new ApiResponse<LibroPrestamoDto> { IsSuccess = true, Message = Mensajes.MESSAGE_QUERY_SUCCESS, Data = libroPrestamo };
        }

        public async Task<ApiResponse<object>> RegistrarLibro(Libro libro)
        {
            if (libro == null)
                return new ApiResponse<object> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR, Data = libro };

            if (string.IsNullOrWhiteSpace(libro.Titulo) || !libro.IdGenero.HasValue || libro.IdGenero <= 0 || !libro.IdAutor.HasValue || libro.IdAutor <= 0 || !libro.NumeroPaginas.HasValue || libro.NumeroPaginas <= 0 || !libro.FechaPublicacion.HasValue || libro.FechaPublicacion > DateOnly.FromDateTime(DateTime.Today))
                return new ApiResponse<object> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR, Data = libro };

            var validacion2 = await _libroRepository.ListaLibros();
            if (validacion2.Any(l => l.Titulo == libro.Titulo))
                return new ApiResponse<object> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR, Data = libro };

            var resultado = await _libroRepository.RegistrarLibro(libro);
            if (resultado > 0)
                return new ApiResponse<object> { IsSuccess = true, Message = Mensajes.MESSAGE_REGISTER };

            return new ApiResponse<object> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR, Data = libro };
        }

        public async Task<ApiResponse<object>> EditarLibro(Libro libro)
        {
            var validacionPrincipal = await _libroRepository.BuscarPorId(libro.IdLibro);
            if (validacionPrincipal == null)
                return new ApiResponse<object> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR };

            if (libro == null)
                return new ApiResponse<object> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR, Data = libro };

            if (string.IsNullOrWhiteSpace(libro.Titulo) || !libro.IdGenero.HasValue || libro.IdGenero <= 0 || !libro.IdAutor.HasValue || libro.IdAutor <= 0 || !libro.NumeroPaginas.HasValue || libro.NumeroPaginas <= 0 || !libro.FechaPublicacion.HasValue || libro.FechaPublicacion > DateOnly.FromDateTime(DateTime.Today))
                return new ApiResponse<object> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR, Data = libro };

            var validacion2 = await _libroRepository.ListaLibros();
            if (validacion2.Any(l => l.Titulo == libro.Titulo))
                return new ApiResponse<object> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR, Data = libro };

            var resultado = await _libroRepository.EditarLibro(libro);
            if (resultado > 0)
                return new ApiResponse<object> { IsSuccess = true, Message = Mensajes.MESSAGE_UPDATE };

            return new ApiResponse<object> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR, Data = libro };
        }

        public async Task<ApiResponse<int>> EliminarLibro(int idLibro)
        {
            try
            {
                var libro = await _libroRepository.BuscarPorId(idLibro);
                if (libro == null)
                    return new ApiResponse<int> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR };

                var resultado = await _libroRepository.EliminarLibro(idLibro);
                if (resultado > 0)
                    return new ApiResponse<int> { IsSuccess = true, Message = Mensajes.MESSAGE_DELETE };

                return new ApiResponse<int> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR };
            }

            catch (SqlException ex) when (ex.Number == 547)
            {
                return new ApiResponse<int> { IsSuccess = false, Message = Mensajes.MESSAGE_DELETE_ERROR };
            }
        }
    }
}
