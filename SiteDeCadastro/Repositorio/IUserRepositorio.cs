using SiteDeCadastro.Models;

namespace SiteDeCadastro.Repositorio
{
    public interface IUserRepositorio
    {
        List<UserModel> BuscarUsers();

        UserModel AddUser(UserModel usuario);

        UserModel BuscarId(int id);

        UserModel EditUser(UserModel usuario);
        UserModel EditPass(UserModel usuario);
        bool ConfirmDelUser(int id);
        UserModel BuscaLogin(string login);
        UserModel BuscaEmailELogin(string email, string login);
    }
}
