using DataBaseFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseFirst.Repository.InterfacesRepository
{
    public interface IGenero
    {
        Task<List<Genero>> ListaGeneros();
        Task<Genero> ObtenerPorNombre(string nombre);
        Task<Genero> ObtenerPorID(int id);
        Task<int> RegistrarGenero(Genero genero);
        Task<int> EditarGenero(Genero genero);
        Task<int> EliminarGenero(int id);
    }
}
