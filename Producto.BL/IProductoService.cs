using Productos.Models;

namespace Productos.BL
{
    public interface IProductoService
    {
        IEnumerable<Producto> ObtenerTodos();

        Producto? ObtenerPorId(int id);

        void Crear(Producto producto);

        void Actualizar(Producto producto);

        void Eliminar(int id);
    }
}