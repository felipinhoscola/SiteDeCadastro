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

        public UserModel BuscaLogin(string login)
        {
            return _bancoContext.Usuarios.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper());//.ToUpper serve para o login não ser case sensitive
        }

        public UserModel AddUser(UserModel usuario)
        {
            usuario.SetSenhaHash();
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

        public UserModel EditUser(UserModel usuario)
        {
            UserModel userBD = BuscarId(usuario.Id);
            if (userBD == null) throw new System.Exception("Houve um erro na edição do usuário");

            userBD.Name = usuario.Name;
            userBD.Login = usuario.Login;
            userBD.Email = usuario.Email;  
            userBD.Perfil = usuario.Perfil;
            userBD.LastAtt = usuario.LastAtt;

            _bancoContext.Usuarios.Update(userBD);
            _bancoContext.SaveChanges();
            return userBD;
        }

        
    }
}
