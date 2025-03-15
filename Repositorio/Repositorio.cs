using Newtonsoft.Json;
using PeliculasWeb.Repositorio.IRepositorio;
using System.Collections;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;

namespace PeliculasWeb.Repositorio
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        // Injectar dependencias http
        private readonly IHttpClientFactory _clientFactory;

        // Constructor
        public Repositorio(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<bool> ActualizarAsync(string url, T itemActualizar, string token = "")
        {
            // Especificamos el metodo
            var peticion = new HttpRequestMessage(HttpMethod.Patch, url);
            if (itemActualizar != null) {
                // Especificamos como enviaremos los datos
                peticion.Content = new StringContent(JsonConvert.SerializeObject(itemActualizar),
                    Encoding.UTF8, "application/json"
                    );
            }
            else
            {
                return false;
            }
            // Enviar peticion
            var cliente = _clientFactory.CreateClient();

            //Aquí validamos el token
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }

            // Asigna el token al encabezado de autorización
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage respuesta = await cliente.SendAsync(peticion);

            //Validar si se actualizo y retorna boleano
            if (respuesta.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> ActualizarPeliculaAsync(string url, T peliculaActualizar, string token = "")
        {
            // Especificamos el metodo
            var peticion = new HttpRequestMessage(HttpMethod.Patch, url);
            var multipartContent = new MultipartFormDataContent(); // utilizado para cargar archivos junto a datos
            if (peliculaActualizar != null)
            {
                // Especificamos como enviaremos los datos
                // Serializar cada parte de pelicula
                foreach(var property in typeof(T).GetProperties())
                {
                    var value = property.GetValue(peliculaActualizar);
                    if(value != null)
                    {
                        // Separamos en archivo y data(string)
                        if(property.PropertyType == typeof(IFormFile))
                        {
                            // Manejar el archivo
                            var file = value as IFormFile;
                            if (file != null)
                            {
                                var streamContent = new StreamContent(file.OpenReadStream());
                                streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
                                multipartContent.Add(streamContent, property.Name, file.FileName);
                            }
                        }
                        else
                        {
                            // Manejar los string
                            var stringContent = new StringContent(value.ToString());
                            multipartContent.Add(stringContent, property.Name);
                        }
                    }
                }
            }
            else
            {
                return false;
            }

            // Enviar peticion
            peticion.Content = multipartContent;
            var cliente = _clientFactory.CreateClient();

            //Aquí validamos el  token
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }

            // Asigna el token al encabezado de autorización
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage respuesta = await cliente.SendAsync(peticion);

            //Validar si se actualizo y retorna boleano
            if (respuesta.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> BorrarAsync(string url, int Id, string token = "")
        {
            // Especificamos el metodo
            var peticion = new HttpRequestMessage(HttpMethod.Delete, url + Id);
           
            // Enviar peticion
            var cliente = _clientFactory.CreateClient();
            //Aquí validamos el  token
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }

            // Asigna el token al encabezado de autorización
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            // Ahora si enviamos peticion
            HttpResponseMessage respuesta = await cliente.SendAsync(peticion);

            //Validar si se actualizo y retorna boleano
            if (respuesta.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable> Buscar(string url, string nombre)
        {
            var peticion = new HttpRequestMessage(HttpMethod.Get, url + nombre);

            var cliente = _clientFactory.CreateClient();

            HttpResponseMessage respuesta = await cliente.SendAsync(peticion);
            if (respuesta.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await respuesta.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<T>>(jsonString);
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> CrearAsync(string url, T itemCrear, string token = "")
        {
            // Especificamos el metodo
            var peticion = new HttpRequestMessage(HttpMethod.Post, url);
            if (itemCrear != null)
            {
                // Especificamos como enviaremos los datos
                peticion.Content = new StringContent(JsonConvert.SerializeObject(itemCrear),
                    Encoding.UTF8, "application/json"
                    );
            }
            else
            {
                return false;
            }
            // Enviar peticion
            var cliente = _clientFactory.CreateClient();

            //Aquí validamos el  token
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }

            // Asigna el token al encabezado de autorización
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage respuesta = await cliente.SendAsync(peticion);

            //Validar si se actualizo y retorna boleano
            if (respuesta.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> CrearPeliculaAsync(string url, T peliculaCrear, string token = "")
        {
            // Especificamos el metodo
            var peticion = new HttpRequestMessage(HttpMethod.Post, url);
            var multipartContent = new MultipartFormDataContent(); // utilizado para cargar archivos junto a datos
            if (peliculaCrear != null)
            {
                // Especificamos como enviaremos los datos
                // Serializar cada parte de pelicula
                foreach (var property in typeof(T).GetProperties())
                {
                    var value = property.GetValue(peliculaCrear);
                    if (value != null)
                    {
                        // Separamos en archivo y data(string)
                        if (property.PropertyType == typeof(IFormFile))
                        {
                            // Manejar el archivo
                            var file = value as IFormFile;
                            if (file != null)
                            {
                                var streamContent = new StreamContent(file.OpenReadStream());
                                streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
                                multipartContent.Add(streamContent, property.Name, file.FileName);
                            }
                        }
                        else
                        {
                            // Manejar los string
                            var stringContent = new StringContent(value.ToString());
                            multipartContent.Add(stringContent, property.Name);
                        }
                    }
                }
            }
            else
            {
                return false;
            }

            // Enviar peticion
            peticion.Content = multipartContent;
            var cliente = _clientFactory.CreateClient();

            //Aquí validamos el token
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }

            // Asigna el token al encabezado de autorización
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage respuesta = await cliente.SendAsync(peticion);

            //Validar si se actualizo y retorna boleano
            if (respuesta.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<T> GetAsync(string url, int Id)
        {
            var peticion = new HttpRequestMessage(HttpMethod.Get, url + Id);

            var cliente = _clientFactory.CreateClient();

            HttpResponseMessage respuesta = await cliente.SendAsync(peticion);
            if (respuesta.StatusCode == System.Net.HttpStatusCode.OK) {
                var jsonString = await respuesta.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(jsonString);
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable> GetPeliculasEnCategoriaAsync(string url, int categoriaId)
        {
            var peticion = new HttpRequestMessage(HttpMethod.Get, url + categoriaId);

            var cliente = _clientFactory.CreateClient();

            HttpResponseMessage respuesta = await cliente.SendAsync(peticion);
            if (respuesta.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await respuesta.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<T>>(jsonString);
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable> GetTodoAsync(string url)
        {
            var peticion = new HttpRequestMessage(HttpMethod.Get, url);

            var cliente = _clientFactory.CreateClient();

            HttpResponseMessage respuesta = await cliente.SendAsync(peticion);
            if (respuesta.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await respuesta.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<T>>(jsonString);
            }
            else
            {
                return null;
            }
        }
    }
}
