using Microsoft.AspNetCore.Mvc;
using System;
using Treinando_MVC_e_Sessao.Models;

namespace Treinando_MVC_e_Sessao.Controllers
{
    public class ClienteController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.ListarClientes = new ModelCliente().ListarTodosClientes();
            return View();
        }

        [HttpGet]
        public IActionResult Cadastro(int? Id)
        {
            if (Id != null)
            {
                ViewBag.Cliente = new ModelCliente().RetornarCliente(Id);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(ModelCliente cadastro)
        {
            if (ModelState.IsValid)
            {
                cadastro.Gravar();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Excluir(int? Id)
        {
            ViewData["IdExcluir"] = Id;
            return View();
        }

        public IActionResult ExcluirCliente(int? Id)
        {
            try
            {
                new ModelCliente().Excluir(Id);
                return View();
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Filtro()
        {
            ViewBag.ListarClientes = new ModelCliente().ListarTodosClientes();
            return View();
        }

        [HttpPost]
        public IActionResult Filtro(ModelCliente filtro)
        {
            try
            {
                String cpf = filtro.CPF.ToString();
                ViewBag.ListarClientes = new ModelCliente().ListarTodosClientesCPF(cpf);
                return View();
            }
            catch (Exception)
            {
                return View();
            }
        }
    }
}
