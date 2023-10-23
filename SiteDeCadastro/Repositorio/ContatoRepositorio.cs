using Microsoft.EntityFrameworkCore;
using SiteDeCadastro.Data;
using SiteDeCadastro.Models;

namespace SiteDeCadastro.Repositorio
{
    public class ContatoRepositorio : IContatoRepositorio
    {
        private readonly BancoContext _bancoContext;
        public ContatoRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }
        public List<ContatoModel> BuscarTabela()
        {
            return _bancoContext.Contatos.ToList();
        }
        public ContatoModel Adicionar(ContatoModel contato)
        {
            _bancoContext.Contatos.Add(contato);
            _bancoContext.SaveChanges();

            return contato;
        }

        public ContatoModel BuscarId(int id)
        {
            return _bancoContext.Contatos.FirstOrDefault(x => x.Id == id);
        }

        public ContatoModel SaveEdit(ContatoModel contato)
        {
            ContatoModel contatoBD = BuscarId(contato.Id);

            if (contatoBD == null) throw new System.Exception("Houve um erro na atualização do contato");

            contatoBD.Nome = contato.Nome;
            contatoBD.Email = contato.Email;
            contatoBD.Celular = contato.Celular;

            _bancoContext.Contatos.Update(contatoBD);
            _bancoContext.SaveChanges();

            return contatoBD;
        }

        public bool Apagar(int Id)
        {
            ContatoModel contatoBD = BuscarId(Id);

            if (contatoBD == null) throw new System.Exception("Houve um erro na exclusão do contato");

            _bancoContext.Contatos.Remove(contatoBD);
            _bancoContext.SaveChanges();
            return true;

        }
    }
}
