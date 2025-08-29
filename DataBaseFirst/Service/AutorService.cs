using DataBaseFirst.Data;
using DataBaseFirst.Models;
using DataBaseFirst.Models.Dto;
using DataBaseFirst.Repository;
using DataBaseFirst.Repository.InterfacesRepository;
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
    public class AutorService : IAutorService
    {
        public readonly AutorRepository _autorRepository;

        public AutorService(AutorRepository autorRepository)
        {

            _autorRepository = autorRepository;
        }

        public async Task<ApiResponse<List<Autor>>> ListaAutores()
        {
            var listaAutores = await _autorRepository.ListaAutores();

            if (listaAutores == null)
                return new ApiResponse<List<Autor>> { IsSuccess = false, Message = Mensajes.MESSAGE_QUERY_EMPTY, Data = listaAutores };

            return new ApiResponse<List<Autor>> { IsSuccess = true, Message = Mensajes.MESSAGE_QUERY_SUCCESS, Data = listaAutores };

        }

        public async Task<ApiResponse<Autor>> ObtenerAutorNombre(string nombre)
        {
            var nombreAutor = await _autorRepository.ObtenerAutorNombre(nombre);

            if (nombre == null)
                return new ApiResponse<Autor> { IsSuccess = false, Message = Mensajes.MESSAGE_QUERY_EMPTY, Data = nombreAutor };

            return new ApiResponse<Autor> { IsSuccess = true, Message = Mensajes.MESSAGE_QUERY_SUCCESS, Data = nombreAutor };
        }

        public async Task<ApiResponse<Autor>> ObtenerAutorID(int id)
        {
            var idAutor = await _autorRepository.ObtenerAutorID(id);

            if (idAutor == null)
                return new ApiResponse<Autor> { IsSuccess = false, Message = Mensajes.MESSAGE_QUERY_EMPTY, Data = idAutor };

            return new ApiResponse<Autor> { IsSuccess = true, Message = Mensajes.MESSAGE_QUERY_SUCCESS, Data = idAutor };

        }

        public async Task<ApiResponse<AutorLibroDto>> ObtenerAutorLibro(string nombre)
        {
            var autorLibro = await _autorRepository.ObtenerAutorLibro(nombre);

            if (autorLibro == null)
                return new ApiResponse<AutorLibroDto> { IsSuccess = false, Message = Mensajes.MESSAGE_QUERY_EMPTY, Data = autorLibro };

            return new ApiResponse<AutorLibroDto> { IsSuccess = true, Message = Mensajes.MESSAGE_QUERY_SUCCESS, Data = autorLibro };
        }

        public async Task<ApiResponse<object>> RegistrarAutor(Autor autor)
        {
            if (autor == null)
                return new ApiResponse<object> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR, Data = autor };

            if (string.IsNullOrWhiteSpace(autor.Nombre) || string.IsNullOrWhiteSpace(autor.Nacionalidad))
                return new ApiResponse<object> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR, Data = autor };

            var validacion = new Regex("^[a-zA-ZáéíóúÁÉÍÓÚñN\\s]+$");
            if (!validacion.IsMatch(autor.Nombre) || !validacion.IsMatch(autor.Nacionalidad))
                return new ApiResponse<object> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR, Data = autor };

            var resultado = await _autorRepository.RegistrarAutor(autor);
            if (resultado > 0)
                return new ApiResponse<object> { IsSuccess = true, Message = Mensajes.MESSAGE_REGISTER };

            return new ApiResponse<object> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR, Data = autor };
        }

        public async Task<ApiResponse<object>> EditarAutor(Autor autor)
        {
            var validacionPrincipal = await _autorRepository.ObtenerAutorID(autor.IdAutor);
            if (validacionPrincipal == null)
                return new ApiResponse<object> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR };

            if (autor == null)
                return new ApiResponse<object> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR, Data = autor };

            if (string.IsNullOrWhiteSpace(autor.Nombre) || string.IsNullOrWhiteSpace(autor.Nacionalidad))
                return new ApiResponse<object> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR, Data = autor };

            var validacion = new Regex("^[a-zA-ZáéíóúÁÉÍÓÚñN\\s]+$");
            if (!validacion.IsMatch(autor.Nombre) || !validacion.IsMatch(autor.Nacionalidad))
                return new ApiResponse<object> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR, Data = autor };

            var resultado = await _autorRepository.EditarAutor(autor);
            if (resultado > 0)
                return new ApiResponse<object> { IsSuccess = true, Message = Mensajes.MESSAGE_UPDATE };

            return new ApiResponse<object> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR, Data = autor };
        }

        public async Task<ApiResponse<int>> EliminarAutor(int id)
        {
            try
            {
                var autor = await _autorRepository.ObtenerAutorID(id);
                if (autor == null)
                    return new ApiResponse<int> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR };
                var resultado = await _autorRepository.EliminarAutor(id);
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
