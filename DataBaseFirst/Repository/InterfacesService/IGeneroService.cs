using DataBaseFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Shared;

namespace DataBaseFirst.Repository.InterfacesService
{
    public interface IGeneroService
    {
        Task<ApiResponse<List<Genero>>> ListaGeneros();
        Task<ApiResponse<Genero>> ObtenerPorNombre(string nombre);
        Task<ApiResponse<Genero>> ObtenerPorID(int id);
        Task<ApiResponse<object>> RegistrarGenero(Genero genero);
        Task<ApiResponse<object>> EditarGenero(Genero genero);
        Task<ApiResponse<int>> EliminarGenero(int id);
    }
}
