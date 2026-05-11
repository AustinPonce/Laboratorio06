using System;
using System.ComponentModel.DataAnnotations;

namespace Productos.Models
{
    public class Producto
    {
        public int Id { get; set; }

        [Required(
            ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(
            100,
            ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(
            ErrorMessage = "El precio es obligatorio.")]
        [Range(
            typeof(decimal),
            "1",
            "9999999",
            ErrorMessage = "El precio debe estar entre 1 y 9.999.999.")]
        public decimal Precio { get; set; }

        [Required(
            ErrorMessage = "La categoría es obligatoria.")]
        public string Categoria { get; set; } = string.Empty;

        public bool Activo { get; set; } = true;

        public DateTime FechaIngreso { get; set; }
            = DateTime.Now;
    }
}