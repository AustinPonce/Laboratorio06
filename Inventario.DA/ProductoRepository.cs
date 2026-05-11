using Microsoft.EntityFrameworkCore;
using Productos.BL;
using Productos.Models;

namespace Productos.DA
{
    public class ProductoRepository
        : IProductoRepository
    {
        private readonly AppDbContext _dbContext;

        public ProductoRepository(
            AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Producto> ObtenerTodos()
        {
            try
            {
                return _dbContext.Productos
                    .AsNoTracking()
                    .OrderBy(producto => producto.Id)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Ocurrió un error al obtener los productos.",
                    ex);
            }
        }

        public Producto? ObtenerPorId(int id)
        {
            try
            {
                return _dbContext.Productos.Find(id);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Ocurrió un error al obtener el producto.",
                    ex);
            }
        }

        public void Agregar(Producto producto)
        {
            try
            {
                _dbContext.Productos.Add(producto);

                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Ocurrió un error al agregar el producto.",
                    ex);
            }
        }

        public void Actualizar(Producto producto)
        {
            try
            {
                Producto? productoExistente =
                    _dbContext.Productos.Find(producto.Id);

                if (productoExistente == null)
                {
                    throw new Exception(
                        "El producto no existe.");
                }

                productoExistente.Nombre =
                    producto.Nombre;

                productoExistente.Precio =
                    producto.Precio;

                productoExistente.Categoria =
                    producto.Categoria;

                productoExistente.Activo =
                    producto.Activo;

                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Ocurrió un error al actualizar el producto.",
                    ex);
            }
        }

        public void Eliminar(int id)
        {
            try
            {
                Producto? productoExistente =
                    _dbContext.Productos.Find(id);

                if (productoExistente == null)
                {
                    throw new Exception(
                        "El producto no existe.");
                }

                _dbContext.Productos.Remove(
                    productoExistente);

                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Ocurrió un error al eliminar el producto.",
                    ex);
            }
        }
    }
}