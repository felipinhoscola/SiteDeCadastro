using Microsoft.AspNetCore.Mvc;
using SiteDeCadastro.Models;
using SiteDeCadastro.Repositorio;

namespace SiteDeCadastro.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginRepositorio _LoginRepositorio;

        public LoginController(ILoginRepositorio loginRepositorio)
        {
            _LoginRepositorio = loginRepositorio;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Entrar(LoginModel userLogin)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_LoginRepositorio.Login(userLogin))
                    {
                        TempData["MensagemSucesso"] = "Bem Vindo!!";
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["MensagemErro"] = "Verifique o usuario e senha!!";
                        return View("Index");
                    }
  
                }
                return View("Index");
            }
            catch (Exception er)
            {
                TempData["MensagemErro"] = "Não foi possivel realizar o login, tente novamente!\n" +
                    $"Detalhe do Erro: {er.Message}";
                return View("Index");
            }

        }
    }
}
