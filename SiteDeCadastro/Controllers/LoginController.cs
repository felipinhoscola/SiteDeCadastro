using Microsoft.AspNetCore.Mvc;
using SiteDeCadastro.Models;
using SiteDeCadastro.Repositorio;

namespace SiteDeCadastro.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserRepositorio _userRepositorio;

        public LoginController(IUserRepositorio userRepositorio)
        {
            _userRepositorio = userRepositorio;
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
                    UserModel usuario = _userRepositorio.BuscaLogin(userLogin.Login);

                    if (usuario != null)
                    {
                        if (usuario.ConfirmPass(userLogin.Password))
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        TempData["MensagemErro"] = "Senha do usuário é inválida. Por favor, tente novamente.";
                        return View("Index");
                    } 

                    TempData["MensagemErro"] = "Usuário e/ou senha inválido(s). Por favor, tente novamente.";
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
