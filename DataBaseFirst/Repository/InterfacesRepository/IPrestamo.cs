using DataBaseFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseFirst.Repository.InterfacesRepository
{
    public interface IPrestamo
    {
        Task<List<Prestamo>> ListaPrestamos();
        Task<Prestamo> ObtenerPorUsuario(int id);
        Task<Prestamo> ObtenerPorLibro(int id);
        Task<int> RegistrarPrestamo(Prestamo prestamo);
    }
}
