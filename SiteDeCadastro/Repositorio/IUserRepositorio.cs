using SiteDeCadastro.Models;

namespace SiteDeCadastro.Repositorio
{
    public interface IUserRepositorio
    {
        List<UserModel> BuscarUsers();

        UserModel AddUser(UserModel usuario);

        UserModel BuscarId(int id);

        UserModel EditUser(UserModel usuario);
        bool ConfirmDelUser(int id);
    }
}
