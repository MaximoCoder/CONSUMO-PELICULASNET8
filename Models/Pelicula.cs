using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeliculasWeb.Models
{
    public class Pelicula
    {
        // Automatico guardara la fecha con la fecha actual
        public Pelicula()
        {
            FechaCreacion = DateTime.Now;
        }
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "La descripcion es obligatorio")]
        public string Descripcion { get; set; }
        public string Duracion { get; set; }
        public IFormFile Imagen { get; set; } //Para subir la imagen de la película
        public string RutaIMagen { get; set; } // Nueva propiedad para la URL de la imagen

        public enum TipoClasificacion { Siete, Trece, Dieciseis, Diechiocho }
        public TipoClasificacion Clasificacion { get; set; }
        public DateTime? FechaCreacion { get; set; }

        public int categoriaId { get; set; }
        public Categoria Categoria { get; set; }

    }
}
