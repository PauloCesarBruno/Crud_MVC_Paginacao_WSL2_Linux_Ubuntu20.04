using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using Treinando_MVC_e_Sessao.Models;

namespace Treinando_MVC_e_Sessao.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login(int? Id)
        {
            if(Id != null)
            {
                if(Id == 0)
                {
                    HttpContext.Session.SetString("IdUsuario", String.Empty);
                    HttpContext.Session.SetString("NomeUsuaio", String.Empty);
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(ModelLogin login)
        {
            if(ModelState.IsValid)
            {
                Boolean loginOk = login.ValidarLogin();
                if(loginOk)
                {
                    HttpContext.Session.SetString("IdUsuario", login.Id);
                    HttpContext.Session.SetString("NomeUsuaio", login.Nome);
                    return RedirectToAction("index", "Home");
                }
                else
                {
                    TempData["ErrorMessage"] = "Email e/ou Senha Incorreto(s) !";
                }
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
