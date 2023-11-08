using Microsoft.AspNetCore.Mvc;
using SiteDeCadastro.Models;
using SiteDeCadastro.Repositorio;
using SiteDeCadastro.Helper;

namespace SiteDeCadastro.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserRepositorio _userRepositorio;
        private readonly ISessionUser _sessionUser;

        public LoginController(IUserRepositorio userRepositorio, ISessionUser sessionUser)
        {
            _userRepositorio = userRepositorio;
            _sessionUser = sessionUser;
        }
        public IActionResult Index()
        {
            //teste para ver se a sessao já esta logada, redicerionando para home
            if(_sessionUser.GetSessionUser() != null ) return RedirectToAction("Index", "Home");
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
                            _sessionUser.CreateSessionUser(usuario);
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
        public IActionResult Sair()
        {
            _sessionUser.RemoveSessionUser();
            return View("Index");
        }
    }


}
