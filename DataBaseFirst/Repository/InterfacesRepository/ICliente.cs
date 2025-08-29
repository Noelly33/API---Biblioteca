using DataBaseFirst.Models;
using DataBaseFirst.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseFirst.Repository.InterfacesRepository
{
    public interface ICliente
    {
        Task<List<Cliente>> ListaClientes();
        Task<Cliente> ObtenerPorNombre(string nombres);
        Task<Cliente> ObtenerPorID(int id);
        Task<ClienteLibroPrestamoDto> ObtenerClienteLibroPrestamo(string nombre);
        Task<int> RegistrarCliente(Cliente cliente);
        Task<int> EditarCliente(Cliente cliente);
        Task<int> EliminarCliente(int id);

    }
}
