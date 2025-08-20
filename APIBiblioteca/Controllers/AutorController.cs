using Azure;
using DataBaseFirst.Models;
using DataBaseFirst.Models.Dto;
using DataBaseFirst.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Utilities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace APIBiblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly AutorService _autorService;

        public AutorController(AutorService autorService) 
        { 
            _autorService = autorService;
        }

        [HttpGet]
        public async Task<ApiResponse<List<Autor>>> ListaAutores()
        {
            return await _autorService.ListaAutores();  
        }

        [HttpGet("/nombre")]
        public async Task<ActionResult<ApiResponse<Autor>>> ObtenerAutorNombre(string nombre)
        {
            var autor = await _autorService.ObtenerAutorNombre(nombre);
            return Ok(autor);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Autor>>> ObtenerAutorID(int id)
        {
            var autor = await _autorService.ObtenerAutorID(id);
            return Ok(autor);
        }

        [HttpGet("/autorLibro")]
        public async Task<ActionResult<ApiResponse<AutorLibroDto>>> ObtenerAutorLibro(string nombre)
        {
            var autorLibro = await _autorService.ObtenerAutorLibro(nombre);
            return Ok(autorLibro);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<object>>> RegistrarAutor([FromBody] Autor autor)
        {
           var response = await _autorService.RegistrarAutor(autor);
           return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPut]
        public async Task<ActionResult<ApiResponse<object>>> EditarAutor([FromBody] Autor autor)
        {
            var response = await _autorService.EditarAutor(autor);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<int>>> EliminarAutor(int id)
        {
            var response = await _autorService.EliminarAutor(id);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

    }
}
