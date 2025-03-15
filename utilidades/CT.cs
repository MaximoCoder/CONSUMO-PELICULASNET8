namespace PeliculasWeb.utilidades
{
    public class CT
    {
        public static string UrlBaseApi = "https://localhost:7030/";
        public static string RutaCategoriasApi = UrlBaseApi + "api/v1/Categorias/";
        public static string RutaPeliculasApi = UrlBaseApi + "api/v1/Peliculas/";
        public static string RutaUsuariosApi = UrlBaseApi + "api/v1/Usuarios/";

        // Faltan rutas para buscar y filtrar peliculas por categorias
        public static string RutaPeliculasEnCategoriaApi = UrlBaseApi + "api/v1/Peliculas/GetPeliculasEnCategoria/";
        public static string RutaPeliculasBusquedaApi = UrlBaseApi + "api/v1/Peliculas/Buscar?nombre=";
    }
}
