using Microsoft.AspNetCore.Mvc;
using SiteDeCadastro.Models;
using SiteDeCadastro.Repositorio;
using SiteDeCadastro.Helper;
using System.Net.Mail;

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

        public IActionResult ChangePass() { return View(); }

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

        [HttpPost]
        public IActionResult AlterPass(ChangePassModel changePass)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserModel usuarioLogado = _sessionUser.GetSessionUser();

                    if(usuarioLogado != null)
                    {
                        if (usuarioLogado.ConfirmPass(changePass.OldPass))
                        {
                            if(!usuarioLogado.ConfirmPass(changePass.NewPass))
                            {
                                usuarioLogado.Password = changePass.NewPass;

                                usuarioLogado.SetSenhaHash();

                                _userRepositorio.EditPass(usuarioLogado);

                                TempData["MensagemSucesso"] = "Senha alterada com sucesso!";
                                return RedirectToAction("Index", "Home");
                            }

                            TempData["MensagemErro"] = "A senha nova, não pode ser igual a atual!";
                            return View("ChangePass");

                        }

                        TempData["MensagemErro"] = "Senha atual inválida, verifique!";
                        return View("ChangePass");

                    }

                    TempData["MensagemErro"] = "Não foi possivel buscar sua sessão, tente novamente!";
                    //o certo era desconectar a _session e jogar ele na tela de login novamente!
                }
                return View("ChangePass");
            }
            catch (System.Exception er)
            {
                TempData["MensagemErro"] = "Não foi possivel alterar a senha, tente novamente!\n" +
                    $"Detalhe do Erro: {er.Message}";
                return View("ChangePass");
            }
        }

    }


}
