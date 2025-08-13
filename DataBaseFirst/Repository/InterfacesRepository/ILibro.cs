using DataBaseFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseFirst.Repository.InterfacesRepository
{
    public interface ILibro
    {
        Task<List<Libro>> ListaLibros();
        Task<Libro> BuscarPorTitulo(string titulo);
        Task<Libro> BuscarPorId(int id);
        Task<int> RegistrarLibro(Libro libro);
        Task<int> EditarLibro(Libro libro);
        Task<int> EliminarLibro(int id);


    }
}
