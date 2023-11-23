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
        private readonly IEmail _email;

        public LoginController(IUserRepositorio userRepositorio, ISessionUser sessionUser, IEmail email)
        {
            _userRepositorio = userRepositorio;
            _sessionUser = sessionUser;
            _email = email;
        }
        public IActionResult Index()
        {
            //teste para ver se a sessao já esta logada, redicerionando para home
            if(_sessionUser.GetSessionUser() != null ) return RedirectToAction("Index", "Home");
            return View();
        }
        public IActionResult ResetPass() { return View(); }

        public IActionResult Sair()
        {
            _sessionUser.RemoveSessionUser();
            return View("Index");
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
        [HttpPost]
        public IActionResult SendLinktoResetPass(ResetPassModel resetPassModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserModel usuario = _userRepositorio.BuscaEmailELogin(resetPassModel.Email, resetPassModel.Login);

                    if (usuario != null)
                    {
                        string newPass = usuario.GenerateNewPass();

                        string msg = $"Sua senha foi alterada, nova senha é: {newPass}";

                        bool emailEnviado = _email.Enviar(usuario.Email, "Sistema de Contatos - Nova senha", msg);

                        if (emailEnviado)
                        {
                            //fazer metodo que altera senha
                            _userRepositorio.EditPass(usuario);
                            TempData["MensagemSucesso"] = $"Um E-mail foi enviado com a nova senha.";
                        }
                        else
                        {
                            TempData["MensagemErro"] = "Não conseguimos enviur sua senha. Tente Novamente";
                        }


                        return View("Index");
                    }

                    TempData["MensagemErro"] = "Dados para redefinir a senha estão incorretos.";
                }
                return View("ResetPass");
            }
            catch (Exception er)
            {
                TempData["MensagemErro"] = "Não foi possivel redefinir a senha, tente novamente!\n" +
                    $"Detalhe do Erro: {er.Message}";
                return View("ResetPass");
            }
        }


    }


}
