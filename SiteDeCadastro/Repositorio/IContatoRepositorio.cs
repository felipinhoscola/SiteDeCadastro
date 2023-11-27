using SiteDeCadastro.Models;

namespace SiteDeCadastro.Repositorio
{
    public interface IContatoRepositorio
    {
        List<ContatoModel> BuscarTabela(int usuarioId);

        ContatoModel BuscarId(int Id);

        ContatoModel SaveEdit(ContatoModel contato);

        ContatoModel Adicionar(ContatoModel contato);

        bool Apagar(int Id);
    }
}