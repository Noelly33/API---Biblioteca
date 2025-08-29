using DataBaseFirst.Models;
using DataBaseFirst.Repository.InterfacesService;
using DataBaseFirst.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Utilities.Shared;

namespace APIBiblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamoController : ControllerBase
    {
        private readonly PrestamoService _prestamoService;

        public PrestamoController(PrestamoService prestamoService)
        {
            _prestamoService = prestamoService;
        }

        [HttpGet]
        public async Task<ApiResponse<List<Prestamo>>> ListaPrestamos()
        {
            return await _prestamoService.ListaPrestamos();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Prestamo>>> ObtenerPorID(int id)
        {
            var prestamo = await _prestamoService.ObtenerPorID(id);
            return Ok(prestamo);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<object>>> RegistrarPrestamo([FromBody] Prestamo prestamo)
        {
            var response = await _prestamoService.RegistrarPrestamo(prestamo);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
