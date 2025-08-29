using DataBaseFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Shared;

namespace DataBaseFirst.Repository.InterfacesService
{
    public interface IPrestamoService
    {
        Task<ApiResponse<List<Prestamo>>> ListaPrestamos();
        Task<ApiResponse<Prestamo>> ObtenerPorID(int id);
        Task<ApiResponse<object>> RegistrarPrestamo(Prestamo prestamo);
    }
}
