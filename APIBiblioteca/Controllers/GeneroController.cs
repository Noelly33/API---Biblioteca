using DataBaseFirst.Models;
using DataBaseFirst.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Utilities.Shared;

namespace APIBiblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneroController : ControllerBase
    {
        private readonly GeneroService _generoService;

        public GeneroController(GeneroService generoService)
        {
            _generoService = generoService;
        }

        [HttpGet]
        public async Task<ApiResponse<List<Genero>>> ListaGeneros()
        {
            return await _generoService.ListaGeneros();
        }

        [HttpGet("generoNombre")]
        public async Task<ActionResult<ApiResponse<Genero>>> ObtenerPorNombre(string generoNombre)
        {
            var genero = await _generoService.ObtenerPorNombre(generoNombre);
            return Ok(genero);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Genero>>> ObtenerPorID(int id)
        {
            var genero = await _generoService.ObtenerPorID(id);
            return Ok(genero);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<object>>> RegistrarGenero([FromBody] Genero genero)
        {
            var response = await _generoService.RegistrarGenero(genero);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPut]
        public async Task<ActionResult<ApiResponse<object>>> EditarGenero([FromBody] Genero genero)
        {
            var response = await _generoService.EditarGenero(genero);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<int>>> EliminarGenero(int id)
        {
            var response = await _generoService.EliminarGenero(id);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
