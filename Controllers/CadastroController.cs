using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Treinando_MVC_e_Sessao.Models;

namespace Treinando_MVC_e_Sessao.Controllers
{
    public class CadastroController : Controller
    {
        [HttpGet]
        public IActionResult Cadastro(int? Id)
        {
            if(Id != null)
            {
                ViewBag.User = new ModelCadastro();
            }
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(ModelCadastro cadastro)
        {
            if(ModelState.IsValid)
            {
                cadastro.InserirUsuario();
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
    }
}
