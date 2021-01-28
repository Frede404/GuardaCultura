using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GuardaCultura.Models;

namespace GuardaCultura.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string erro="", RegistarPessoaViewModel PessoaInfo=null)
        {
            if (erro == "Login")
            {
                ViewBag.Erro = erro;
                ModelState.AddModelError("errologin", "Login invalido");
            }
            if (erro == "Registar")
            {
                ViewBag.Erro = erro;
                ModelState.AddModelError("erroregistar", "Registo invalido");
            }
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
