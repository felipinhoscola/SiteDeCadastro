using SiteDeCadastro.Models;

namespace SiteDeCadastro.Repositorio
{
    public interface ILoginRepositorio
    {
        bool Login(LoginModel login);
    }
}
