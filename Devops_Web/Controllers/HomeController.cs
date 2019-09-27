using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Devops_Web.Models;
using Devops_Web.Models.DB;

namespace Devops_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly PlacesDBContext placesDB;

        public HomeController(PlacesDBContext placesDBContext)
        {
            try
            {
                this.placesDB = placesDBContext;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
        }

        public IActionResult Index()
        {
            if (!UsuarioAutenticado.IsUserLoggedIn || UsuarioAutenticado.UserHash == string.Empty || UsuarioAutenticado.UserEmailouCelular == string.Empty)
            {
                return RedirectToAction("Login", "Account");
            }

            var Usuarios = placesDB.Usuarios.Where(x => (x.Email == UsuarioAutenticado.UserEmailouCelular || x.Celular.Contains(UsuarioAutenticado.UserEmailouCelular)) && x.Hash == UsuarioAutenticado.UserHash).ToList();
            Usuario usuario;

            if (Usuarios.Count == 0)
            {
                return RedirectToAction("Login", "Account");
            }

            var user = Usuarios.FirstOrDefault(x => (x.Email == UsuarioAutenticado.UserEmailouCelular || x.Celular.Contains(UsuarioAutenticado.UserEmailouCelular)) && x.Hash == UsuarioAutenticado.UserHash);

            usuario = new Usuario()
            {
                Id = user.Id,
                Hash = user.Hash,
                Nome = user.Nome,
                SobreNome = user.SobreNome,
                DataNascimento = user.DataNascimento,
                Email = user.Email,
                Celular = user.Celular,
            };

            return View(usuario);
        }

        public IActionResult IndexPosLogin(Usuario usuario)
        {
            if (!UsuarioAutenticado.IsUserLoggedIn || UsuarioAutenticado.UserHash == string.Empty || UsuarioAutenticado.UserEmailouCelular == string.Empty)
            {
                return RedirectToAction("Login", "Account");
            }

            return View("Index", usuario);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
