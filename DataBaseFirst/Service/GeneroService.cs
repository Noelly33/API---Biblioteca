using DataBaseFirst.Models;
using DataBaseFirst.Repository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Shared;

namespace DataBaseFirst.Service
{
    public class GeneroService
    {

        public readonly GeneroRepository _generoRepository;

        public GeneroService(GeneroRepository generoRepository)
        {

            _generoRepository = generoRepository;
        }

        public async Task<ApiResponse<List<Genero>>> ListaGeneros()
        {
            var listaGeneros = await _generoRepository.ListaGeneros();

            if (listaGeneros == null)
                return new ApiResponse<List<Genero>> { IsSuccess = false, Message = Mensajes.MESSAGE_QUERY_EMPTY, Data = listaGeneros };

            return new ApiResponse<List<Genero>> { IsSuccess = true, Message = Mensajes.MESSAGE_QUERY_SUCCESS, Data = listaGeneros };

        }

        public async Task<ApiResponse<Genero>> ObtenerPorNombre(string nombre)
        {
            var nombreGenero = await _generoRepository.ObtenerPorNombre(nombre);

            if (nombre == null)
                return new ApiResponse<Genero> { IsSuccess = false, Message = Mensajes.MESSAGE_QUERY_EMPTY, Data = nombreGenero };

            return new ApiResponse<Genero> { IsSuccess = true, Message = Mensajes.MESSAGE_QUERY_SUCCESS, Data = nombreGenero };
        }

        public async Task<ApiResponse<Genero>> ObtenerPorID(int id)
        {
            var idGenero = await _generoRepository.ObtenerPorID(id);

            if (idGenero == null)
                return new ApiResponse<Genero> { IsSuccess = false, Message = Mensajes.MESSAGE_QUERY_EMPTY, Data = idGenero };

            return new ApiResponse<Genero> { IsSuccess = true, Message = Mensajes.MESSAGE_QUERY_SUCCESS, Data = idGenero };

        }

        public async Task<ApiResponse<object>> RegistrarGenero(Genero genero)
        {
            if (genero == null)
                return new ApiResponse<object> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR, Data = genero };

            if (string.IsNullOrWhiteSpace(genero.Nombre))
                return new ApiResponse<object> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR, Data = genero };

            var resultado = await _generoRepository.RegistrarGenero(genero);
            if (resultado > 0)
                return new ApiResponse<object> { IsSuccess = true, Message = Mensajes.MESSAGE_REGISTER };

            return new ApiResponse<object> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR, Data = genero };
        }

        public async Task<ApiResponse<object>> EditarGenero(Genero genero)
        {
            var validacionPrincipal = await _generoRepository.ObtenerPorID(genero.IdGenero);
            if (validacionPrincipal == null)
                return new ApiResponse<object> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR };

            if (genero == null)
                return new ApiResponse<object> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR, Data = genero };

            if (string.IsNullOrWhiteSpace(genero.Nombre))
                return new ApiResponse<object> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR, Data = genero };

            var resultado = await _generoRepository.EditarGenero(genero);
            if (resultado > 0)
                return new ApiResponse<object> { IsSuccess = true, Message = Mensajes.MESSAGE_UPDATE };

            return new ApiResponse<object> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR, Data = genero };
        }

        public async Task<ApiResponse<int>> EliminarGenero(int id)
        {
            try
            {
                var cliente = await _generoRepository.ObtenerPorID(id);
                if (cliente == null)
                    return new ApiResponse<int> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR };
                var resultado = await _generoRepository.EliminarGenero(id);
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
