using Microsoft.AspNetCore.Mvc;
using Productos.BL;
using Productos.Models;

namespace Laboratorio04.Web.Controllers
{
    public class ProductosController : Controller
    {
        private readonly IProductoService _productoService;

        public ProductosController(
            IProductoService productoService)
        {
            _productoService = productoService;
        }

        public IActionResult Index()
        {
            IEnumerable<Producto> productos =
                _productoService.ObtenerTodos();

            return View(productos.ToList());
        }

        public IActionResult Detalles(int id)
        {
            Producto? producto =
                _productoService.ObtenerPorId(id);

            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        public IActionResult Crear()
        {
            return View(new Producto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Producto producto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(producto);
                }

                _productoService.Crear(producto);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(
                    string.Empty,
                    ex.Message);

                return View(producto);
            }
        }

        public IActionResult Editar(int id)
        {
            Producto? producto =
                _productoService.ObtenerPorId(id);

            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Producto producto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(producto);
                }

                _productoService.Actualizar(producto);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(
                    string.Empty,
                    ex.Message);

                return View(producto);
            }
        }

        public IActionResult Eliminar(int id)
        {
            Producto? producto =
                _productoService.ObtenerPorId(id);

            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Eliminar")]
        public IActionResult EliminarConfirmado(int id)
        {
            try
            {
                _productoService.Eliminar(id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(
                    string.Empty,
                    ex.Message);

                return View();
            }
        }
    }
}