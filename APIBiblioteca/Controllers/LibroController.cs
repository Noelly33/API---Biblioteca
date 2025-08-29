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
    public class LibroController : ControllerBase
    {
        private readonly LibroService _libroService;

        public LibroController(LibroService libroService)
        {
            _libroService = libroService;
        }

        [HttpGet]
        public async Task<ApiResponse<List<Libro>>> ListaLibros()
        {
            return await _libroService.ListaLibros();
        }

        [HttpGet("titulo")]
        public async Task<ActionResult<ApiResponse<Libro>>> BuscarPorTitulo(string titulo)
        {
            var libro = await _libroService.BuscarPorTitulo(titulo);
            return Ok(libro);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Libro>>> BuscarPorId(int id)
        {
            var libro = await _libroService.BuscarPorId(id);
            return Ok(libro);
        }

        [HttpGet("libroPrestamo")]
        public async Task<ActionResult<ApiResponse<LibroPrestamoDto>>> ObtenerLibroPrestamo(string libroPrestamo)
        {
            var libro = await _libroService.ObtenerLibroPrestamo(libroPrestamo);
            return Ok(libro);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<object>>> RegistrarLibro([FromBody] Libro libro)
        {
            var response = await _libroService.RegistrarLibro(libro);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPut]
        public async Task<ActionResult<ApiResponse<object>>> EditarLibro([FromBody] Libro libro)
        {
            var response = await _libroService.EditarLibro(libro);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<int>>> EliminarLibro(int id)
        {
            var response = await _libroService.EliminarLibro(id);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
