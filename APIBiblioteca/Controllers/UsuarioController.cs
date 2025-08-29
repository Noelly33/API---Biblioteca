using DataBaseFirst.Helpers;
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
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;
        private readonly MenuService _menuService;
        private readonly Token _token;

        public UsuarioController(UsuarioService usuarioService, MenuService menuService, Token token)
        {
            _usuarioService = usuarioService;
            _menuService = menuService;
            _token = token;
        }

        [HttpGet]
        public async Task<ApiResponse<List<Usuario>>> ListaUsuarios()
        {
            return await _usuarioService.ListaUsuarios();
        }

        /*[HttpGet("/nombres")]
        public async Task<ActionResult<ApiResponse<Autor>>> ObtenerUsuarioNombre(string nombres)
        {
            var usuario = await _usuarioService.ObtenerUsuarioNombre(nombres);
            return Ok(usuario);
        }*/

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Autor>>> ObtenerUsuarioID(int id)
        {
            var usuario = await _usuarioService.ObtenerUsuarioID(id);
            return Ok(usuario);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<UsuarioRolDto>>> IniciarSesion([FromBody] LoginDto login)
        {
            var response = await _usuarioService.IniciarSesion(login);
            
            if(!response.IsSuccess)
                return Unauthorized(response);

            List<Menu> menus = await _menuService.ObtenerMenu(response.Data!.id_usuario);
            string tokenGenerado = _token.GenerarToken(response.Data, menus);

            return Ok(new ApiResponse<object> { IsSuccess = true, Message = Mensajes.MESSAGE_LOGIN_TOKEN, Data = new {token = tokenGenerado}});
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
