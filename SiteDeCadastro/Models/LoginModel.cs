using System;
using System.ComponentModel.DataAnnotations;

namespace SiteDeCadastro.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Insira o Login do contato!")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Insira a Senha do contato!")]
        public string Password { get; set; }
    }
}
