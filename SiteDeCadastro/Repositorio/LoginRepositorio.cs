using SiteDeCadastro.Data;
using SiteDeCadastro.Migrations;
using SiteDeCadastro.Models;

namespace SiteDeCadastro.Repositorio
{
    public class LoginRepositorio : ILoginRepositorio
    {
        private readonly BancoContext _bancoContext;

        public LoginRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public bool Login(LoginModel login)
        {
            UserModel userBD = _bancoContext.Usuarios.FirstOrDefault(x => x.Login == login.Login);
            if ( userBD == null) return false;

            return (userBD.Login == login.Login && userBD.Password == login.Password ? true : false);
        }
    }
}
