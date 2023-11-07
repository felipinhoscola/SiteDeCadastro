using SiteDeCadastro.Models;

namespace SiteDeCadastro.Helper
{
    public interface ISessionUser
    {
        void CreateSessionUser(UserModel usuario);
        void RemoveSessionUser();
        UserModel GetSessionUser();
    }
}
