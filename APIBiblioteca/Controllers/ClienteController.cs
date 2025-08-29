using DataBaseFirst.Models;
using DataBaseFirst.Models.Dto;
using DataBaseFirst.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Utilities.Shared;

namespace APIBiblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteService _clienteService;

        public ClienteController(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<ApiResponse<List<Cliente>>> ListaClientes()
        {
            return await _clienteService.ListaClientes();
        }

        [HttpGet("nombres")]
        public async Task<ActionResult<ApiResponse<Cliente>>> ObtenerPorNombre(string nombres)
        {
            var cliente = await _clienteService.ObtenerPorNombre(nombres);
            return Ok(cliente);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Cliente>>> ObtenerPorID(int id)
        {
            var cliente = await _clienteService.ObtenerPorID(id);
            return Ok(cliente);
        }

        [HttpGet("ClienteLibroPrestamo")]
        public async Task<ActionResult<ApiResponse<ClienteLibroPrestamoDto>>> ObtenerClienteLibroPrestamo(string ClienteLibroPrestamo)
        {
            var clienteLibro = await _clienteService.ObtenerClienteLibroPrestamo(ClienteLibroPrestamo);
            return Ok(clienteLibro);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<object>>> RegistrarCliente([FromBody] Cliente cliente)
        {
            var response = await _clienteService.RegistrarCliente(cliente);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPut]
        public async Task<ActionResult<ApiResponse<object>>> EditarCliente([FromBody] Cliente cliente)
        {
            var response = await _clienteService.EditarCliente(cliente);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<int>>> EliminarCliente(int id)
        {
            var response = await _clienteService.EliminarCliente(id);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
