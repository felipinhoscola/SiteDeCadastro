using Microsoft.AspNetCore.Mvc;
using SiteDeCadastro.Filters;
using SiteDeCadastro.Models;
using SiteDeCadastro.Repositorio;
using System.Collections.Generic;

namespace SiteDeCadastro.Controllers
{
    [LoggedAdminPage]
    public class UserController : Controller
    {
        private readonly IUserRepositorio _UserRepositorio;
        public UserController(IUserRepositorio userRepositorio)
        {
            _UserRepositorio = userRepositorio;
        }
        public IActionResult Index()
        {
            List<UserModel> tabelaUsuario = _UserRepositorio.BuscarUsers();
            return View(tabelaUsuario);
        }

        public IActionResult AddUser()
        {
            return View();
        }

        public IActionResult EditUser(int id)
        {
            return View(_UserRepositorio.BuscarId(id));
        }
        public IActionResult DelUser(int id)
        {
            return View(_UserRepositorio.BuscarId(id));
        }

        [HttpPost]
        public IActionResult AddUser(UserModel Usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Usuario.DateCad = DateTime.Now;//Setando data atual
                    Usuario.LastAtt = DateTime.Now;

                    _UserRepositorio.AddUser(Usuario);
                    TempData["MensagemSucesso"] = "Usuário Cadastrado!";
                    return RedirectToAction("Index");
                }
                return View(Usuario);
            }
            catch (System.Exception er)
            {
                TempData["MensagemErro"] = "Não foi possivel cadastrar o usuário, tente novamente!\n" +
                    $"Detalhe do Erro: {er.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult EditUser(UserNoPassModel UserNoPass)
        {
            try
            {
                UserModel usuario = null;
                if (ModelState.IsValid)
                {
                    usuario = new UserModel()
                    {
                        Id = UserNoPass.Id,
                        Name = UserNoPass.Name,
                        Login = UserNoPass.Login,
                        Email = UserNoPass.Email,
                        Perfil = UserNoPass.Perfil
                    };

                    usuario.LastAtt = DateTime.Now;

                    usuario = _UserRepositorio.EditUser(usuario);
                    TempData["MensagemSucesso"] = "Usuário Atualizado!";
                    return RedirectToAction("Index");
                }
                return View(usuario);
            }
            catch (System.Exception er)
            {
                TempData["MensagemErro"] = "Não foi possivel atualizar o usuário, tente novamente!\n" +
                    $"Detalhe do Erro: {er.Message}";
                return RedirectToAction("Index");
            }
        }
        public IActionResult ConfirmDelUser(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool ret = _UserRepositorio.ConfirmDelUser(id);
                    if (ret)
                    {
                        TempData["MensagemSucesso"] = "Contato apagado com sucesso!!";
                    }
                    else
                    {
                        TempData["MensagemErro"] = "Não foi possivel apagar o contato, tente novamente!!";
                    }

                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch (System.Exception er)
            {
                TempData["MensagemErro"] = "Não foi possivel apagar o contato, tente novamente!\n" +
                    $"Detalhe do Erro: {er.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
