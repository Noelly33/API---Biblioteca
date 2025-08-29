using DataBaseFirst.Models;
using DataBaseFirst.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Shared;

namespace DataBaseFirst.Repository.InterfacesService
{
    public interface IClienteService
    {
        Task<ApiResponse<List<Cliente>>> ListaClientes();
        Task<ApiResponse<Cliente>> ObtenerPorNombre(string nombres);
        Task<ApiResponse<Cliente>> ObtenerPorID(int id);
        Task<ApiResponse<ClienteLibroPrestamoDto>> ObtenerClienteLibroPrestamo(string nombre);
        Task<ApiResponse<object>> RegistrarCliente(Cliente cliente);
        Task<ApiResponse<object>> EditarCliente(Cliente cliente);
        Task<ApiResponse<int>> EliminarCliente(int id);
    }
}
