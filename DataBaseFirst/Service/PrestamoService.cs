using DataBaseFirst.Models;
using DataBaseFirst.Repository;
using DataBaseFirst.Repository.InterfacesRepository;
using DataBaseFirst.Repository.InterfacesService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Utilities;

namespace DataBaseFirst.Service
{
    public class PrestamoService : IPrestamoService
    {
        public readonly PrestamoRepository _prestamoRepository;

        public PrestamoService(PrestamoRepository prestamoRepository)
        {

            _prestamoRepository = prestamoRepository;
        }

        public async Task<ApiResponse<List<Prestamo>>> ListaPrestamos()
        {
            var listaPrestamos = await _prestamoRepository.ListaPrestamos();

            if (listaPrestamos == null)
                return new ApiResponse<List<Prestamo>> { IsSuccess = false, Message = Mensajes.MESSAGE_QUERY_EMPTY, Data = listaPrestamos };

            return new ApiResponse<List<Prestamo>> { IsSuccess = true, Message = Mensajes.MESSAGE_QUERY_SUCCESS, Data = listaPrestamos };

        }

        public async Task<ApiResponse<Prestamo>> ObtenerPorID(int id)
        {
            var idPrestamo = await _prestamoRepository.ObtenerPorID(id);

            if (idPrestamo == null)
                return new ApiResponse<Prestamo> { IsSuccess = false, Message = Mensajes.MESSAGE_QUERY_EMPTY, Data = idPrestamo };

            return new ApiResponse<Prestamo> { IsSuccess = true, Message = Mensajes.MESSAGE_QUERY_SUCCESS, Data = idPrestamo };

        }

        public async Task<ApiResponse<Prestamo>> ObtenerPorLibro(int idLibro)
        {
            var libro = await _prestamoRepository.ObtenerPorLibro(idLibro);

            if (idLibro == null)
                return new ApiResponse<Prestamo> { IsSuccess = false, Message = Mensajes.MESSAGE_QUERY_EMPTY, Data = libro };

            return new ApiResponse<Prestamo> { IsSuccess = true, Message = Mensajes.MESSAGE_QUERY_SUCCESS, Data = libro };
        }

        public async Task<ApiResponse<Prestamo>> ObtenerPorUsuario(int idUsuario)
        {
            var usuario = await _prestamoRepository.ObtenerPorUsuario(idUsuario);

            if (usuario == null)
                return new ApiResponse<Prestamo> { IsSuccess = false, Message = Mensajes.MESSAGE_QUERY_EMPTY, Data = usuario };

            return new ApiResponse<Prestamo> { IsSuccess = true, Message = Mensajes.MESSAGE_QUERY_SUCCESS, Data = usuario };

        }

        public async Task<ApiResponse<object>> RegistrarPrestamo(Prestamo prestamo)
        {
            if (prestamo == null)
                return new ApiResponse<object> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR, Data = prestamo };

            if (!prestamo.IdUsuario.HasValue || prestamo.IdUsuario <= 0 || !prestamo.IdLibro.HasValue || prestamo.IdLibro <= 0 || !prestamo.FechaPrestamo.HasValue || !prestamo.FechaDevolucion.HasValue ||prestamo.FechaDevolucion > DateOnly.FromDateTime(DateTime.Today))
                return new ApiResponse<object> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR, Data = prestamo };

            var resultado = await _prestamoRepository.RegistrarPrestamo(prestamo);
            if (resultado > 0)
                return new ApiResponse<object> { IsSuccess = true, Message = Mensajes.MESSAGE_REGISTER };

            return new ApiResponse<object> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR, Data = prestamo };
        }

    }
}
