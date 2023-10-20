using Microsoft.AspNetCore.Mvc;
using SiteDeCadastro.Models;
using SiteDeCadastro.Repositorio;
using System.Diagnostics;

namespace SiteDeCadastro.Controllers {

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
            _contatoRepositorio.Apagar(Id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult AddContact(ContatoModel contato)
        {
            _contatoRepositorio.Adicionar(contato);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult SaveContact(ContatoModel contato)
        {
            _contatoRepositorio.SaveEdit(contato);
            return RedirectToAction("Index");
        }
    }
}
