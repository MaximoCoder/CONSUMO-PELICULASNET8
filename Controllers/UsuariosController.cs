using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeliculasWeb.Models;
using PeliculasWeb.Repositorio.IRepositorio;
using PeliculasWeb.utilidades;

namespace PeliculasWeb.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {
        private readonly IUsuarioRepositorio _repoUsuario;

        public UsuariosController(IUsuarioRepositorio repoUsuario)
        {
            _repoUsuario = repoUsuario;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new Usuario() { });
        }

        [HttpGet]
        public async Task<IActionResult> GetTodosUsuarios()
        {
            return Json(new { data = await _repoUsuario.GetTodoAsync(CT.RutaUsuariosApi) });
        }
    }
}
