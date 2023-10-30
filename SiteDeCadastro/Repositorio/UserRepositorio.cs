using SiteDeCadastro.Data;
using SiteDeCadastro.Migrations;
using SiteDeCadastro.Models;

namespace SiteDeCadastro.Repositorio
{
    public class UserRepositorio : IUserRepositorio
    {
        private readonly BancoContext _bancoContext;

        public UserRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public object ModelState { get; private set; }

        public UserModel AddUser(UserModel usuario)
        {
            _bancoContext.Usuarios.Add(usuario);
            _bancoContext.SaveChanges();

            return usuario;
        }

        public UserModel BuscarId(int id)
        { 
            return _bancoContext.Usuarios.FirstOrDefault(x => x.Id == id);//Vai pegar o primeiro resultado, ou o resultado 
        }

        public List<UserModel> BuscarUsers()
        {
            return _bancoContext.Usuarios.ToList();
        }

        public bool ConfirmDelUser(int id)
        {
            UserModel userBD = BuscarId(id);

            if (userBD == null) throw new System.Exception("Houve um erro na exclusão do usuário");

            _bancoContext.Usuarios.Remove(userBD);
            _bancoContext.SaveChanges();
            return true;
        }
    }
}
