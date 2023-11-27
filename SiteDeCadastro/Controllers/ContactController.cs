using Microsoft.AspNetCore.Mvc;
using SiteDeCadastro.Filters;
using SiteDeCadastro.Helper;
using SiteDeCadastro.Models;
using SiteDeCadastro.Repositorio;
using System.Diagnostics;

namespace SiteDeCadastro.Controllers {
    
    [LoggedUserPage]
    public class ContactController : Controller
    {
        private readonly IContatoRepositorio _contatoRepositorio;
        private readonly ISessionUser _sessionUser;
        public ContactController(IContatoRepositorio contatoRepositorio, ISessionUser sessionUser)
        {
            _contatoRepositorio = contatoRepositorio;
            _sessionUser = sessionUser;
        }
        public IActionResult Index()
        {
            UserModel userLogged = _sessionUser.GetSessionUser();
            List<ContatoModel> tabelaContato = _contatoRepositorio.BuscarTabela(userLogged.Id);
            return View(tabelaContato);
        }
        public IActionResult AddContact()
        {
            return View();
        }
        public IActionResult EditContact(int id)
        {
            return View(_contatoRepositorio.BuscarId(id));
        }

        public IActionResult DelContact(int id)
        {
            return View(_contatoRepositorio.BuscarId(id));
        }
        public IActionResult ApagarContato(int Id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool ret = _contatoRepositorio.Apagar(Id);
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
        [HttpPost]
        public IActionResult AddContact(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserModel userLogged = _sessionUser.GetSessionUser();
                    contato.UsuarioId = userLogged.Id;
                    _contatoRepositorio.Adicionar(contato);
                    //variavel temporaria, onde vai receber a mensagem e ser acessada dentro da view.
                    TempData["MensagemSucesso"] = "Contato cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }
                string mensagemErro = "Não foi possível cadastrar o contato. Erros de validação:\n";

                foreach (var erro in ModelState.Values.SelectMany(v => v.Errors))
                {
                    mensagemErro += $"{erro.ErrorMessage}\n";
                }

                TempData["MensagemErro"] = mensagemErro;
                return View(contato);
            }
            catch (System.Exception er)
            {
                TempData["MensagemErro"] = "Não foi possivel cadastrar o contato, tente novamente!\n" +
                    $"Detalhe do Erro: {er.Message}";
                return RedirectToAction("Index");
            }
            
        }

        [HttpPost]
        public IActionResult SaveContact(ContatoModel contato)
        {
            
            try
            {
                if (ModelState.IsValid)
                {
                    UserModel userLogged = _sessionUser.GetSessionUser();
                    contato.UsuarioId = userLogged.Id;
                    _contatoRepositorio.SaveEdit(contato);
                    TempData["MensagemSucesso"] = "Contato atualizado com sucesso!!";
                    return RedirectToAction("Index");
                }

                return View("EditContact", contato);
            }
            catch (System.Exception er)
            {
                TempData["MensagemErro"] = "Não foi possivel atualizar o contato, tente novamente!\n" +
                    $"Detalhe do Erro: {er.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
