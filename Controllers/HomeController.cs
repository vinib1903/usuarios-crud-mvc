using Microsoft.AspNetCore.Mvc;
using MvcUsers.Models;
using System.Diagnostics;

namespace MvcUsers.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detalhes()
        {
            return View("Detalhes");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (username == "SISTEMA" && password == "candidato123")
            {
                HttpContext.Session.SetString("User", "Authenticated");
                return RedirectToAction("Index", "Usuario");
            }

            ViewBag.ErrorMessage = "Usuário ou senha inválidos.";
            return View();
        }
    }
}
