using DataBaseFirst.Models;
using DataBaseFirst.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace DataBaseFirst.Repository.InterfacesService
{
    public interface ILibroService
    {
        Task<ApiResponse<List<Libro>>> ListaLibros();
        Task<ApiResponse<Libro>> BuscarPorTitulo(string titulo);
        Task<ApiResponse<Libro>> BuscarPorId(int id);
        Task<ApiResponse<LibroPrestamoDto>> ObtenerLibroPrestamo(string nombre);
        Task<ApiResponse<object>> RegistrarLibro(Libro libro);
        Task<ApiResponse<object>> EditarLibro(Libro libro);
        Task<ApiResponse<int>> EliminarLibro(int id);

    }
}
