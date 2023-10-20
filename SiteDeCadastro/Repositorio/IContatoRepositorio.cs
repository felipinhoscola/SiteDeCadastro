using SiteDeCadastro.Models;

namespace SiteDeCadastro.Repositorio
{
    public interface IContatoRepositorio
    {
        List<ContatoModel> BuscarTabela();

        ContatoModel BuscarId(int Id);

        ContatoModel SaveEdit(ContatoModel contato);

        ContatoModel Adicionar(ContatoModel contato);

        void Apagar(int Id);
    }
}