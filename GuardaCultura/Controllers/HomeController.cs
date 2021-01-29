using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GuardaCultura.Models;
using GuardaCultura.Data;

namespace GuardaCultura.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly GuardaCulturaContext _context;

        public HomeController(ILogger<HomeController> logger, GuardaCulturaContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(string erro="", string emaillogin="", string emailregisto = "", string nome = "", string data_nasc = "", string sexo = "", string nacionalidade = "")
        {
            if (erro == "Login")
            {
                ViewBag.Erro = erro;
                ViewBag.Email = emaillogin;
                ModelState.AddModelError("errologin", "Login invalido");
            }
            if (erro == "Registar")
            {
                ViewBag.Erro = erro;
                ViewBag.Email = emailregisto;
                ViewBag.Nome = nome;
                ViewBag.Data_Nasc = data_nasc;
                ViewBag.Sexo = sexo;
                ViewBag.Nacionalidade=nacionalidade;
                ModelState.AddModelError("erroregistar", "Registo invalido");
            }
            return View();
        }

        public IActionResult BackOffice()
        {
            return View(
                new ListaFotografias
                {
                    Fotografias=_context.Fotografia
                }
            );
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
