using DataBaseFirst.Models;
using DataBaseFirst.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace APIBiblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<ApiResponse<List<Usuario>>> ListaUsuarios()
        {
            return await _usuarioService.ListaUsuarios();
        }

        [HttpGet("/nombres")]
        public async Task<ActionResult<ApiResponse<Autor>>> ObtenerUsuarioNombre(string nombres)
        {
            var usuario = await _usuarioService.ObtenerUsuarioNombre(nombres);
            return Ok(usuario);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Autor>>> ObtenerUsuarioID(int id)
        {
            var usuario = await _usuarioService.ObtenerUsuarioID(id);
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<object>>> RegistrarUsuario([FromBody] Usuario usuario)
        {
            var response = await _usuarioService.RegistrarUsuario(usuario);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPut]
        public async Task<ActionResult<ApiResponse<object>>> EditarUsuario([FromBody] Usuario usuario)
        {
            var response = await _usuarioService.EditarUsuario(usuario);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<int>>> EliminarUsuario(int id)
        {
            var response = await _usuarioService.EliminarUsuario(id);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
