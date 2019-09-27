using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Devops_Web.Models;
using Devops_Web.Models.DB;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Devops_Web.Services;

namespace Devops_Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly PlacesDBContext placesDB;

        public AccountController(PlacesDBContext placesDBContext)
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

        public IActionResult Login()
        {
            var model = new LoginViewModel { Login = new Login(), Cadastro = new Cadastro() };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ValidarLogin(Login login)
        {
            if (ModelState.IsValid)
            {
                login.Senha = md5Hash.GetMd5Hash(login.Senha);

                var user = placesDB.Usuarios.Where(x => (x.Email == login.EmailouCelular || x.Celular.Contains(login.EmailouCelular)) && x.Senha == login.Senha).FirstOrDefault();

                if (user != null)
                {                   
                    var usuario = new Usuario()
                    {
                        Id = user.Id,
                        Hash = user.Hash,
                        Nome = user.Nome,
                        SobreNome = user.SobreNome,
                        DataNascimento = user.DataNascimento,
                        Email = user.Email,
                        Celular = user.Celular,
                    };

                    UsuarioAutenticado.IsUserLoggedIn = true;
                    UsuarioAutenticado.UserHash = user.Hash;
                    UsuarioAutenticado.UserEmailouCelular = login.EmailouCelular;

                    return RedirectToAction("IndexPosLogin", "Home", usuario);
                }
                else if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Nome de Usuário ou senha inválidos!");
                }
            }

            var model = new LoginViewModel { Login = login, Cadastro = new Cadastro() };

            return View("Login", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Cadastro(Cadastro cadastro)
        {
            var model = new LoginViewModel { Login = new Login(), Cadastro = cadastro };

            if (ModelState.IsValid)
            {
                var Usuarios = placesDB.Usuarios.Where(x => (x.Email == cadastro.EmailouCelularCadastro || x.Celular.Contains(cadastro.EmailouCelularCadastro))).FirstOrDefault();

                if (Usuarios == null)
                {
                    var usuario = new Usuarios();
                    cadastro.Senha = md5Hash.GetMd5Hash(cadastro.Senha);

                    //criada para não considera o + do celular Ex: +5511987654321
                    var EmailouCelularCadastroNoPlus = cadastro.EmailouCelularCadastro;
                    EmailouCelularCadastroNoPlus.Replace("+", string.Empty);

                    if (cadastro.EmailouCelularCadastro.Contains("@"))
                    {
                        usuario = new Usuarios
                        {
                            Nome = cadastro.Nome,
                            SobreNome = cadastro.Sobrenome,
                            DataNascimento = cadastro.DataNascimento.Value,
                            Email = cadastro.EmailouCelularCadastro,
                            Senha = cadastro.Senha
                        };
                    }
                    else if (EmailouCelularCadastroNoPlus.All(char.IsDigit))
                    {
                        usuario = new Usuarios
                        {
                            Nome = cadastro.Nome,
                            SobreNome = cadastro.Sobrenome,
                            DataNascimento = cadastro.DataNascimento.Value,
                            Celular = cadastro.EmailouCelularCadastro,
                            Senha = cadastro.Senha
                        };
                    }
                    else
                    {
                        ModelState.AddModelError("Cadastro.EmailouCelularCadastro", "Por favor preecha um Email ou Celular válido!");

                        return View("Login", model);
                    }

                    placesDB.Add(usuario);
                    placesDB.SaveChanges();

                    var newUserId = placesDB.Usuarios.FirstOrDefault(x => (x.Email == cadastro.EmailouCelularCadastro || x.Celular.Contains(cadastro.EmailouCelularCadastro))).Id.ToString();

                    var newUserIdToHash = md5Hash.GetMd5Hash(newUserId);

                    placesDB.Usuarios.FirstOrDefault(x => (x.Email == cadastro.EmailouCelularCadastro || x.Celular.Contains(cadastro.EmailouCelularCadastro))).Hash = newUserIdToHash;
                    placesDB.SaveChanges();

                    model.Cadastro.CadastroCheck = false;

                    ViewBag.CadastroSucesso = "Cadastro realizado com sucesso!";

                    return View("Login", model);
                }
                else if (Usuarios != null)
                {
                    ModelState.AddModelError("Cadastro.EmailouCelularCadastro", "Email ou celular ja cadastrados!");

                    return View("Login", model);
                }
            }

            return View("Login", model);
        }
    }
}