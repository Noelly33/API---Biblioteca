using DataBaseFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseFirst.Repository.InterfacesRepository
{
    public interface IAutor
    {
        Task<List<Autor>> ListaAutores();
        Task<Autor> ObtenerAutorNombre(string nombre);
        Task<Autor> ObtenerAutorID(int id);
        Task<int> RegistrarAutor(Autor autor);
        Task<int> EditarAutor(Autor autor);
        Task<int>EliminarAutor(int id);
    }
}
