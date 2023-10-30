using SiteDeCadastro.Models;

namespace SiteDeCadastro.Repositorio
{
    public interface IUserRepositorio
    {
        List<UserModel> BuscarUsers();
        UserModel AddUser(UserModel usuario);

        UserModel BuscarId(int id);

        bool ConfirmDelUser(int id);
    }
}
