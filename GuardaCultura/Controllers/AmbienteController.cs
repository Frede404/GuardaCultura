using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GuardaCultura.Controllers
{
    public class AmbienteController : Controller
    {
        public IActionResult Ambiente()
        {
            return View();
        }
        public IActionResult QualidadeAr()
        {
            return View();
        }
        public IActionResult Paisagens()
        {
            return View();
        }
    }
}
