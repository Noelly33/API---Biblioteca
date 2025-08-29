using DataBaseFirst.Models;
using DataBaseFirst.Models.Dto;
using DataBaseFirst.Repository;
using DataBaseFirst.Repository.InterfacesService;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Utilities.Shared;

namespace DataBaseFirst.Service
{
    public class ClienteService : IClienteService
    {
        public readonly ClienteRepository _clienteRepository;

        public ClienteService(ClienteRepository clienteRepository)
        {

            _clienteRepository = clienteRepository;
        }

        public async Task<ApiResponse<List<Cliente>>> ListaClientes()
        {
            var listaClientes = await _clienteRepository.ListaClientes();

            if (listaClientes == null)
                return new ApiResponse<List<Cliente>> { IsSuccess = false, Message = Mensajes.MESSAGE_QUERY_EMPTY, Data = listaClientes };

            return new ApiResponse<List<Cliente>> { IsSuccess = true, Message = Mensajes.MESSAGE_QUERY_SUCCESS, Data = listaClientes };

        }

        public async Task<ApiResponse<Cliente>> ObtenerPorNombre(string nombre)
        {
            var nombreCliente = await _clienteRepository.ObtenerPorNombre(nombre);

            if (nombre == null)
                return new ApiResponse<Cliente> { IsSuccess = false, Message = Mensajes.MESSAGE_QUERY_EMPTY, Data = nombreCliente };

            return new ApiResponse<Cliente> { IsSuccess = true, Message = Mensajes.MESSAGE_QUERY_SUCCESS, Data = nombreCliente };
        }

        public async Task<ApiResponse<Cliente>> ObtenerPorID(int id)
        {
            var idCliente = await _clienteRepository.ObtenerPorID(id);

            if (idCliente == null)
                return new ApiResponse<Cliente> { IsSuccess = false, Message = Mensajes.MESSAGE_QUERY_EMPTY, Data = idCliente };

            return new ApiResponse<Cliente> { IsSuccess = true, Message = Mensajes.MESSAGE_QUERY_SUCCESS, Data = idCliente };

        }

        public async Task<ApiResponse<ClienteLibroPrestamoDto>> ObtenerClienteLibroPrestamo(string nombre)
        {
            var clienteLibroPrestamo = await _clienteRepository.ObtenerClienteLibroPrestamo(nombre);

            if (clienteLibroPrestamo == null)
                return new ApiResponse<ClienteLibroPrestamoDto> { IsSuccess = false, Message = Mensajes.MESSAGE_QUERY_EMPTY, Data = clienteLibroPrestamo };

            return new ApiResponse<ClienteLibroPrestamoDto> { IsSuccess = true, Message = Mensajes.MESSAGE_QUERY_SUCCESS, Data = clienteLibroPrestamo };
        }

        public async Task<ApiResponse<object>> RegistrarCliente(Cliente cliente)
        {
            if (cliente == null)
                return new ApiResponse<object> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR, Data = cliente };

            if (string.IsNullOrWhiteSpace(cliente.Nombres) || string.IsNullOrWhiteSpace(cliente.Apellidos) || string.IsNullOrWhiteSpace(cliente.Cedula))
                return new ApiResponse<object> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR, Data = cliente };

            var validacion = new Regex("^[a-zA-ZáéíóúÁÉÍÓÚñN\\s]+$");
            if (!validacion.IsMatch(cliente.Nombres) || !validacion.IsMatch(cliente.Apellidos))
                return new ApiResponse<object> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR, Data = cliente };

            var validacion2 = new Regex(@"^\d{10}$");
            if (!validacion2.IsMatch(cliente.Cedula))
                return new ApiResponse<object> { IsSuccess = false, Message = Mensajes.MESSAGE_REQUIRED_FIELDS, Data = cliente };


            var resultado = await _clienteRepository.RegistrarCliente(cliente);
            if (resultado > 0)
                return new ApiResponse<object> { IsSuccess = true, Message = Mensajes.MESSAGE_REGISTER };

            return new ApiResponse<object> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR, Data = cliente };
        }

        public async Task<ApiResponse<object>> EditarCliente(Cliente cliente)
        {
            var validacionPrincipal = await _clienteRepository.ObtenerPorID(cliente.IdCliente);
            if (validacionPrincipal == null)
                return new ApiResponse<object> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR };

            if (cliente == null)
                return new ApiResponse<object> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR, Data = cliente };

            if (string.IsNullOrWhiteSpace(cliente.Nombres) || string.IsNullOrWhiteSpace(cliente.Apellidos) || string.IsNullOrWhiteSpace(cliente.Cedula))
                return new ApiResponse<object> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR, Data = cliente };

            var validacion = new Regex("^[a-zA-ZáéíóúÁÉÍÓÚñN\\s]+$");
            if (!validacion.IsMatch(cliente.Nombres) || !validacion.IsMatch(cliente.Apellidos))
                return new ApiResponse<object> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR, Data = cliente };

            var validacion2 = new Regex(@"^\d{10}$");
            if (!validacion2.IsMatch(cliente.Cedula))
                return new ApiResponse<object> { IsSuccess = false, Message = Mensajes.MESSAGE_REQUIRED_FIELDS, Data = cliente };


            var resultado = await _clienteRepository.EditarCliente(cliente);
            if (resultado > 0)
                return new ApiResponse<object> { IsSuccess = true, Message = Mensajes.MESSAGE_UPDATE };

            return new ApiResponse<object> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR, Data = cliente };
        }

        public async Task<ApiResponse<int>> EliminarCliente(int id)
        {
            try
            {
                var cliente = await _clienteRepository.ObtenerPorID(id);
                if (cliente == null)
                    return new ApiResponse<int> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR };
                var resultado = await _clienteRepository.EliminarCliente(id);
                if (resultado > 0)
                    return new ApiResponse<int> { IsSuccess = true, Message = Mensajes.MESSAGE_DELETE };

                return new ApiResponse<int> { IsSuccess = false, Message = Mensajes.MESSAGE_ERROR };
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                return new ApiResponse<int> { IsSuccess = false, Message = Mensajes.MESSAGE_DELETE_ERROR };
            }
        }
    }
}
