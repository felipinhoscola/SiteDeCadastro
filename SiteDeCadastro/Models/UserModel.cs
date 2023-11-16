using SiteDeCadastro.Enums;
using SiteDeCadastro.Helper;
using System;
using System.ComponentModel.DataAnnotations;

namespace SiteDeCadastro.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Insira o Nome do contato!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Insira o Login do contato!")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Insira a Senha do contato!")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Insira o Email do contato!")]
        [EmailAddress(ErrorMessage = "O Email está incorreto!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Selecione um Perfil")]
        public PerfilEnum? Perfil { get; set; }
        public DateTime DateCad { get; set; }
        public DateTime? LastAtt { get; set; }

        public bool ConfirmPass(string password)
        { 
            return Password == password.GerarHash(); // testa se a senha infromada é igual a senha da Model
        }
        
        public void SetSenhaHash()
        {
            Password = Password.GerarHash();
        } 

        public string GenerateNewPass()
        {
            string newPass = Guid.NewGuid().ToString().Substring(0, 8);
            Password = newPass.GerarHash();
            return newPass;
        }
    }
}
