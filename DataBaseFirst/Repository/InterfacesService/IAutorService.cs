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
    public interface IAutorService
    {
        Task<ApiResponse<List<Autor>>> ListaAutores();
        Task<ApiResponse<Autor>> ObtenerAutorNombre(string nombre);
        Task<ApiResponse<Autor>> ObtenerAutorID(int id);
        Task<ApiResponse<AutorLibroDto>> ObtenerAutorLibro(string nombre);
        Task<ApiResponse<object>> RegistrarAutor(Autor autor);
        Task<ApiResponse<object>> EditarAutor(Autor autor);
        Task<ApiResponse<int>> EliminarAutor(int id);
    }
}
