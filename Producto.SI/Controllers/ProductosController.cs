using Microsoft.AspNetCore.Mvc;
using Productos.BL;
using System.Collections.Generic;
using ProductoModel = Productos.Models.Producto;

namespace Productos.SI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosApiController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductosApiController(
            IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            IEnumerable<ProductoModel> productos =
                _productoService.ObtenerTodos();

            return Ok(productos);
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerPorId(int id)
        {
            ProductoModel? producto =
                _productoService.ObtenerPorId(id);

            if (producto == null)
            {
                return NotFound();
            }

            return Ok(producto);
        }

        [HttpPost]
        public IActionResult Crear(
            [FromBody] ProductoModel producto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _productoService.Crear(producto);

            return Ok(producto);
        }

        [HttpPut]
        public IActionResult Actualizar(
            [FromBody] ProductoModel producto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _productoService.Actualizar(producto);

            return Ok(producto);
        }

        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            _productoService.Eliminar(id);

            return Ok();
        }
    }
}