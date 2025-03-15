using System.Collections;

namespace PeliculasWeb.Repositorio.IRepositorio
{
    public interface IRepositorio<T> where T : class
    {
        //Obtener lista total
        Task<IEnumerable> GetTodoAsync(string url);

        //Obtener Peliculas en una categoria
        Task<IEnumerable> GetPeliculasEnCategoriaAsync(string url, int categoriaId);

        //Buscador
        Task<IEnumerable> Buscar(string url, string nombre);

        // Traer un registro por id
        Task<T> GetAsync(string url, int Id);

        // Crear categoria o usuario
        Task<bool> CrearAsync(string url, T itemCrear, string token);

        // Crear pelicula
        Task<bool> CrearPeliculaAsync(string url, T peliculaCrear, string token);

        // Actualizar categoria o usuario
        Task<bool> ActualizarAsync(string url, T itemActualizar, string token);

        // Actualizar pelicula
        Task<bool> ActualizarPeliculaAsync(string url, T peliculaActualizar, string token);

        // Borrar
        Task<bool> BorrarAsync(string url, int Id, string token);
    }
}
