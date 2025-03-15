using System.ComponentModel.DataAnnotations;

namespace PeliculasWeb.Models
{
    public class Categoria
    {
        // Automatico guardara la fecha con la fecha actual
        public Categoria()
        {
            FechaCreacion = DateTime.Now; 
        }
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
