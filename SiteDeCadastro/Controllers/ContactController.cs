using Microsoft.AspNetCore.Mvc;
using SiteDeCadastro.Filters;
using SiteDeCadastro.Models;
using SiteDeCadastro.Repositorio;
using System.Diagnostics;

namespace SiteDeCadastro.Controllers {
    
    [LoggedUserPage]
    public class ContactController : Controller
    {
        private readonly IContatoRepositorio _contatoRepositorio;
        public ContactController(IContatoRepositorio contatoRepositorio)
        {
            _contatoRepositorio = contatoRepositorio;

        }
        public IActionResult Index()
        {
            List<ContatoModel> tabelaContato = _contatoRepositorio.BuscarTabela();
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
                    _contatoRepositorio.Adicionar(contato);
                    //variavel temporaria, onde vai receber a mensagem e ser acessada dentro da view.
                    TempData["MensagemSucesso"] = "Contato cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }

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
